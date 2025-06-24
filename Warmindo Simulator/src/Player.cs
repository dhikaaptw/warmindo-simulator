using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;

namespace Warmindo_Simulator.src
{
    internal class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Image Sprite { get; set; }

        private List<Image> frames = new List<Image>();
        private int currentFrame = 0;
        private int frameDelay = 0;
        private int frameSpeed = 5; // makin kecil makin cepat

        private const int PlayerSize = 80; // Ukuran player tetap konsisten

        public Player(int x, int y, Image sprite)
        {
            X = x;
            Y = y;
            Sprite = sprite;
        }

        public void LoadFramesFromFolder(string folderPath)
        {
            frames.Clear();

            for (int i = 1; i <= 15; i++)
            {
                string path = Path.Combine(folderPath, $"{i}.png");
                if (File.Exists(path))
                {
                    frames.Add(Image.FromFile(path));
                }
                else
                {
                    Console.WriteLine($"Tidak ditemukan: {path}");
                }
            }

            if (frames.Count == 0)
            {
                MessageBox.Show("Gagal load sprite! Pastikan 1.png - 15.png ada di folder assets.");
            }
        }

        public void Animate()
        {
            if (frames.Count == 0) return;

            frameDelay++;
            if (frameDelay >= frameSpeed)
            {
                currentFrame = (currentFrame + 1) % frames.Count;
                frameDelay = 0;
            }
        }

        // ✅ Cegah tabrak obstacle dan keluar layar
        public void Move(Keys key, int speed, Rectangle obstacle, int formWidth, int formHeight)
        {
            int newX = X;
            int newY = Y;

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
            if (frames.Count > 0)
                g.DrawImage(frames[currentFrame], X, Y, PlayerSize, PlayerSize);
            else if (Sprite != null)
                g.DrawImage(Sprite, X, Y, PlayerSize, PlayerSize);
            else
                g.FillRectangle(Brushes.OrangeRed, X, Y, PlayerSize, PlayerSize); // fallback
        }

        public Rectangle GetBounds()
        {
            return new Rectangle(X, Y, PlayerSize, PlayerSize);
        }
    }
}
