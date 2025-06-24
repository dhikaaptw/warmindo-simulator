using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Warmindo_Simulator.src
{
    internal class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Image Sprite { get; set; }

        public Player(int x, int y, Image sprite)
        {
            X = x;
            Y = y;
            Sprite = sprite;
        }

        public void Move(Keys key, int speed)
        {
            switch (key)
            {
                case Keys.W: Y -= speed; break;
                case Keys.S: Y += speed; break;
                case Keys.A: X -= speed; break;
                case Keys.D: X += speed; break;
            }
        }

        public void Draw(Graphics g)
        {
            if (Sprite != null)
                g.DrawImage(Sprite, X, Y, 40, 40);
            else
                g.FillEllipse(Brushes.Orange, X, Y, 40, 40); // karakter sementara
        }

        public Rectangle GetBounds()
        {
            return new Rectangle(X, Y, 40, 40);
        }
    }
}
