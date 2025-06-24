namespace Warmindo_Simulator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnServe = new Button();
            lblPesanan = new Label();
            lblTimer = new Label();
            lblSkor = new Label();
            cmbMenu = new ComboBox();
            btnCook = new Button();
            label1 = new Label();
            btnRestart = new Button();
            lblCookingStatus = new Label();
            SuspendLayout();
            // 
            // btnServe
            // 
            btnServe.Location = new Point(594, 82);
            btnServe.Name = "btnServe";
            btnServe.Size = new Size(150, 46);
            btnServe.TabIndex = 0;
            btnServe.Text = "Serve";
            btnServe.UseVisualStyleBackColor = true;
            btnServe.Click += btnServe_Click;
            // 
            // lblPesanan
            // 
            lblPesanan.AutoSize = true;
            lblPesanan.Location = new Point(45, 395);
            lblPesanan.Name = "lblPesanan";
            lblPesanan.Size = new Size(141, 32);
            lblPesanan.TabIndex = 1;
            lblPesanan.Text = "Pesanan = -";
            lblPesanan.Click += lblPesanan_Click;
            // 
            // lblTimer
            // 
            lblTimer.AutoSize = true;
            lblTimer.Location = new Point(337, 9);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(113, 32);
            lblTimer.TabIndex = 2;
            lblTimer.Text = "Waktu : 0";
            // 
            // lblSkor
            // 
            lblSkor.AutoSize = true;
            lblSkor.Location = new Point(624, 9);
            lblSkor.Name = "lblSkor";
            lblSkor.Size = new Size(93, 32);
            lblSkor.TabIndex = 3;
            lblSkor.Text = "Skor : 0";
            // 
            // cmbMenu
            // 
            cmbMenu.FormattingEnabled = true;
            cmbMenu.Location = new Point(0, 0);
            cmbMenu.Name = "cmbMenu";
            cmbMenu.Size = new Size(242, 40);
            cmbMenu.TabIndex = 5;
            // 
            // btnCook
            // 
            btnCook.Location = new Point(320, 82);
            btnCook.Name = "btnCook";
            btnCook.Size = new Size(150, 46);
            btnCook.TabIndex = 6;
            btnCook.Text = "Masak";
            btnCook.UseVisualStyleBackColor = true;
            btnCook.Click += btnCook_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(624, 320);
            label1.Name = "label1";
            label1.Size = new Size(0, 32);
            label1.TabIndex = 7;
            // 
            // btnRestart
            // 
            btnRestart.Location = new Point(320, 288);
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(150, 46);
            btnRestart.TabIndex = 8;
            btnRestart.Text = "Restart Game";
            btnRestart.UseVisualStyleBackColor = true;
            btnRestart.Visible = false;
            // 
            // lblCookingStatus
            // 
            lblCookingStatus.AutoSize = true;
            lblCookingStatus.Location = new Point(357, 395);
            lblCookingStatus.Name = "lblCookingStatus";
            lblCookingStatus.Size = new Size(267, 32);
            lblCookingStatus.TabIndex = 9;
            lblCookingStatus.Text = "Tidak Sedang Memasak";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(778, 466);
            Controls.Add(lblCookingStatus);
            Controls.Add(btnRestart);
            Controls.Add(label1);
            Controls.Add(btnCook);
            Controls.Add(cmbMenu);
            Controls.Add(lblSkor);
            Controls.Add(lblTimer);
            Controls.Add(lblPesanan);
            Controls.Add(btnServe);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnServe;
        private Label lblPesanan;
        private Label lblTimer;
        private Label lblSkor;
        private ComboBox cmbMenu;
        private Button btnCook;
        private Label label1;
        private Button btnRestart;
        private Label lblCookingStatus;
    }
}
