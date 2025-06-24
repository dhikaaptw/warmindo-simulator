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

        public Form1()
        {
            InitializeComponent();

            player = new Player(50, 50, Image.FromFile("assets/sprite.png"));

            gameTimer.Interval = 1000; // 1 detik
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            cmbMenu.Items.Add("Mie Instan");
            cmbMenu.Items.Add("Es Teh Manis");
            cmbMenu.SelectedIndex = 0; // default ke menu pertama

            AddNewCustomer(); // mulai dengan 1 pelanggan
        }

        private void TampilkanPelanggan()
        {
            if (customerQueue.Count > 0)
            {
                Customer current = customerQueue[0];
                lblPesanan.Text = $"Pesanan: {current.Order}";
                //picPelanggan.Image = Image.FromFile("assets/sprite.png");
            }
            else
            {
                lblPesanan.Text = "Tidak ada pelanggan";
                picPelanggan.Image = null;
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
           // picPelanggan.Image = Image.FromFile("assets/sprite.png"); // ganti sesuai gambar kamu

            TampilkanPelanggan();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (isCooking)
            {
                cookTimeLeft--;
                string selectedMenu = cmbMenu.SelectedItem.ToString();
                lblCookingStatus.Text = $"Memasak {selectedMenu} - sisa {cookTimeLeft} detik...";

                if (cookTimeLeft <= 0)
                {
                    isCooking = false;
                    MessageBox.Show("Masakan siap disajikan!");
                }
            }

            if (customerQueue.Count > 0)
            {
                Customer current = customerQueue[0];
                current.DecreaseWaitTime();

                lblTimer.Text = $"Waktu: {current.WaitTime}";

                if (current.WaitTime <= 0 && !current.IsServed)
                {
                    MessageBox.Show("Pelanggan marah dan pergi!");
                    customerQueue.RemoveAt(0);
                    AddNewCustomer();
                }
            }

            lblSkor.Text = $"Skor: {score}";
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

            if (selectedMenu == "Mie Instan")
                cookTimeLeft = 10;
            else if (selectedMenu == "Es Teh Manis")
                cookTimeLeft = 5;

            isCooking = true;
            lblCookingStatus.Text = $"Memasak {selectedMenu} - sisa {cookTimeLeft} detik...";
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            customerQueue.Clear();
            score = 0;
            isCooking = false;
            cookTimeLeft = 0;

            btnServe.Enabled = true;
            btnCook.Enabled = true;
            cmbMenu.Enabled = true;
            btnRestart.Visible = false;

            gameTimer.Start();
            AddNewCustomer();

            lblSkor.Text = "Skor: 0";
            lblTimer.Text = "Waktu: 0";
            lblPesanan.Text = "Pesanan: -";
            picPelanggan.Image = null;
        }
    }
}
