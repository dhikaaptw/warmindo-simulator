using System;
using System.Drawing;
using System.Windows.Forms;

namespace Warmindo_Simulator.src
{
    internal class LogicGame
    {
        private Player player;
        private Kitchen kitchen;
        private OrderSystem orderSystem;
        private InputHandler input;
        private Rectangle komporRect;
        private Rectangle mejaRect;
        private Image mejaImage;
        private Image lantaiImage;

        private int score = 0;
        private int delayBeforeNewCustomer = 0;
        private int customerTimeCounter = 0;

        private string selectedMenu = "Mie Instan";

        public string SelectedMenu
        {
            get { return selectedMenu; }
            set { selectedMenu = value; }
        }

        public string CookingStatusText
        {
            get { return kitchen.GetStatusText(SelectedMenu); }
        }

        public int Score
        {
            get { return score; }
        }

        public Rectangle KomporRect
        {
            get { return komporRect; }
        }

        public LogicGame()
        {
            player = new Player(100, 100, null);
            player.LoadFramesFromFolder("assets");

            kitchen = new Kitchen();
            orderSystem = new OrderSystem();
            input = new InputHandler();

            lantaiImage = AssetManager.LoadImage("assets/lantai.png");

            komporRect = new Rectangle(300, 150, 80, 80);
            mejaImage = AssetManager.LoadImage("assets/mejaServe.png");
            mejaRect = new Rectangle(komporRect.Right + 120, komporRect.Top, 160, 72); // posisinya di kanan kompor

            orderSystem.AddCustomer();
        }

        public void HandleKeyDown(Keys key)
        {
            input.KeyDown(key);
        }

        public void HandleKeyUp(Keys key)
        {
            input.KeyUp(key);
        }

        public void Update(Form form)
        {
            if (input.IsKeyPressed())
            {
                player.Move(
                    input.GetActiveKey().Value,
                    5,
                    GetGabunganObstacle(), // gabungan kompor + meja
                    form.ClientSize.Width,
                    form.ClientSize.Height
            );

                player.Animate();
            }

            kitchen.UpdateCooking(delegate () {
                MessageBox.Show("Masakan siap disajikan!");
            });

            Customer current = orderSystem.GetFirstCustomer();
            if (current != null)
            {
                customerTimeCounter++;
                if (customerTimeCounter >= 33)
                {
                    customerTimeCounter = 0;
                    current.DecreaseWaitTime();

                    if (current.WaitTime <= 0 && !current.IsServed && !current.IsAngry)
                    {
                        current.IsAngry = true;
                        MessageBox.Show("Pelanggan marah dan pergi!");
                        orderSystem.RemoveFirstCustomer();
                        delayBeforeNewCustomer = 60;
                    }
                }
            }

            if (delayBeforeNewCustomer > 0)
            {
                delayBeforeNewCustomer--;
            }
            else if (orderSystem.Customers.Count == 0)
            {
                orderSystem.AddCustomer();
            }
        }

        private Rectangle GetGabunganObstacle()
        {
            return Rectangle.Union(komporRect, mejaRect); // jadi 1 area
        }

        public bool IsNearMeja()
        {
            Rectangle playerRect = player.GetBounds();
            return playerRect.IntersectsWith(mejaRect);
        }

        public bool IsNearMejaServe()
        {
            Rectangle playerRect = player.GetBounds();
            Rectangle detectArea = new Rectangle(mejaRect.X, mejaRect.Y, mejaRect.Width, mejaRect.Height);

            // Optional: perbesar sedikit area meja biar lebih gampang trigger
            detectArea.Inflate(10, 10);

            return playerRect.IntersectsWith(detectArea);
        }


        public void Draw(Graphics g)
        {
            if (lantaiImage != null)
            {
                g.DrawImage(lantaiImage, 0, 0, 800, 600); // ukuran formny 800x600
            }

            Image kompor = AssetManager.LoadImage("assets/kompor.png");
            if (kompor != null)
            {
                int scale = 2; 
                int width = kompor.Width * scale;
                int height = kompor.Height * scale;

                g.DrawImage(kompor, komporRect.X, komporRect.Y, width, height);
            }

            if (mejaImage != null)
            {
                g.DrawImage(mejaImage, mejaRect);
            }
            player.Draw(g);
        }

        public bool IsNearKompor()
        {
            Rectangle playerBounds = player.GetBounds();
            Rectangle areaBawahKompor = new Rectangle(
                komporRect.X,
                komporRect.Bottom,
                komporRect.Width,
                50 // tinggi area dekat bawah kompor
            );

            return playerBounds.IntersectsWith(areaBawahKompor);
        }


        public void StartCooking()
        {
            kitchen.StartCooking(SelectedMenu);
        }

        public void Serve()
        {
            Customer current = orderSystem.GetFirstCustomer();
            if (!kitchen.IsCooking && current != null && !current.IsServed)
            {
                if (current.Order == SelectedMenu)
                {
                    score += 10;
                    MessageBox.Show("Pesanan sesuai! Skor +10");
                }
                else
                {
                    MessageBox.Show("Pesanan salah!");
                }
                current.IsServed = true;
                orderSystem.RemoveFirstCustomer();
                orderSystem.AddCustomer();
            }
            else
            {
                MessageBox.Show("Masakan belum siap!");
            }
        }
        public void Reset()
        {
            orderSystem.Clear();
            orderSystem.AddCustomer();
            score = 0;
            delayBeforeNewCustomer = 0;
            customerTimeCounter = 0;
        }

        public string GetOrderText()
        {
            Customer c = orderSystem.GetFirstCustomer();
            if (c != null)
            {
                return "Pesanan: " + c.Order;
            }
            return "Tidak ada pelanggan";
        }

        public int GetTimeLeft()
        {
            Customer c = orderSystem.GetFirstCustomer();
            if (c != null)
            {
                return c.WaitTime;
            }
            return 0;
        }
    }
}