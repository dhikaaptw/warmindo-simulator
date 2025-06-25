using Warmindo_Simulator.src;
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Media;

namespace Warmindo_Simulator
{
    public partial class Form1 : Form
    {
        private LogicGame logic;
        private UIManager ui;
        private System.Windows.Forms.Timer gameTimer;
        private SoundPlayer bgmPlayer;

        public Form1()
        {
            InitializeComponent();

            cmbMenu.DropDownStyle = ComboBoxStyle.DropDownList;

            this.KeyPreview = true;
            this.DoubleBuffered = true;

            bgmPlayer = new SoundPlayer("assets/dj.wav");
            bgmPlayer.PlayLooping(); // biar musik ngulang terus
            

            logic = new LogicGame();
            ui = new UIManager(lblPesanan, lblTimer, lblSkor, lblCookingStatus);

            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 50;
            gameTimer.Tick += GameLoop;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.KeyUp += new KeyEventHandler(Form1_KeyUp);

            cmbMenu.Items.Add("Mie Instan");
            cmbMenu.Items.Add("Es Teh Manis");
            cmbMenu.Items.Add("Nasi Goreng");
            cmbMenu.Items.Add("Minuman Manis");
            cmbMenu.SelectedIndex = 0;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            logic.Update(this); // logika utama game
            btnCook.Visible = logic.IsNearKompor(); // tombol masak muncul kalau dekat kompor
            btnServe.Visible = logic.IsNearMejaServe();
            Invalidate(); // untuk ngegambar ulang layar
        }

        private void GameLoop(object sender, EventArgs e)
        {
            logic.SelectedMenu = cmbMenu.SelectedItem.ToString();
            logic.Update(this);
            ui.UpdateUI(logic);
            this.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            logic.HandleKeyDown(e.KeyCode);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            logic.HandleKeyUp(e.KeyCode);
        }

        private void btnCook_Click(object sender, EventArgs e)
        {
            logic.StartCooking();
        }

        private void btnServe_Click(object sender, EventArgs e)
        {
            logic.Serve();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            logic.Reset();
            ui.ResetUI();
        }

        private void lblPesanan_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Itu pesanan pelanggan ya, jangan disentuh");
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            logic.Draw(e.Graphics);
        }
    }
}
