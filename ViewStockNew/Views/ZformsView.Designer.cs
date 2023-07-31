namespace ViewStockNew.Views
{
    partial class ZformsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZformsView));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            pictureBox2 = new PictureBox();
            richTextBox1 = new RichTextBox();
            textBox11 = new TextBox();
            BtnAgregarTipo = new FontAwesome.Sharp.IconButton();
            GridDetalles = new DataGridView();
            iconButton8 = new FontAwesome.Sharp.IconButton();
            iconButton7 = new FontAwesome.Sharp.IconButton();
            label18 = new Label();
            textBox6 = new TextBox();
            label9 = new Label();
            textBox5 = new TextBox();
            label4 = new Label();
            textBox2 = new TextBox();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GridDetalles).BeginInit();
            SuspendLayout();
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImage = (Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.Location = new Point(173, 70);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(420, 81);
            pictureBox2.TabIndex = 136;
            pictureBox2.TabStop = false;
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = Color.FromArgb(38, 38, 38);
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.ForeColor = SystemColors.ControlLight;
            richTextBox1.Location = new Point(352, 188);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(110, 53);
            richTextBox1.TabIndex = 138;
            richTextBox1.Text = "O bien utilizando un código QR desde tu aplicación";
            // 
            // textBox11
            // 
            textBox11.BackColor = Color.FromArgb(55, 55, 55);
            textBox11.BorderStyle = BorderStyle.None;
            textBox11.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            textBox11.ForeColor = SystemColors.ButtonFace;
            textBox11.Location = new Point(79, 247);
            textBox11.Name = "textBox11";
            textBox11.Size = new Size(153, 19);
            textBox11.TabIndex = 140;
            // 
            // BtnAgregarTipo
            // 
            BtnAgregarTipo.BackColor = Color.DarkOrange;
            BtnAgregarTipo.Cursor = Cursors.Hand;
            BtnAgregarTipo.FlatAppearance.BorderSize = 0;
            BtnAgregarTipo.FlatAppearance.MouseDownBackColor = Color.Goldenrod;
            BtnAgregarTipo.FlatAppearance.MouseOverBackColor = Color.Gold;
            BtnAgregarTipo.FlatStyle = FlatStyle.Flat;
            BtnAgregarTipo.ForeColor = SystemColors.ActiveCaptionText;
            BtnAgregarTipo.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            BtnAgregarTipo.IconColor = Color.Black;
            BtnAgregarTipo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnAgregarTipo.IconSize = 30;
            BtnAgregarTipo.Location = new Point(238, 242);
            BtnAgregarTipo.Name = "BtnAgregarTipo";
            BtnAgregarTipo.Size = new Size(35, 28);
            BtnAgregarTipo.TabIndex = 139;
            BtnAgregarTipo.UseVisualStyleBackColor = false;
            // 
            // GridDetalles
            // 
            GridDetalles.AllowUserToAddRows = false;
            GridDetalles.AllowUserToDeleteRows = false;
            GridDetalles.AllowUserToResizeColumns = false;
            GridDetalles.AllowUserToResizeRows = false;
            GridDetalles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            GridDetalles.BackgroundColor = Color.FromArgb(50, 50, 50);
            GridDetalles.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Arial", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            GridDetalles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            GridDetalles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            GridDetalles.Location = new Point(90, 41);
            GridDetalles.Name = "GridDetalles";
            GridDetalles.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(50, 50, 50);
            dataGridViewCellStyle2.Font = new Font("Lucida Sans", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            GridDetalles.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(50, 50, 50);
            dataGridViewCellStyle3.Font = new Font("Lucida Sans", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.White;
            GridDetalles.RowsDefaultCellStyle = dataGridViewCellStyle3;
            GridDetalles.RowTemplate.Height = 25;
            GridDetalles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridDetalles.Size = new Size(413, 397);
            GridDetalles.TabIndex = 196;
            // 
            // iconButton8
            // 
            iconButton8.BackColor = Color.DarkOrange;
            iconButton8.Cursor = Cursors.Hand;
            iconButton8.FlatAppearance.BorderSize = 0;
            iconButton8.FlatAppearance.MouseDownBackColor = Color.Goldenrod;
            iconButton8.FlatAppearance.MouseOverBackColor = Color.Gold;
            iconButton8.FlatStyle = FlatStyle.Flat;
            iconButton8.ForeColor = SystemColors.ActiveCaptionText;
            iconButton8.IconChar = FontAwesome.Sharp.IconChar.Calculator;
            iconButton8.IconColor = Color.Black;
            iconButton8.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton8.IconSize = 25;
            iconButton8.Location = new Point(748, 208);
            iconButton8.Name = "iconButton8";
            iconButton8.Size = new Size(26, 22);
            iconButton8.TabIndex = 313;
            iconButton8.UseVisualStyleBackColor = false;
            // 
            // iconButton7
            // 
            iconButton7.BackColor = Color.DarkOrange;
            iconButton7.Cursor = Cursors.Hand;
            iconButton7.FlatAppearance.BorderSize = 0;
            iconButton7.FlatAppearance.MouseDownBackColor = Color.Goldenrod;
            iconButton7.FlatAppearance.MouseOverBackColor = Color.Gold;
            iconButton7.FlatStyle = FlatStyle.Flat;
            iconButton7.ForeColor = SystemColors.ActiveCaptionText;
            iconButton7.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            iconButton7.IconColor = Color.Black;
            iconButton7.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton7.IconSize = 25;
            iconButton7.Location = new Point(748, 303);
            iconButton7.Name = "iconButton7";
            iconButton7.Size = new Size(26, 22);
            iconButton7.TabIndex = 312;
            iconButton7.UseVisualStyleBackColor = false;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Lucida Sans", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label18.ForeColor = SystemColors.ControlLightLight;
            label18.Location = new Point(509, 185);
            label18.Name = "label18";
            label18.Size = new Size(90, 22);
            label18.TabIndex = 311;
            label18.Text = "Importe";
            // 
            // textBox6
            // 
            textBox6.BackColor = Color.FromArgb(55, 55, 55);
            textBox6.BorderStyle = BorderStyle.None;
            textBox6.Font = new Font("Lucida Sans", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox6.ForeColor = SystemColors.ButtonFace;
            textBox6.Location = new Point(509, 209);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(233, 19);
            textBox6.TabIndex = 310;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Lucida Sans", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label9.ForeColor = SystemColors.ControlLightLight;
            label9.Location = new Point(509, 282);
            label9.Name = "label9";
            label9.Size = new Size(75, 22);
            label9.TabIndex = 309;
            label9.Text = "Vuelto";
            // 
            // textBox5
            // 
            textBox5.BackColor = Color.FromArgb(55, 55, 55);
            textBox5.BorderStyle = BorderStyle.None;
            textBox5.Font = new Font("Lucida Sans", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox5.ForeColor = SystemColors.ButtonFace;
            textBox5.Location = new Point(509, 306);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(233, 19);
            textBox5.TabIndex = 308;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Lucida Sans", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.ControlLightLight;
            label4.Location = new Point(509, 236);
            label4.Name = "label4";
            label4.Size = new Size(78, 22);
            label4.TabIndex = 307;
            label4.Text = "Dinero";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(55, 55, 55);
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Lucida Sans", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox2.ForeColor = SystemColors.ButtonFace;
            textBox2.Location = new Point(509, 260);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(233, 19);
            textBox2.TabIndex = 306;
            // 
            // iconButton2
            // 
            iconButton2.BackColor = Color.DarkOrange;
            iconButton2.Cursor = Cursors.Hand;
            iconButton2.FlatAppearance.BorderSize = 0;
            iconButton2.FlatAppearance.MouseDownBackColor = Color.Goldenrod;
            iconButton2.FlatAppearance.MouseOverBackColor = Color.Gold;
            iconButton2.FlatStyle = FlatStyle.Flat;
            iconButton2.ForeColor = SystemColors.ActiveCaptionText;
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.Calculator;
            iconButton2.IconColor = Color.Black;
            iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton2.IconSize = 25;
            iconButton2.Location = new Point(748, 259);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(26, 22);
            iconButton2.TabIndex = 305;
            iconButton2.UseVisualStyleBackColor = false;
            // 
            // ZformsView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(38, 38, 38);
            ClientSize = new Size(800, 450);
            Controls.Add(iconButton8);
            Controls.Add(iconButton7);
            Controls.Add(label18);
            Controls.Add(textBox6);
            Controls.Add(label9);
            Controls.Add(textBox5);
            Controls.Add(label4);
            Controls.Add(textBox2);
            Controls.Add(iconButton2);
            Controls.Add(GridDetalles);
            Controls.Add(textBox11);
            Controls.Add(BtnAgregarTipo);
            Controls.Add(richTextBox1);
            Controls.Add(pictureBox2);
            Name = "ZformsView";
            Text = "ZformsView";
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)GridDetalles).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox2;
        private RichTextBox richTextBox1;
        private TextBox textBox11;
        private FontAwesome.Sharp.IconButton BtnAgregarTipo;
        private DataGridView GridDetalles;
        private FontAwesome.Sharp.IconButton iconButton8;
        private FontAwesome.Sharp.IconButton iconButton7;
        private Label label18;
        private TextBox textBox6;
        private Label label9;
        private TextBox textBox5;
        private Label label4;
        private TextBox textBox2;
        private FontAwesome.Sharp.IconButton iconButton2;
    }
}