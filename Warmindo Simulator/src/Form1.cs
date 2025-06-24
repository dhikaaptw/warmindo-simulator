using Warmindo_Simulator.src;
using System;
using System.Windows.Forms;
using System.Drawing;

namespace Warmindo_Simulator
{
    public partial class Form1 : Form
    {
        private LogicGame logic;
        private UIManager ui;
        private System.Windows.Forms.Timer gameTimer;

        public Form1()
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.DoubleBuffered = true;

            logic = new LogicGame();
            ui = new UIManager(lblPesanan, lblTimer, lblSkor, lblCookingStatus);

            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 30;
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.KeyUp += new KeyEventHandler(Form1_KeyUp);

            cmbMenu.Items.Add("Mie Instan");
            cmbMenu.Items.Add("Es Teh Manis");
            cmbMenu.SelectedIndex = 0;
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
