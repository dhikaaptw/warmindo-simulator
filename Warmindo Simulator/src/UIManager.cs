using System.Windows.Forms;

namespace Warmindo_Simulator.src
{
    internal class UIManager
    {
        private Label lblPesanan;
        private Label lblTimer;
        private Label lblSkor;
        private Label lblCookingStatus;

        public UIManager(Label pesanan, Label timer, Label skor, Label cookingStatus)
        {
            lblPesanan = pesanan;
            lblTimer = timer;
            lblSkor = skor;
            lblCookingStatus = cookingStatus;
        }

        public void UpdateUI(LogicGame logicGame)
        {
            lblPesanan.Text = logicGame.GetOrderText();
            lblTimer.Text = "Waktu: " + logicGame.GetTimeLeft().ToString();
            lblSkor.Text = "Skor: " + logicGame.Score.ToString();
            lblCookingStatus.Text = logicGame.CookingStatusText;
        }

        public void ResetUI()
        {
            lblPesanan.Text = "Pesanan: -";
            lblTimer.Text = "Waktu: 0";
            lblSkor.Text = "Skor: 0";
            lblCookingStatus.Text = "";
        }
    }
}
