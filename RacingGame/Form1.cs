using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RacingGame
{
    public partial class RacingGame : Form
    {
        private Point _firstPos; //save, gdzie click first
        private bool dragging; //save, przyciągamy okno.
        private bool _statusLose = false;
        private int _countCoins = 0;
        public RacingGame()
        {
            InitializeComponent();

            bg1.MouseDown += MouseClickDown;
            bg1.MouseUp += MouseClickUp;
            bg1.MouseMove += MouseClickMove;
            bg2.MouseDown += MouseClickDown;
            bg2.MouseUp += MouseClickUp;
            bg2.MouseMove += MouseClickMove;

            loseInfo.Visible = false;
            btnrestart.Visible = false;

            KeyPreview = true;
        }

        private void MouseClickDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            _firstPos.X = e.X;
            _firstPos.Y = e.Y;
        }

        private void MouseClickUp(object sender, MouseEventArgs e) 
        {
            dragging = false;
        }

        private void MouseClickMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point currentPos = PointToScreen(new Point(e.X, e.Y));
                this.Location = new Point(currentPos.X - _firstPos.X + bg1.Left, currentPos.Y - _firstPos.Y);
            }
        }
 
        private void RacingGame_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape) 
            {
                this.Close();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            int speed = 7;
            bg1.Left -= speed;
            bg2.Left -= speed;
            coin.Left -= speed;

            int carSpeed = 10;
            enemy1.Left -= carSpeed;
            enemy2.Left -= carSpeed;
            enemy3.Left -= carSpeed;

            if (bg1.Left <= 0)
            {
                bg1.Left = 639;
                bg2.Left = 0;
            }

            if (coin.Left <= 0)
            {
                coin.Left = 700;
                Random random = new Random();
                coin.Top = random.Next(21, 291);
            }

            if (enemy1.Left <= -80)
            {
                enemy1.Left = 750;
                Random random = new Random();
                enemy1.Top = random.Next(21, 291);
            }
            if (enemy2.Left <= -80)
            {
                enemy2.Left = 800;
                Random random = new Random();
                enemy2.Top = random.Next(21, 291);
            }
            if (enemy3.Left <= -80)
            {
                enemy3.Left = 870;
                Random random = new Random();
                enemy3.Top = random.Next(21, 291);
            }

            if (player.Bounds.IntersectsWith(enemy1.Bounds) 
                || player.Bounds.IntersectsWith(enemy2.Bounds)
                || player.Bounds.IntersectsWith(enemy3.Bounds))
            {
                timer.Enabled = false;
                loseInfo.Visible = true;
                btnrestart.Visible = true;
                _statusLose = true;
            }

            if (player.Bounds.IntersectsWith(coin.Bounds))
            {
                _countCoins++;
                coinsInfo.Text = $"Coins: {_countCoins}";
                coin.Left = 700;
                Random random = new Random();
                coin.Top = random.Next(21, 291);
            }
        }

        private void RacingGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (_statusLose)
            {
                return;
            }

            int speed = 10;
            if ((e.KeyCode == Keys.Up || e.KeyCode == Keys.W) && player.Top > 21)
            {
                player.Top -= speed;
            } 
            else if ((e.KeyCode == Keys.Down || e.KeyCode == Keys.S) && player.Top < 291)
            {
                player.Top += speed;
            } 
            else if ((e.KeyCode == Keys.Right || e.KeyCode == Keys.D) && player.Left < 535)
            {
                player.Left += speed;
            } 
            else if ((e.KeyCode == Keys.Left || e.KeyCode == Keys.A) && player.Left > 4)
            {
                player.Left -= speed;
            }
        }

        private void btnrestart_Click(object sender, EventArgs e)
        {
            enemy1.Left = 700;
            enemy2.Left = 800;
            enemy3.Left = 930;
            player.Top = 150;
            player.Left = 12;

            loseInfo.Visible = false;
            btnrestart.Visible = false;
            timer.Enabled = true;
            _statusLose = false;

            _countCoins = 0;
            coinsInfo.Text = $"Coins: {_countCoins}";
            coin.Left = 900;
        }
    }
}
