namespace ViewStockNew.Views
{
    partial class CreateUserView
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
            IcoBtnInfo = new FontAwesome.Sharp.IconButton();
            label15 = new Label();
            pictureBox6 = new PictureBox();
            BtnEliminarImagen = new FontAwesome.Sharp.IconButton();
            BtnSubirImagen = new FontAwesome.Sharp.IconButton();
            PctImagen = new PictureBox();
            ComboGenero = new ComboBox();
            label5 = new Label();
            ComboTipoDeUsuario = new ComboBox();
            label4 = new Label();
            TxtContraseña = new TextBox();
            label3 = new Label();
            TxtUsuario = new TextBox();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            label9 = new Label();
            TxtNombreApellido = new TextBox();
            label7 = new Label();
            LblTittle = new Label();
            pictureBox4 = new PictureBox();
            TxtRepeatPassword = new TextBox();
            label2 = new Label();
            BtnGuardar = new FontAwesome.Sharp.IconButton();
            BtnCancelar = new FontAwesome.Sharp.IconButton();
            BtnContinuar = new FontAwesome.Sharp.IconButton();
            label16 = new Label();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PctImagen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // IcoBtnInfo
            // 
            IcoBtnInfo.BackColor = Color.Transparent;
            IcoBtnInfo.Cursor = Cursors.Hand;
            IcoBtnInfo.FlatAppearance.BorderSize = 0;
            IcoBtnInfo.FlatAppearance.MouseDownBackColor = Color.Transparent;
            IcoBtnInfo.FlatAppearance.MouseOverBackColor = Color.Transparent;
            IcoBtnInfo.FlatStyle = FlatStyle.Flat;
            IcoBtnInfo.ForeColor = SystemColors.ActiveCaptionText;
            IcoBtnInfo.IconChar = FontAwesome.Sharp.IconChar.Info;
            IcoBtnInfo.IconColor = SystemColors.ButtonFace;
            IcoBtnInfo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            IcoBtnInfo.IconSize = 22;
            IcoBtnInfo.Location = new Point(647, 22);
            IcoBtnInfo.Name = "IcoBtnInfo";
            IcoBtnInfo.Size = new Size(35, 28);
            IcoBtnInfo.TabIndex = 134;
            IcoBtnInfo.UseVisualStyleBackColor = false;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Lucida Sans", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label15.ForeColor = Color.Gold;
            label15.Location = new Point(480, 82);
            label15.Name = "label15";
            label15.Size = new Size(85, 22);
            label15.TabIndex = 133;
            label15.Text = "Imagen";
            // 
            // pictureBox6
            // 
            pictureBox6.Image = Properties.Resources.OrangeDivider;
            pictureBox6.Location = new Point(480, 107);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(202, 18);
            pictureBox6.TabIndex = 132;
            pictureBox6.TabStop = false;
            // 
            // BtnEliminarImagen
            // 
            BtnEliminarImagen.BackColor = Color.DarkOrange;
            BtnEliminarImagen.Cursor = Cursors.Hand;
            BtnEliminarImagen.FlatAppearance.BorderSize = 0;
            BtnEliminarImagen.FlatAppearance.MouseDownBackColor = Color.Goldenrod;
            BtnEliminarImagen.FlatAppearance.MouseOverBackColor = Color.Gold;
            BtnEliminarImagen.FlatStyle = FlatStyle.Flat;
            BtnEliminarImagen.Font = new Font("Lucida Sans", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            BtnEliminarImagen.ForeColor = SystemColors.ActiveCaptionText;
            BtnEliminarImagen.IconChar = FontAwesome.Sharp.IconChar.None;
            BtnEliminarImagen.IconColor = Color.Black;
            BtnEliminarImagen.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnEliminarImagen.IconSize = 30;
            BtnEliminarImagen.Location = new Point(587, 370);
            BtnEliminarImagen.Name = "BtnEliminarImagen";
            BtnEliminarImagen.Size = new Size(95, 25);
            BtnEliminarImagen.TabIndex = 131;
            BtnEliminarImagen.Text = "Eliminar Imagen";
            BtnEliminarImagen.UseVisualStyleBackColor = false;
            BtnEliminarImagen.Click += BtnEliminarImagen_Click;
            // 
            // BtnSubirImagen
            // 
            BtnSubirImagen.BackColor = Color.DarkOrange;
            BtnSubirImagen.Cursor = Cursors.Hand;
            BtnSubirImagen.FlatAppearance.BorderSize = 0;
            BtnSubirImagen.FlatAppearance.MouseDownBackColor = Color.Goldenrod;
            BtnSubirImagen.FlatAppearance.MouseOverBackColor = Color.Gold;
            BtnSubirImagen.FlatStyle = FlatStyle.Flat;
            BtnSubirImagen.Font = new Font("Lucida Sans", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            BtnSubirImagen.ForeColor = SystemColors.ActiveCaptionText;
            BtnSubirImagen.IconChar = FontAwesome.Sharp.IconChar.None;
            BtnSubirImagen.IconColor = Color.Black;
            BtnSubirImagen.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnSubirImagen.IconSize = 30;
            BtnSubirImagen.Location = new Point(480, 370);
            BtnSubirImagen.Name = "BtnSubirImagen";
            BtnSubirImagen.Size = new Size(95, 25);
            BtnSubirImagen.TabIndex = 130;
            BtnSubirImagen.Text = "Subir Imagen";
            BtnSubirImagen.UseVisualStyleBackColor = false;
            BtnSubirImagen.Click += BtnSubirImagen_Click;
            // 
            // PctImagen
            // 
            PctImagen.BackColor = Color.FromArgb(50, 50, 50);
            PctImagen.Image = Properties.Resources.Caca__1_;
            PctImagen.Location = new Point(480, 134);
            PctImagen.Name = "PctImagen";
            PctImagen.Size = new Size(202, 200);
            PctImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            PctImagen.TabIndex = 129;
            PctImagen.TabStop = false;
            // 
            // ComboGenero
            // 
            ComboGenero.BackColor = Color.FromArgb(55, 55, 55);
            ComboGenero.Cursor = Cursors.Hand;
            ComboGenero.FlatStyle = FlatStyle.Flat;
            ComboGenero.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            ComboGenero.ForeColor = SystemColors.ButtonFace;
            ComboGenero.FormattingEnabled = true;
            ComboGenero.Items.AddRange(new object[] { "Masculino", "Femenino", "Otro" });
            ComboGenero.Location = new Point(242, 370);
            ComboGenero.Name = "ComboGenero";
            ComboGenero.Size = new Size(194, 26);
            ComboGenero.TabIndex = 128;
            ComboGenero.SelectedIndexChanged += ComboGenero_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = SystemColors.ButtonFace;
            label5.Location = new Point(242, 349);
            label5.Name = "label5";
            label5.Size = new Size(71, 18);
            label5.TabIndex = 127;
            label5.Text = "Género:";
            // 
            // ComboTipoDeUsuario
            // 
            ComboTipoDeUsuario.BackColor = Color.FromArgb(55, 55, 55);
            ComboTipoDeUsuario.Cursor = Cursors.Hand;
            ComboTipoDeUsuario.FlatStyle = FlatStyle.Flat;
            ComboTipoDeUsuario.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            ComboTipoDeUsuario.ForeColor = SystemColors.ButtonFace;
            ComboTipoDeUsuario.FormattingEnabled = true;
            ComboTipoDeUsuario.Location = new Point(27, 370);
            ComboTipoDeUsuario.Name = "ComboTipoDeUsuario";
            ComboTipoDeUsuario.Size = new Size(194, 26);
            ComboTipoDeUsuario.TabIndex = 126;
            ComboTipoDeUsuario.SelectedIndexChanged += ComboTipoDeUsuario_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.ButtonFace;
            label4.Location = new Point(27, 349);
            label4.Name = "label4";
            label4.Size = new Size(140, 18);
            label4.TabIndex = 125;
            label4.Text = "Tipo de Usuario:";
            // 
            // TxtContraseña
            // 
            TxtContraseña.BackColor = Color.FromArgb(55, 55, 55);
            TxtContraseña.BorderStyle = BorderStyle.None;
            TxtContraseña.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            TxtContraseña.ForeColor = SystemColors.ButtonFace;
            TxtContraseña.Location = new Point(27, 264);
            TxtContraseña.Name = "TxtContraseña";
            TxtContraseña.PasswordChar = '♥';
            TxtContraseña.Size = new Size(409, 19);
            TxtContraseña.TabIndex = 124;
            TxtContraseña.TextChanged += TxtContraseña_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ButtonFace;
            label3.Location = new Point(27, 243);
            label3.Name = "label3";
            label3.Size = new Size(106, 18);
            label3.TabIndex = 123;
            label3.Text = "Contraseña:";
            // 
            // TxtUsuario
            // 
            TxtUsuario.BackColor = Color.FromArgb(55, 55, 55);
            TxtUsuario.BorderStyle = BorderStyle.None;
            TxtUsuario.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            TxtUsuario.ForeColor = SystemColors.ButtonFace;
            TxtUsuario.Location = new Point(27, 207);
            TxtUsuario.Name = "TxtUsuario";
            TxtUsuario.Size = new Size(409, 19);
            TxtUsuario.TabIndex = 122;
            TxtUsuario.TextChanged += TxtUsuario_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(27, 186);
            label1.Name = "label1";
            label1.Size = new Size(169, 18);
            label1.TabIndex = 121;
            label1.Text = "Nombre de Usuario:";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.OrangeDivider;
            pictureBox1.Location = new Point(27, 105);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(409, 18);
            pictureBox1.TabIndex = 120;
            pictureBox1.TabStop = false;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Lucida Sans", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label9.ForeColor = Color.Gold;
            label9.Location = new Point(27, 80);
            label9.Name = "label9";
            label9.Size = new Size(68, 22);
            label9.TabIndex = 119;
            label9.Text = "Datos";
            // 
            // TxtNombreApellido
            // 
            TxtNombreApellido.BackColor = Color.FromArgb(55, 55, 55);
            TxtNombreApellido.BorderStyle = BorderStyle.None;
            TxtNombreApellido.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            TxtNombreApellido.ForeColor = SystemColors.ButtonFace;
            TxtNombreApellido.Location = new Point(27, 153);
            TxtNombreApellido.Name = "TxtNombreApellido";
            TxtNombreApellido.Size = new Size(409, 19);
            TxtNombreApellido.TabIndex = 118;
            TxtNombreApellido.TextChanged += TxtNombreApellido_TextChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = SystemColors.ButtonFace;
            label7.Location = new Point(27, 132);
            label7.Name = "label7";
            label7.Size = new Size(159, 18);
            label7.TabIndex = 117;
            label7.Text = "Nombre y Apellido:";
            // 
            // LblTittle
            // 
            LblTittle.AutoSize = true;
            LblTittle.Font = new Font("Lucida Sans", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            LblTittle.ForeColor = Color.Gold;
            LblTittle.Location = new Point(27, 22);
            LblTittle.Name = "LblTittle";
            LblTittle.Size = new Size(234, 28);
            LblTittle.TabIndex = 116;
            LblTittle.Text = "Creando Usuario...";
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.OrangeDivider;
            pictureBox4.Location = new Point(27, 52);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(655, 25);
            pictureBox4.TabIndex = 115;
            pictureBox4.TabStop = false;
            // 
            // TxtRepeatPassword
            // 
            TxtRepeatPassword.BackColor = Color.FromArgb(55, 55, 55);
            TxtRepeatPassword.BorderStyle = BorderStyle.None;
            TxtRepeatPassword.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            TxtRepeatPassword.ForeColor = SystemColors.ButtonFace;
            TxtRepeatPassword.Location = new Point(27, 315);
            TxtRepeatPassword.Name = "TxtRepeatPassword";
            TxtRepeatPassword.PasswordChar = '♥';
            TxtRepeatPassword.Size = new Size(409, 19);
            TxtRepeatPassword.TabIndex = 136;
            TxtRepeatPassword.TextChanged += TxtRepeatPassword_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ButtonFace;
            label2.Location = new Point(27, 294);
            label2.Name = "label2";
            label2.Size = new Size(183, 18);
            label2.TabIndex = 135;
            label2.Text = "Repite la Contraseña:";
            // 
            // BtnGuardar
            // 
            BtnGuardar.BackColor = Color.LimeGreen;
            BtnGuardar.Cursor = Cursors.Hand;
            BtnGuardar.FlatAppearance.BorderSize = 0;
            BtnGuardar.FlatAppearance.MouseDownBackColor = Color.DarkGreen;
            BtnGuardar.FlatAppearance.MouseOverBackColor = Color.Green;
            BtnGuardar.FlatStyle = FlatStyle.Flat;
            BtnGuardar.ForeColor = SystemColors.ActiveCaptionText;
            BtnGuardar.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            BtnGuardar.IconColor = Color.Black;
            BtnGuardar.IconFont = FontAwesome.Sharp.IconFont.Solid;
            BtnGuardar.IconSize = 35;
            BtnGuardar.Location = new Point(548, 475);
            BtnGuardar.Name = "BtnGuardar";
            BtnGuardar.Size = new Size(63, 50);
            BtnGuardar.TabIndex = 139;
            BtnGuardar.UseVisualStyleBackColor = false;
            BtnGuardar.Click += BtnGuardar_Click;
            // 
            // BtnCancelar
            // 
            BtnCancelar.BackColor = Color.Red;
            BtnCancelar.Cursor = Cursors.Hand;
            BtnCancelar.FlatAppearance.BorderSize = 0;
            BtnCancelar.FlatAppearance.MouseDownBackColor = Color.DarkRed;
            BtnCancelar.FlatAppearance.MouseOverBackColor = Color.Firebrick;
            BtnCancelar.FlatStyle = FlatStyle.Flat;
            BtnCancelar.ForeColor = SystemColors.ActiveCaptionText;
            BtnCancelar.IconChar = FontAwesome.Sharp.IconChar.Backspace;
            BtnCancelar.IconColor = Color.Black;
            BtnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnCancelar.IconSize = 35;
            BtnCancelar.Location = new Point(27, 475);
            BtnCancelar.Name = "BtnCancelar";
            BtnCancelar.Size = new Size(63, 50);
            BtnCancelar.TabIndex = 138;
            BtnCancelar.UseVisualStyleBackColor = false;
            BtnCancelar.Click += BtnCancelar_Click;
            // 
            // BtnContinuar
            // 
            BtnContinuar.BackColor = Color.DarkOrange;
            BtnContinuar.Cursor = Cursors.Hand;
            BtnContinuar.FlatAppearance.BorderSize = 0;
            BtnContinuar.FlatAppearance.MouseDownBackColor = Color.Goldenrod;
            BtnContinuar.FlatAppearance.MouseOverBackColor = Color.Gold;
            BtnContinuar.FlatStyle = FlatStyle.Flat;
            BtnContinuar.ForeColor = SystemColors.ActiveCaptionText;
            BtnContinuar.IconChar = FontAwesome.Sharp.IconChar.CircleArrowRight;
            BtnContinuar.IconColor = Color.Black;
            BtnContinuar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnContinuar.IconSize = 35;
            BtnContinuar.Location = new Point(617, 475);
            BtnContinuar.Name = "BtnContinuar";
            BtnContinuar.Size = new Size(63, 50);
            BtnContinuar.TabIndex = 137;
            BtnContinuar.UseVisualStyleBackColor = false;
            BtnContinuar.Click += BtnContinuar_Click;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Lucida Sans", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label16.ForeColor = Color.Gold;
            label16.Location = new Point(27, 429);
            label16.Name = "label16";
            label16.Size = new Size(97, 22);
            label16.TabIndex = 146;
            label16.Text = "Acciones";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.OrangeDivider;
            pictureBox2.Location = new Point(25, 454);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(655, 10);
            pictureBox2.TabIndex = 145;
            pictureBox2.TabStop = false;
            // 
            // CreateUserView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(38, 38, 38);
            ClientSize = new Size(711, 537);
            Controls.Add(label16);
            Controls.Add(pictureBox2);
            Controls.Add(BtnGuardar);
            Controls.Add(BtnCancelar);
            Controls.Add(BtnContinuar);
            Controls.Add(TxtRepeatPassword);
            Controls.Add(label2);
            Controls.Add(IcoBtnInfo);
            Controls.Add(label15);
            Controls.Add(pictureBox6);
            Controls.Add(BtnEliminarImagen);
            Controls.Add(BtnSubirImagen);
            Controls.Add(PctImagen);
            Controls.Add(ComboGenero);
            Controls.Add(label5);
            Controls.Add(ComboTipoDeUsuario);
            Controls.Add(label4);
            Controls.Add(TxtContraseña);
            Controls.Add(label3);
            Controls.Add(TxtUsuario);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(label9);
            Controls.Add(TxtNombreApellido);
            Controls.Add(label7);
            Controls.Add(LblTittle);
            Controls.Add(pictureBox4);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "CreateUserView";
            StartPosition = FormStartPosition.CenterScreen;
            Load += CreateUserView_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)PctImagen).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FontAwesome.Sharp.IconButton IcoBtnInfo;
        private Label label15;
        private PictureBox pictureBox6;
        private FontAwesome.Sharp.IconButton BtnEliminarImagen;
        private FontAwesome.Sharp.IconButton BtnSubirImagen;
        private PictureBox PctImagen;
        private ComboBox ComboGenero;
        private Label label5;
        private ComboBox ComboTipoDeUsuario;
        private Label label4;
        private TextBox TxtContraseña;
        private Label label3;
        private TextBox TxtUsuario;
        private Label label1;
        private PictureBox pictureBox1;
        private Label label9;
        private TextBox TxtNombreApellido;
        private Label label7;
        private Label LblTittle;
        private PictureBox pictureBox4;
        private TextBox TxtRepeatPassword;
        private Label label2;
        private FontAwesome.Sharp.IconButton BtnGuardar;
        private FontAwesome.Sharp.IconButton BtnCancelar;
        private FontAwesome.Sharp.IconButton BtnContinuar;
        private Label label16;
        private PictureBox pictureBox2;
    }
}