namespace ViewStockNew.Views
{
    partial class CreateProveedorView
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
            pictureBox4 = new PictureBox();
            label9 = new Label();
            pictureBox10 = new PictureBox();
            LblTittle = new Label();
            TxtNombre = new TextBox();
            label13 = new Label();
            TxtTelefono = new TextBox();
            label1 = new Label();
            TxtEmail = new TextBox();
            label6 = new Label();
            TxtDireccion = new TextBox();
            label2 = new Label();
            label16 = new Label();
            BtnAgregarLocalidad = new FontAwesome.Sharp.IconButton();
            BtnAgregarProvincia = new FontAwesome.Sharp.IconButton();
            label7 = new Label();
            label8 = new Label();
            ComboLocalidad = new ComboBox();
            ComboProvincia = new ComboBox();
            label15 = new Label();
            pictureBox2 = new PictureBox();
            PctImagen = new PictureBox();
            BtnEliminarImagen = new FontAwesome.Sharp.IconButton();
            BtnSubirImagen = new FontAwesome.Sharp.IconButton();
            pictureBox5 = new PictureBox();
            BtnGuardar = new FontAwesome.Sharp.IconButton();
            BtnCancelar = new FontAwesome.Sharp.IconButton();
            BtnContinuar = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PctImagen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
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
            IcoBtnInfo.Location = new Point(697, 35);
            IcoBtnInfo.Name = "IcoBtnInfo";
            IcoBtnInfo.Size = new Size(35, 28);
            IcoBtnInfo.TabIndex = 107;
            IcoBtnInfo.UseVisualStyleBackColor = false;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.OrangeDivider;
            pictureBox4.Location = new Point(26, 114);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(480, 10);
            pictureBox4.TabIndex = 106;
            pictureBox4.TabStop = false;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Lucida Sans", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label9.ForeColor = Color.Gold;
            label9.Location = new Point(26, 89);
            label9.Name = "label9";
            label9.Size = new Size(68, 22);
            label9.TabIndex = 105;
            label9.Text = "Datos";
            // 
            // pictureBox10
            // 
            pictureBox10.Image = Properties.Resources.OrangeDivider;
            pictureBox10.Location = new Point(25, 61);
            pictureBox10.Name = "pictureBox10";
            pictureBox10.Size = new Size(707, 10);
            pictureBox10.TabIndex = 104;
            pictureBox10.TabStop = false;
            // 
            // LblTittle
            // 
            LblTittle.AutoSize = true;
            LblTittle.Font = new Font("Lucida Sans", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            LblTittle.ForeColor = Color.Gold;
            LblTittle.Location = new Point(25, 31);
            LblTittle.Name = "LblTittle";
            LblTittle.Size = new Size(246, 28);
            LblTittle.TabIndex = 103;
            LblTittle.Text = "Creando Proveedor";
            // 
            // TxtNombre
            // 
            TxtNombre.BackColor = Color.FromArgb(55, 55, 55);
            TxtNombre.BorderStyle = BorderStyle.None;
            TxtNombre.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            TxtNombre.ForeColor = SystemColors.ButtonFace;
            TxtNombre.Location = new Point(25, 188);
            TxtNombre.Name = "TxtNombre";
            TxtNombre.Size = new Size(225, 19);
            TxtNombre.TabIndex = 116;
            TxtNombre.TextChanged += TxtNombre_TextChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label13.ForeColor = SystemColors.ButtonFace;
            label13.Location = new Point(25, 167);
            label13.Name = "label13";
            label13.Size = new Size(76, 18);
            label13.TabIndex = 115;
            label13.Text = "Nombre:";
            // 
            // TxtTelefono
            // 
            TxtTelefono.BackColor = Color.FromArgb(55, 55, 55);
            TxtTelefono.BorderStyle = BorderStyle.None;
            TxtTelefono.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            TxtTelefono.ForeColor = SystemColors.ButtonFace;
            TxtTelefono.Location = new Point(279, 188);
            TxtTelefono.Name = "TxtTelefono";
            TxtTelefono.Size = new Size(225, 19);
            TxtTelefono.TabIndex = 118;
            TxtTelefono.TextChanged += TxtTelefono_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(279, 167);
            label1.Name = "label1";
            label1.Size = new Size(84, 18);
            label1.TabIndex = 117;
            label1.Text = "Teléfono:";
            // 
            // TxtEmail
            // 
            TxtEmail.BackColor = Color.FromArgb(55, 55, 55);
            TxtEmail.BorderStyle = BorderStyle.None;
            TxtEmail.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            TxtEmail.ForeColor = SystemColors.ButtonFace;
            TxtEmail.Location = new Point(279, 239);
            TxtEmail.Name = "TxtEmail";
            TxtEmail.Size = new Size(225, 19);
            TxtEmail.TabIndex = 128;
            TxtEmail.TextChanged += TxtEmail_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = SystemColors.ButtonFace;
            label6.Location = new Point(279, 218);
            label6.Name = "label6";
            label6.Size = new Size(55, 18);
            label6.TabIndex = 127;
            label6.Text = "Email:";
            // 
            // TxtDireccion
            // 
            TxtDireccion.BackColor = Color.FromArgb(55, 55, 55);
            TxtDireccion.BorderStyle = BorderStyle.None;
            TxtDireccion.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            TxtDireccion.ForeColor = SystemColors.ButtonFace;
            TxtDireccion.Location = new Point(25, 239);
            TxtDireccion.Name = "TxtDireccion";
            TxtDireccion.Size = new Size(225, 19);
            TxtDireccion.TabIndex = 131;
            TxtDireccion.TextChanged += TxtDireccion_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ButtonFace;
            label2.Location = new Point(25, 218);
            label2.Name = "label2";
            label2.Size = new Size(88, 18);
            label2.TabIndex = 130;
            label2.Text = "Dirección:";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Lucida Sans", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label16.ForeColor = Color.Gold;
            label16.Location = new Point(26, 362);
            label16.Name = "label16";
            label16.Size = new Size(97, 22);
            label16.TabIndex = 153;
            label16.Text = "Acciones";
            // 
            // BtnAgregarLocalidad
            // 
            BtnAgregarLocalidad.BackColor = Color.DarkOrange;
            BtnAgregarLocalidad.Cursor = Cursors.Hand;
            BtnAgregarLocalidad.FlatAppearance.BorderSize = 0;
            BtnAgregarLocalidad.FlatAppearance.MouseDownBackColor = Color.Goldenrod;
            BtnAgregarLocalidad.FlatAppearance.MouseOverBackColor = Color.Gold;
            BtnAgregarLocalidad.FlatStyle = FlatStyle.Flat;
            BtnAgregarLocalidad.ForeColor = SystemColors.ActiveCaptionText;
            BtnAgregarLocalidad.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            BtnAgregarLocalidad.IconColor = Color.Black;
            BtnAgregarLocalidad.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnAgregarLocalidad.IconSize = 30;
            BtnAgregarLocalidad.Location = new Point(469, 312);
            BtnAgregarLocalidad.Name = "BtnAgregarLocalidad";
            BtnAgregarLocalidad.Size = new Size(35, 28);
            BtnAgregarLocalidad.TabIndex = 152;
            BtnAgregarLocalidad.UseVisualStyleBackColor = false;
            BtnAgregarLocalidad.Click += BtnAgregarLocalidad_Click;
            // 
            // BtnAgregarProvincia
            // 
            BtnAgregarProvincia.BackColor = Color.DarkOrange;
            BtnAgregarProvincia.Cursor = Cursors.Hand;
            BtnAgregarProvincia.FlatAppearance.BorderSize = 0;
            BtnAgregarProvincia.FlatAppearance.MouseDownBackColor = Color.Goldenrod;
            BtnAgregarProvincia.FlatAppearance.MouseOverBackColor = Color.Gold;
            BtnAgregarProvincia.FlatStyle = FlatStyle.Flat;
            BtnAgregarProvincia.ForeColor = SystemColors.ActiveCaptionText;
            BtnAgregarProvincia.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            BtnAgregarProvincia.IconColor = Color.Black;
            BtnAgregarProvincia.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnAgregarProvincia.IconSize = 30;
            BtnAgregarProvincia.Location = new Point(216, 312);
            BtnAgregarProvincia.Name = "BtnAgregarProvincia";
            BtnAgregarProvincia.Size = new Size(35, 28);
            BtnAgregarProvincia.TabIndex = 149;
            BtnAgregarProvincia.UseVisualStyleBackColor = false;
            BtnAgregarProvincia.Click += BtnAgregarProvincia_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = SystemColors.ButtonFace;
            label7.Location = new Point(279, 291);
            label7.Name = "label7";
            label7.Size = new Size(90, 18);
            label7.TabIndex = 148;
            label7.Text = "Localidad:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label8.ForeColor = SystemColors.ButtonFace;
            label8.Location = new Point(25, 292);
            label8.Name = "label8";
            label8.Size = new Size(86, 18);
            label8.TabIndex = 147;
            label8.Text = "Provincia:";
            // 
            // ComboLocalidad
            // 
            ComboLocalidad.BackColor = Color.FromArgb(55, 55, 55);
            ComboLocalidad.Cursor = Cursors.Hand;
            ComboLocalidad.FlatStyle = FlatStyle.Flat;
            ComboLocalidad.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            ComboLocalidad.ForeColor = SystemColors.ButtonFace;
            ComboLocalidad.FormattingEnabled = true;
            ComboLocalidad.Location = new Point(279, 312);
            ComboLocalidad.Name = "ComboLocalidad";
            ComboLocalidad.Size = new Size(184, 26);
            ComboLocalidad.TabIndex = 146;
            ComboLocalidad.SelectedIndexChanged += ComboLocalidad_SelectedIndexChanged;
            // 
            // ComboProvincia
            // 
            ComboProvincia.BackColor = Color.FromArgb(55, 55, 55);
            ComboProvincia.Cursor = Cursors.Hand;
            ComboProvincia.FlatStyle = FlatStyle.Flat;
            ComboProvincia.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            ComboProvincia.ForeColor = SystemColors.ButtonFace;
            ComboProvincia.FormattingEnabled = true;
            ComboProvincia.Location = new Point(25, 313);
            ComboProvincia.Name = "ComboProvincia";
            ComboProvincia.Size = new Size(185, 26);
            ComboProvincia.TabIndex = 145;
            ComboProvincia.SelectedIndexChanged += ComboProvincia_SelectedIndexChanged;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Lucida Sans", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label15.ForeColor = Color.Gold;
            label15.Location = new Point(540, 84);
            label15.Name = "label15";
            label15.Size = new Size(85, 22);
            label15.TabIndex = 156;
            label15.Text = "Imagen";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.OrangeDivider;
            pictureBox2.Location = new Point(540, 109);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(193, 15);
            pictureBox2.TabIndex = 155;
            pictureBox2.TabStop = false;
            // 
            // PctImagen
            // 
            PctImagen.BackColor = Color.FromArgb(50, 50, 50);
            PctImagen.Image = Properties.Resources.Caca__1_;
            PctImagen.Location = new Point(539, 130);
            PctImagen.Name = "PctImagen";
            PctImagen.Size = new Size(193, 176);
            PctImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            PctImagen.TabIndex = 154;
            PctImagen.TabStop = false;
            // 
            // BtnEliminarImagen
            // 
            BtnEliminarImagen.BackColor = Color.FromArgb(249, 43, 43);
            BtnEliminarImagen.Cursor = Cursors.Hand;
            BtnEliminarImagen.FlatAppearance.BorderSize = 0;
            BtnEliminarImagen.FlatAppearance.MouseDownBackColor = Color.Goldenrod;
            BtnEliminarImagen.FlatAppearance.MouseOverBackColor = Color.Gold;
            BtnEliminarImagen.FlatStyle = FlatStyle.Flat;
            BtnEliminarImagen.Font = new Font("Lucida Sans", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            BtnEliminarImagen.ForeColor = SystemColors.ControlLight;
            BtnEliminarImagen.IconChar = FontAwesome.Sharp.IconChar.None;
            BtnEliminarImagen.IconColor = Color.Black;
            BtnEliminarImagen.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnEliminarImagen.IconSize = 30;
            BtnEliminarImagen.Location = new Point(644, 312);
            BtnEliminarImagen.Name = "BtnEliminarImagen";
            BtnEliminarImagen.Size = new Size(89, 28);
            BtnEliminarImagen.TabIndex = 158;
            BtnEliminarImagen.Text = "Eliminar Imagen";
            BtnEliminarImagen.UseVisualStyleBackColor = false;
            BtnEliminarImagen.Click += BtnEliminarImagen_Click;
            // 
            // BtnSubirImagen
            // 
            BtnSubirImagen.BackColor = Color.FromArgb(13, 216, 71);
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
            BtnSubirImagen.Location = new Point(540, 312);
            BtnSubirImagen.Name = "BtnSubirImagen";
            BtnSubirImagen.Size = new Size(99, 28);
            BtnSubirImagen.TabIndex = 157;
            BtnSubirImagen.Text = "Subir Imagen";
            BtnSubirImagen.UseVisualStyleBackColor = false;
            BtnSubirImagen.Click += BtnSubirImagen_Click;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = Properties.Resources.OrangeDivider;
            pictureBox5.Location = new Point(26, 387);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(707, 10);
            pictureBox5.TabIndex = 159;
            pictureBox5.TabStop = false;
            // 
            // BtnGuardar
            // 
            BtnGuardar.BackColor = Color.FromArgb(13, 216, 71);
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
            BtnGuardar.Location = new Point(518, 403);
            BtnGuardar.Name = "BtnGuardar";
            BtnGuardar.Size = new Size(105, 40);
            BtnGuardar.TabIndex = 162;
            BtnGuardar.UseVisualStyleBackColor = false;
            BtnGuardar.Click += BtnGuardar_Click;
            // 
            // BtnCancelar
            // 
            BtnCancelar.BackColor = Color.FromArgb(249, 43, 43);
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
            BtnCancelar.Location = new Point(26, 403);
            BtnCancelar.Name = "BtnCancelar";
            BtnCancelar.Size = new Size(105, 40);
            BtnCancelar.TabIndex = 161;
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
            BtnContinuar.Location = new Point(629, 403);
            BtnContinuar.Name = "BtnContinuar";
            BtnContinuar.Size = new Size(103, 40);
            BtnContinuar.TabIndex = 160;
            BtnContinuar.UseVisualStyleBackColor = false;
            BtnContinuar.Click += BtnContinuar_Click;
            // 
            // CreateProveedorView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(38, 38, 38);
            ClientSize = new Size(758, 463);
            Controls.Add(BtnGuardar);
            Controls.Add(BtnCancelar);
            Controls.Add(BtnContinuar);
            Controls.Add(pictureBox5);
            Controls.Add(BtnEliminarImagen);
            Controls.Add(BtnSubirImagen);
            Controls.Add(label15);
            Controls.Add(pictureBox2);
            Controls.Add(PctImagen);
            Controls.Add(label16);
            Controls.Add(BtnAgregarLocalidad);
            Controls.Add(BtnAgregarProvincia);
            Controls.Add(label7);
            Controls.Add(label8);
            Controls.Add(ComboLocalidad);
            Controls.Add(ComboProvincia);
            Controls.Add(TxtDireccion);
            Controls.Add(label2);
            Controls.Add(TxtEmail);
            Controls.Add(label6);
            Controls.Add(TxtTelefono);
            Controls.Add(label1);
            Controls.Add(TxtNombre);
            Controls.Add(label13);
            Controls.Add(IcoBtnInfo);
            Controls.Add(pictureBox4);
            Controls.Add(label9);
            Controls.Add(pictureBox10);
            Controls.Add(LblTittle);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            Name = "CreateProveedorView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CreateProveedorView";
            Load += CreateProveedorView_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)PctImagen).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FontAwesome.Sharp.IconButton IcoBtnInfo;
        private PictureBox pictureBox4;
        private Label label9;
        private PictureBox pictureBox10;
        private Label LblTittle;
        private TextBox TxtNombre;
        private Label label13;
        private TextBox TxtTelefono;
        private Label label1;
        private TextBox TxtEmail;
        private Label label6;
        private TextBox TxtDireccion;
        private Label label2;
        private Label label16;
        private FontAwesome.Sharp.IconButton BtnAgregarLocalidad;
        private FontAwesome.Sharp.IconButton BtnAgregarProvincia;
        private Label label7;
        private Label label8;
        private ComboBox ComboLocalidad;
        private ComboBox ComboProvincia;
        private Label label15;
        private PictureBox pictureBox2;
        private PictureBox PctImagen;
        private FontAwesome.Sharp.IconButton BtnEliminarImagen;
        private FontAwesome.Sharp.IconButton BtnSubirImagen;
        private PictureBox pictureBox5;
        private FontAwesome.Sharp.IconButton BtnGuardar;
        private FontAwesome.Sharp.IconButton BtnCancelar;
        private FontAwesome.Sharp.IconButton BtnContinuar;
    }
}