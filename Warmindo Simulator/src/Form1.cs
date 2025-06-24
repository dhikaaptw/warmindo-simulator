using Warmindo_Simulator.src;

namespace Warmindo_Simulator
{
    public partial class Form1 : Form
    {
        private List<Customer> customerQueue = new List<Customer>();
        private System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer();
        private int score = 0;
        private int cookTimeLeft = 0;
        private bool isCooking = false;
        private Random random = new Random();
        private Player player;
        private Keys? lastKeyPressed = null;
        private int delayBeforeNewCustomer = 0;
        private bool customerJustLeft = false;
        private int customerTimeCounter = 0;
        private int cookingTimeCounter = 0;
        private Rectangle komporRect = new Rectangle(200, 100, 200, 200);
        private Image komporImage;

        public Form1()
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;

            player = new Player(100, 100, null);
            player.LoadFramesFromFolder("assets");
            this.DoubleBuffered = true;

            komporImage = Image.FromFile("assets/kompor.png");

            gameTimer.Interval = 30;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            cmbMenu.Items.Add("Mie Instan");
            cmbMenu.Items.Add("Es Teh Manis");
            cmbMenu.SelectedIndex = 0;

            AddNewCustomer();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.A || e.KeyCode == Keys.S || e.KeyCode == Keys.D)
            {
                lastKeyPressed = e.KeyCode;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == lastKeyPressed)
            {
                lastKeyPressed = null;
            }
        }

        private void TampilkanPelanggan()
        {
            if (customerQueue.Count > 0)
            {
                Customer current = customerQueue[0];
                lblPesanan.Text = $"Pesanan: {current.Order}";
                //picPelanggan.Image = null;
            }
            else
            {
                lblPesanan.Text = "Tidak ada pelanggan";
                //picPelanggan.Image = null;
            }
        }

        private void AddNewCustomer()
        {
            if (customerQueue.Count >= 5)
            {
                gameTimer.Stop();
                btnServe.Enabled = false;
                btnCook.Enabled = false;
                cmbMenu.Enabled = false;
                btnRestart.Visible = true;
                return;
            }

            string[] orders = { "Mie Instan", "Es Teh Manis" };
            string selectedOrder = orders[random.Next(orders.Length)];

            Customer newCustomer = new Customer(selectedOrder);
            customerQueue.Add(newCustomer);

            lblPesanan.Text = $"Pesanan: {selectedOrder}";
            TampilkanPelanggan();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (delayBeforeNewCustomer > 0)
            {
                delayBeforeNewCustomer--;
                return;
            }

            if (customerJustLeft)
            {
                customerJustLeft = false;
                AddNewCustomer();
            }

            if (lastKeyPressed != null)
            {
                player.Move(lastKeyPressed.Value, 5, komporRect, this.ClientSize.Width, this.ClientSize.Height);
                player.Animate();
            }

            if (isCooking)
            {
                cookingTimeCounter++;
                if (cookingTimeCounter >= 33)
                {
                    cookingTimeCounter = 0;
                    cookTimeLeft--;

                    string selectedMenu = cmbMenu.SelectedItem.ToString();
                    lblCookingStatus.Text = $"Memasak {selectedMenu} - sisa {cookTimeLeft} detik...";

                    if (cookTimeLeft <= 0)
                    {
                        isCooking = false;
                        MessageBox.Show("Masakan siap disajikan!");
                    }
                }
            }

            if (customerQueue.Count > 0)
            {
                Customer current = customerQueue[0];
                customerTimeCounter++;
                if (customerTimeCounter >= 33)
                {
                    customerTimeCounter = 0;
                    current.DecreaseWaitTime();
                }

                lblTimer.Text = $"Waktu: {current.WaitTime}";

                if (current.WaitTime <= 0 && !current.IsServed && !current.IsAngry)
                {
                    current.IsAngry = true;
                    MessageBox.Show("Pelanggan marah dan pergi!");
                    customerQueue.RemoveAt(0);
                    TampilkanPelanggan();

                    delayBeforeNewCustomer = 60;
                    customerJustLeft = true;
                    return;
                }
            }

            lblSkor.Text = $"Skor: {score}";
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.DrawImage(komporImage, komporRect.X, komporRect.Y, komporRect.Width, komporRect.Height);

            player.Draw(e.Graphics);
        }

        private void btnServe_Click(object sender, EventArgs e)
        {
            if (!isCooking && cookTimeLeft <= 0)
            {
                if (customerQueue.Count > 0)
                {
                    Customer current = customerQueue[0];
                    string playerServed = cmbMenu.SelectedItem.ToString();

                    if (playerServed == current.Order && !current.IsServed)
                    {
                        score += 10;
                        MessageBox.Show("Pesanan sesuai! Skor +10");
                        current.IsServed = true;
                        customerQueue.RemoveAt(0);
                        TampilkanPelanggan();
                        AddNewCustomer();
                    }
                    else
                    {
                        customerQueue.RemoveAt(0);
                        TampilkanPelanggan();
                        MessageBox.Show("Pesanan salah!");
                        AddNewCustomer();
                    }
                }
            }
            else
            {
                MessageBox.Show("Masakan belum siap!");
            }
        }

        private void lblPesanan_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Itu pesanan pelanggan ya, jangan disentuh");
        }

        private void btnCook_Click(object sender, EventArgs e)
        {
            string selectedMenu = cmbMenu.SelectedItem.ToString();

            cookTimeLeft = selectedMenu switch
            {
                "Mie Instan" => 10,
                "Es Teh Manis" => 5,
                _ => 0
            };

            isCooking = true;
            cookingTimeCounter = 0;
            lblCookingStatus.Text = $"Memasak {selectedMenu} - sisa {cookTimeLeft} detik...";
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            customerQueue.Clear();
            score = 0;
            isCooking = false;
            cookTimeLeft = 0;
            delayBeforeNewCustomer = 0;
            customerJustLeft = false;
            customerTimeCounter = 0;
            cookingTimeCounter = 0;

            btnServe.Enabled = true;
            btnCook.Enabled = true;
            cmbMenu.Enabled = true;
            btnRestart.Visible = false;

            gameTimer.Start();
            AddNewCustomer();

            lblSkor.Text = "Skor: 0";
            lblTimer.Text = "Waktu: 0";
            lblPesanan.Text = "Pesanan: -";
            //picPelanggan.Image = null;
        }
    }
}
