namespace ViewStockNew.Views
{
    partial class SplashView
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashView));
            TmrTiempo = new System.Windows.Forms.Timer(components);
            ProBarSplash = new ProgressBar();
            LblMessage = new Label();
            BtnIniciarSesion = new Button();
            LblContraseña = new Label();
            LblSesion = new Label();
            TxtPassword = new TextBox();
            TxtUser = new TextBox();
            PctTittle = new PictureBox();
            PctLoading = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)PctTittle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PctLoading).BeginInit();
            SuspendLayout();
            // 
            // TmrTiempo
            // 
            TmrTiempo.Enabled = true;
            TmrTiempo.Tick += TmrTiempo_Tick;
            // 
            // ProBarSplash
            // 
            ProBarSplash.Location = new Point(37, 384);
            ProBarSplash.Name = "ProBarSplash";
            ProBarSplash.Size = new Size(370, 24);
            ProBarSplash.TabIndex = 90;
            ProBarSplash.Visible = false;
            // 
            // LblMessage
            // 
            LblMessage.AutoSize = true;
            LblMessage.ForeColor = SystemColors.ButtonFace;
            LblMessage.Location = new Point(139, 346);
            LblMessage.Name = "LblMessage";
            LblMessage.Size = new Size(161, 15);
            LblMessage.TabIndex = 97;
            LblMessage.Text = "¿Has olvidado tu contraseña?";
            LblMessage.Visible = false;
            // 
            // BtnIniciarSesion
            // 
            BtnIniciarSesion.BackColor = Color.DarkOrange;
            BtnIniciarSesion.FlatStyle = FlatStyle.Popup;
            BtnIniciarSesion.Font = new Font("Lucida Sans", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            BtnIniciarSesion.ForeColor = Color.Black;
            BtnIniciarSesion.Location = new Point(111, 287);
            BtnIniciarSesion.Name = "BtnIniciarSesion";
            BtnIniciarSesion.Size = new Size(216, 56);
            BtnIniciarSesion.TabIndex = 96;
            BtnIniciarSesion.Text = "INICIAR SESIÓN";
            BtnIniciarSesion.UseVisualStyleBackColor = false;
            BtnIniciarSesion.Visible = false;
            BtnIniciarSesion.Click += BtnIniciarSesion_Click;
            // 
            // LblContraseña
            // 
            LblContraseña.AutoSize = true;
            LblContraseña.Font = new Font("Lucida Sans", 9F, FontStyle.Bold, GraphicsUnit.Point);
            LblContraseña.ForeColor = Color.Gold;
            LblContraseña.Location = new Point(45, 206);
            LblContraseña.Name = "LblContraseña";
            LblContraseña.Size = new Size(95, 13);
            LblContraseña.TabIndex = 95;
            LblContraseña.Text = "CONTRASEÑA";
            LblContraseña.Visible = false;
            // 
            // LblSesion
            // 
            LblSesion.AutoSize = true;
            LblSesion.Font = new Font("Lucida Sans", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            LblSesion.ForeColor = Color.DarkOrange;
            LblSesion.Location = new Point(45, 151);
            LblSesion.Name = "LblSesion";
            LblSesion.Size = new Size(315, 15);
            LblSesion.TabIndex = 94;
            LblSesion.Text = "INICIA SESIÓN CON EL NOMBRE DE TU CUENTA";
            LblSesion.Visible = false;
            // 
            // TxtPassword
            // 
            TxtPassword.BackColor = Color.FromArgb(66, 66, 66);
            TxtPassword.BorderStyle = BorderStyle.FixedSingle;
            TxtPassword.Font = new Font("Lucida Sans", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TxtPassword.ForeColor = SystemColors.ControlLightLight;
            TxtPassword.Location = new Point(45, 222);
            TxtPassword.Name = "TxtPassword";
            TxtPassword.PasswordChar = '♥';
            TxtPassword.Size = new Size(362, 26);
            TxtPassword.TabIndex = 93;
            TxtPassword.Visible = false;
            TxtPassword.KeyDown += TxtPassword_KeyDown;
            // 
            // TxtUser
            // 
            TxtUser.BackColor = Color.FromArgb(66, 66, 66);
            TxtUser.BorderStyle = BorderStyle.FixedSingle;
            TxtUser.Font = new Font("Lucida Sans", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TxtUser.ForeColor = SystemColors.ControlLightLight;
            TxtUser.Location = new Point(45, 169);
            TxtUser.Name = "TxtUser";
            TxtUser.Size = new Size(362, 26);
            TxtUser.TabIndex = 92;
            TxtUser.Visible = false;
            // 
            // PctTittle
            // 
            PctTittle.BackgroundImage = (Image)resources.GetObject("PctTittle.BackgroundImage");
            PctTittle.BackgroundImageLayout = ImageLayout.Stretch;
            PctTittle.Image = Properties.Resources.ViewStockTittle;
            PctTittle.Location = new Point(24, 31);
            PctTittle.Name = "PctTittle";
            PctTittle.Size = new Size(395, 83);
            PctTittle.SizeMode = PictureBoxSizeMode.StretchImage;
            PctTittle.TabIndex = 91;
            PctTittle.TabStop = false;
            PctTittle.Visible = false;
            PctTittle.Click += PctTittle_Click;
            // 
            // PctLoading
            // 
            PctLoading.Image = Properties.Resources.CargandoVS;
            PctLoading.Location = new Point(-2, -17);
            PctLoading.Name = "PctLoading";
            PctLoading.Size = new Size(445, 455);
            PctLoading.SizeMode = PictureBoxSizeMode.StretchImage;
            PctLoading.TabIndex = 101;
            PctLoading.TabStop = false;
            // 
            // SplashView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(38, 38, 38);
            ClientSize = new Size(440, 421);
            Controls.Add(PctLoading);
            Controls.Add(LblMessage);
            Controls.Add(BtnIniciarSesion);
            Controls.Add(LblContraseña);
            Controls.Add(LblSesion);
            Controls.Add(TxtPassword);
            Controls.Add(TxtUser);
            Controls.Add(PctTittle);
            Controls.Add(ProBarSplash);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "SplashView";
            StartPosition = FormStartPosition.CenterScreen;
            Activated += SplashView_Activated;
            Load += SplashView_Load;
            ((System.ComponentModel.ISupportInitialize)PctTittle).EndInit();
            ((System.ComponentModel.ISupportInitialize)PctLoading).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer TmrTiempo;
        private ProgressBar ProBarSplash;
        private Label LblMessage;
        private Button BtnIniciarSesion;
        private Label LblContraseña;
        private Label LblSesion;
        private TextBox TxtPassword;
        private TextBox TxtUser;
        private PictureBox PctTittle;
        private PictureBox PctLoading;
    }
}