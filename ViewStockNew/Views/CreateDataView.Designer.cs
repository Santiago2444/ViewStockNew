namespace ViewStockNew.Views
{
    partial class CreateDataView
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
            pictureBox10 = new PictureBox();
            LblTitle = new Label();
            TxtNombre = new TextBox();
            label13 = new Label();
            BtnGuardar = new FontAwesome.Sharp.IconButton();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).BeginInit();
            SuspendLayout();
            // 
            // pictureBox10
            // 
            pictureBox10.Image = Properties.Resources.OrangeDivider;
            pictureBox10.Location = new Point(33, 61);
            pictureBox10.Name = "pictureBox10";
            pictureBox10.Size = new Size(233, 10);
            pictureBox10.TabIndex = 78;
            pictureBox10.TabStop = false;
            // 
            // LblTitle
            // 
            LblTitle.AutoSize = true;
            LblTitle.Font = new Font("Lucida Sans", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            LblTitle.ForeColor = Color.Gold;
            LblTitle.Location = new Point(33, 30);
            LblTitle.Name = "LblTitle";
            LblTitle.Size = new Size(229, 28);
            LblTitle.TabIndex = 77;
            LblTitle.Text = "Creando Producto";
            // 
            // TxtNombre
            // 
            TxtNombre.BackColor = Color.FromArgb(55, 55, 55);
            TxtNombre.BorderStyle = BorderStyle.None;
            TxtNombre.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            TxtNombre.ForeColor = SystemColors.ButtonFace;
            TxtNombre.Location = new Point(33, 113);
            TxtNombre.Name = "TxtNombre";
            TxtNombre.Size = new Size(233, 19);
            TxtNombre.TabIndex = 116;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label13.ForeColor = SystemColors.ButtonFace;
            label13.Location = new Point(33, 92);
            label13.Name = "label13";
            label13.Size = new Size(76, 18);
            label13.TabIndex = 115;
            label13.Text = "Nombre:";
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
            BtnGuardar.Location = new Point(186, 148);
            BtnGuardar.Name = "BtnGuardar";
            BtnGuardar.Size = new Size(80, 40);
            BtnGuardar.TabIndex = 135;
            BtnGuardar.UseVisualStyleBackColor = false;
            BtnGuardar.Click += BtnGuardar_Click;
            // 
            // iconButton2
            // 
            iconButton2.BackColor = Color.FromArgb(249, 43, 43);
            iconButton2.Cursor = Cursors.Hand;
            iconButton2.FlatAppearance.BorderSize = 0;
            iconButton2.FlatAppearance.MouseDownBackColor = Color.DarkRed;
            iconButton2.FlatAppearance.MouseOverBackColor = Color.Firebrick;
            iconButton2.FlatStyle = FlatStyle.Flat;
            iconButton2.ForeColor = SystemColors.ActiveCaptionText;
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.Backspace;
            iconButton2.IconColor = Color.Black;
            iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton2.IconSize = 35;
            iconButton2.Location = new Point(33, 148);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(76, 40);
            iconButton2.TabIndex = 134;
            iconButton2.UseVisualStyleBackColor = false;
            iconButton2.Click += iconButton2_Click;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(288, 195);
            panel1.TabIndex = 136;
            // 
            // CreateDataView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(38, 38, 38);
            ClientSize = new Size(312, 219);
            Controls.Add(BtnGuardar);
            Controls.Add(iconButton2);
            Controls.Add(TxtNombre);
            Controls.Add(label13);
            Controls.Add(pictureBox10);
            Controls.Add(LblTitle);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CreateDataView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CreateDataView";
            ((System.ComponentModel.ISupportInitialize)pictureBox10).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox10;
        private Label LblTitle;
        private TextBox TxtNombre;
        private Label label13;
        private FontAwesome.Sharp.IconButton BtnGuardar;
        private FontAwesome.Sharp.IconButton iconButton2;
        private Panel panel1;
    }
}