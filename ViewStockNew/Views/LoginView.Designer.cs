namespace ViewStockNew.Views
{
    partial class LoginView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginView));
            LblMessage = new Label();
            BtnIniciarSesion = new Button();
            label2 = new Label();
            label1 = new Label();
            TxtPassword = new TextBox();
            TxtUser = new TextBox();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // LblMessage
            // 
            LblMessage.AutoSize = true;
            LblMessage.ForeColor = SystemColors.ButtonFace;
            LblMessage.Location = new Point(156, 333);
            LblMessage.Name = "LblMessage";
            LblMessage.Size = new Size(161, 15);
            LblMessage.TabIndex = 13;
            LblMessage.Text = "¿Has olvidado tu contraseña?";
            // 
            // BtnIniciarSesion
            // 
            BtnIniciarSesion.BackColor = Color.DarkOrange;
            BtnIniciarSesion.FlatStyle = FlatStyle.Popup;
            BtnIniciarSesion.Font = new Font("Lucida Sans", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            BtnIniciarSesion.ForeColor = Color.Black;
            BtnIniciarSesion.Location = new Point(128, 274);
            BtnIniciarSesion.Name = "BtnIniciarSesion";
            BtnIniciarSesion.Size = new Size(216, 56);
            BtnIniciarSesion.TabIndex = 12;
            BtnIniciarSesion.Text = "INICIAR SESIÓN";
            BtnIniciarSesion.UseVisualStyleBackColor = false;
            BtnIniciarSesion.Click += BtnIniciarSesion_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Lucida Sans", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.Gold;
            label2.Location = new Point(62, 193);
            label2.Name = "label2";
            label2.Size = new Size(95, 13);
            label2.TabIndex = 11;
            label2.Text = "CONTRASEÑA";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Lucida Sans", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.DarkOrange;
            label1.Location = new Point(62, 138);
            label1.Name = "label1";
            label1.Size = new Size(315, 15);
            label1.TabIndex = 10;
            label1.Text = "INICIA SESIÓN CON EL NOMBRE DE TU CUENTA";
            // 
            // TxtPassword
            // 
            TxtPassword.BackColor = Color.FromArgb(66, 66, 66);
            TxtPassword.BorderStyle = BorderStyle.FixedSingle;
            TxtPassword.Font = new Font("Lucida Sans", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TxtPassword.ForeColor = SystemColors.ControlLightLight;
            TxtPassword.Location = new Point(62, 209);
            TxtPassword.Name = "TxtPassword";
            TxtPassword.PasswordChar = '♥';
            TxtPassword.Size = new Size(362, 26);
            TxtPassword.TabIndex = 9;
            TxtPassword.KeyDown += TxtPassword_KeyDown;
            // 
            // TxtUser
            // 
            TxtUser.BackColor = Color.FromArgb(66, 66, 66);
            TxtUser.BorderStyle = BorderStyle.FixedSingle;
            TxtUser.Font = new Font("Lucida Sans", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TxtUser.ForeColor = SystemColors.ControlLightLight;
            TxtUser.Location = new Point(62, 156);
            TxtUser.Name = "TxtUser";
            TxtUser.Size = new Size(362, 26);
            TxtUser.TabIndex = 8;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(55, 45);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(370, 68);
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // LoginView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(38, 38, 38);
            ClientSize = new Size(495, 421);
            Controls.Add(LblMessage);
            Controls.Add(BtnIniciarSesion);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(TxtPassword);
            Controls.Add(TxtUser);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LoginView";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LoginView";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LblMessage;
        private Button BtnIniciarSesion;
        private Label label2;
        private Label label1;
        private TextBox TxtPassword;
        private TextBox TxtUser;
        private PictureBox pictureBox1;
    }
}