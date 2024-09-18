//using System.Windows.Forms;

namespace _2DGame
{
    public partial class Form1 : Form
    {
        #region vars
        private const int TileSize = 32;
        //private const int orgTileSize = 16;
        private const int GridWidth = 16;
        private const int GridHeight = 12;
        private const int scale = 3;

        //private const int TileSize = orgTileSize * scale;
        //private Brush playerColor = Brushes.Blue;
        private Image playerImage = null; 
        private Point playerPosition = new Point(0, 0);

        //private System.Timers.Timer movementTimer;
        private System.Windows.Forms.Timer movementTimer;
        private Keys currentKey;
        private bool isKeyPressed = false;

        #endregion vars

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // Verhindert Flackern beim Zeichnen
            this.KeyDown += new KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new KeyEventHandler(this.Form1_KeyUp);

            //Timer
            movementTimer = new System.Windows.Forms.Timer();
            movementTimer.Interval = 80;
            movementTimer.Tick += new EventHandler(this.OnMovementTimerTick);

            playerImage = ByteArrayToImage(Properties.Resources.Player_foward);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // Spielfeld zeichnen
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    g.DrawRectangle(Pens.Black, x * TileSize, y * TileSize, TileSize, TileSize);
                }
            }

            // Spielerfigur zeichnen
            //g.FillRectangle(playerImage, playerPosition.X * TileSize, playerPosition.Y * TileSize, TileSize, TileSize);
            g.DrawImage(playerImage, playerPosition.X * TileSize, playerPosition.Y * TileSize, TileSize, TileSize);
        }

        #region Keys

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isKeyPressed)
            {
                isKeyPressed = true;
                currentKey = e.KeyCode;
                movementTimer.Start();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == currentKey) 
            {
                isKeyPressed = false;
                movementTimer.Stop(); 
            }
        }

        #endregion Keys

        private void OnMovementTimerTick(object sender, EventArgs e)
        {
            switch (currentKey)
            {
                case Keys.Up: // Nach oben
                    if (playerPosition.Y > 0)
                        playerPosition.Y--;
                    playerImage = ByteArrayToImage(Properties.Resources.Player_back); // Konvertiere Bild von byte[] zu Image
                    break;
                case Keys.Down: // Nach unten
                    if (playerPosition.Y < GridHeight - 1)
                        playerPosition.Y++;
                    playerImage = ByteArrayToImage(Properties.Resources.Player_foward);
                    break;
                case Keys.Left:
                    if (playerPosition.X > 0)
                        playerPosition.X--;
                    playerImage = ByteArrayToImage(Properties.Resources.Player_left);
                    //playerImage = Properties.Resources.Player_left;
                    break;
                case Keys.Right:
                    if (playerPosition.X < GridWidth - 1)
                        playerPosition.X++;
                    playerImage = ByteArrayToImage(Properties.Resources.Player_right);
                    break;

            }
            Invalidate();
        }

        public Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }

    }
}
