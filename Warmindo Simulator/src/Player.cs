using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Warmindo_Simulator.src
{
    internal class Player
    {
        public int X { get; set; }
        public int Y { get; set; }

        private Dictionary<string, List<Image>> directionFrames = new Dictionary<string, List<Image>>();
        private string currentDirection = "Vertical";
        private int currentFrame = 0;
        private int frameDelay = 0;
        private int frameSpeed = 5;
        private const int PlayerSize = 80;

        public Player(int x, int y, Image sprite)
        {
            X = x;
            Y = y;
        }

        public void LoadFramesFromFolder(string folderPath)
        {
            directionFrames.Clear();

            directionFrames["Vertical"] = new List<Image>();
            directionFrames["Right"] = new List<Image>();
            directionFrames["Left"] = new List<Image>();

            for (int i = 1; i <= 15; i++)
            {
                string path = Path.Combine(folderPath, i + ".png");
                if (File.Exists(path))
                {
                    if (i <= 5)
                        directionFrames["Vertical"].Add(Image.FromFile(path));
                    else if (i <= 10)
                        directionFrames["Right"].Add(Image.FromFile(path));
                    else
                        directionFrames["Left"].Add(Image.FromFile(path));
                }
            }

            if (directionFrames["Vertical"].Count == 0)
            {
                MessageBox.Show("Gagal load sprite! Pastikan file 1-15.png ada di folder assets.");
            }
        }

        public void Animate()
        {
            if (!directionFrames.ContainsKey(currentDirection)) return;
            if (directionFrames[currentDirection].Count == 0) return;

            frameDelay++;
            if (frameDelay >= frameSpeed)
            {
                currentFrame = (currentFrame + 1) % directionFrames[currentDirection].Count;
                frameDelay = 0;
            }
        }

        public void Move(Keys key, int speed, Rectangle obstacle, int formWidth, int formHeight)
        {
            int newX = X;
            int newY = Y;

            if (key == Keys.W || key == Keys.S)
                currentDirection = "Vertical";
            else if (key == Keys.A)
                currentDirection = "Left";
            else if (key == Keys.D)
                currentDirection = "Right";

            switch (key)
            {
                case Keys.W: newY -= speed; break;
                case Keys.S: newY += speed; break;
                case Keys.A: newX -= speed; break;
                case Keys.D: newX += speed; break;
            }

            Rectangle newBounds = new Rectangle(newX, newY, PlayerSize, PlayerSize);

            bool insideForm = newBounds.Left >= 0 &&
                              newBounds.Top >= 0 &&
                              newBounds.Right <= formWidth &&
                              newBounds.Bottom <= formHeight;

            if (insideForm && !newBounds.IntersectsWith(obstacle))
            {
                X = newX;
                Y = newY;
            }
        }

        public void Draw(Graphics g)
        {
            if (directionFrames.ContainsKey(currentDirection) && directionFrames[currentDirection].Count > 0)
            {
                g.DrawImage(directionFrames[currentDirection][currentFrame], X, Y, PlayerSize, PlayerSize);
            }
            else
            {
                g.FillRectangle(Brushes.Blue, X, Y, PlayerSize, PlayerSize); // fallback
            }
        }

        public Rectangle GetBounds()
        {
            return new Rectangle(X, Y, PlayerSize, PlayerSize);
        }
    }
}
