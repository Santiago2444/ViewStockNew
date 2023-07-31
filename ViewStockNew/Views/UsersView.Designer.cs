namespace ViewStockNew.Views
{
    partial class UsersView
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
            pictureBox2 = new PictureBox();
            pictureBox9 = new PictureBox();
            label18 = new Label();
            pictureBox8 = new PictureBox();
            dataGridView1 = new DataGridView();
            BtnDis = new FontAwesome.Sharp.IconButton();
            label2 = new Label();
            BtnEna = new FontAwesome.Sharp.IconButton();
            label17 = new Label();
            BtnImprimir = new FontAwesome.Sharp.IconButton();
            TxtBuscar = new TextBox();
            BtnEliminar = new FontAwesome.Sharp.IconButton();
            iconButton3 = new FontAwesome.Sharp.IconButton();
            BtnEditar = new FontAwesome.Sharp.IconButton();
            CboTipo = new ComboBox();
            BtnNuevo = new FontAwesome.Sharp.IconButton();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            RadioEnabled = new RadioButton();
            dateTimePicker2 = new DateTimePicker();
            RadioDisabled = new RadioButton();
            dateTimePicker1 = new DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImage = Properties.Resources.LogoDesktopTwo2;
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.Location = new Point(648, 35);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(176, 127);
            pictureBox2.TabIndex = 100;
            pictureBox2.TabStop = false;
            // 
            // pictureBox9
            // 
            pictureBox9.Image = Properties.Resources.OrangeDivider;
            pictureBox9.Location = new Point(512, 45);
            pictureBox9.Name = "pictureBox9";
            pictureBox9.Size = new Size(97, 19);
            pictureBox9.TabIndex = 99;
            pictureBox9.TabStop = false;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Lucida Sans", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label18.ForeColor = Color.Gold;
            label18.Location = new Point(512, 20);
            label18.Name = "label18";
            label18.Size = new Size(97, 44);
            label18.TabIndex = 98;
            label18.Text = "Acciones\r\n\r\n";
            // 
            // pictureBox8
            // 
            pictureBox8.Image = Properties.Resources.OrangeDivider;
            pictureBox8.Location = new Point(21, 45);
            pictureBox8.Name = "pictureBox8";
            pictureBox8.Size = new Size(467, 19);
            pictureBox8.TabIndex = 90;
            pictureBox8.TabStop = false;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = Color.FromArgb(50, 50, 50);
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(22, 181);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(802, 398);
            dataGridView1.TabIndex = 91;
            // 
            // BtnDis
            // 
            BtnDis.BackColor = Color.Red;
            BtnDis.Cursor = Cursors.Hand;
            BtnDis.FlatAppearance.BorderSize = 0;
            BtnDis.FlatAppearance.MouseDownBackColor = Color.DarkRed;
            BtnDis.FlatAppearance.MouseOverBackColor = Color.Firebrick;
            BtnDis.FlatStyle = FlatStyle.Flat;
            BtnDis.ForeColor = SystemColors.ActiveCaptionText;
            BtnDis.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            BtnDis.IconColor = Color.Black;
            BtnDis.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnDis.IconSize = 30;
            BtnDis.Location = new Point(553, 99);
            BtnDis.Name = "BtnDis";
            BtnDis.Size = new Size(35, 28);
            BtnDis.TabIndex = 96;
            BtnDis.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ButtonFace;
            label2.Location = new Point(22, 69);
            label2.Name = "label2";
            label2.Size = new Size(67, 18);
            label2.TabIndex = 80;
            label2.Text = "Buscar:";
            // 
            // BtnEna
            // 
            BtnEna.BackColor = Color.LimeGreen;
            BtnEna.Cursor = Cursors.Hand;
            BtnEna.FlatAppearance.BorderSize = 0;
            BtnEna.FlatAppearance.MouseDownBackColor = Color.DarkGreen;
            BtnEna.FlatAppearance.MouseOverBackColor = Color.Green;
            BtnEna.FlatStyle = FlatStyle.Flat;
            BtnEna.ForeColor = SystemColors.ActiveCaptionText;
            BtnEna.IconChar = FontAwesome.Sharp.IconChar.Check;
            BtnEna.IconColor = Color.Black;
            BtnEna.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnEna.IconSize = 30;
            BtnEna.Location = new Point(553, 65);
            BtnEna.Name = "BtnEna";
            BtnEna.Size = new Size(35, 28);
            BtnEna.TabIndex = 97;
            BtnEna.UseVisualStyleBackColor = false;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Lucida Sans", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label17.ForeColor = Color.Gold;
            label17.Location = new Point(21, 20);
            label17.Name = "label17";
            label17.Size = new Size(259, 22);
            label17.TabIndex = 89;
            label17.Text = "Herramientas de filtrado";
            // 
            // BtnImprimir
            // 
            BtnImprimir.BackColor = Color.DarkOrange;
            BtnImprimir.Cursor = Cursors.Hand;
            BtnImprimir.FlatAppearance.BorderSize = 0;
            BtnImprimir.FlatAppearance.MouseDownBackColor = Color.Goldenrod;
            BtnImprimir.FlatAppearance.MouseOverBackColor = Color.Gold;
            BtnImprimir.FlatStyle = FlatStyle.Flat;
            BtnImprimir.ForeColor = SystemColors.ActiveCaptionText;
            BtnImprimir.IconChar = FontAwesome.Sharp.IconChar.Print;
            BtnImprimir.IconColor = Color.Black;
            BtnImprimir.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnImprimir.IconSize = 30;
            BtnImprimir.Location = new Point(553, 133);
            BtnImprimir.Name = "BtnImprimir";
            BtnImprimir.Size = new Size(35, 28);
            BtnImprimir.TabIndex = 94;
            BtnImprimir.UseVisualStyleBackColor = false;
            // 
            // TxtBuscar
            // 
            TxtBuscar.BackColor = Color.FromArgb(55, 55, 55);
            TxtBuscar.BorderStyle = BorderStyle.None;
            TxtBuscar.Font = new Font("Lucida Sans", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TxtBuscar.ForeColor = SystemColors.ButtonFace;
            TxtBuscar.Location = new Point(94, 69);
            TxtBuscar.Name = "TxtBuscar";
            TxtBuscar.Size = new Size(394, 19);
            TxtBuscar.TabIndex = 81;
            // 
            // BtnEliminar
            // 
            BtnEliminar.BackColor = Color.DarkOrange;
            BtnEliminar.Cursor = Cursors.Hand;
            BtnEliminar.FlatAppearance.BorderSize = 0;
            BtnEliminar.FlatAppearance.MouseDownBackColor = Color.Goldenrod;
            BtnEliminar.FlatAppearance.MouseOverBackColor = Color.Gold;
            BtnEliminar.FlatStyle = FlatStyle.Flat;
            BtnEliminar.ForeColor = SystemColors.ActiveCaptionText;
            BtnEliminar.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
            BtnEliminar.IconColor = Color.Black;
            BtnEliminar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnEliminar.IconSize = 30;
            BtnEliminar.Location = new Point(512, 133);
            BtnEliminar.Name = "BtnEliminar";
            BtnEliminar.Size = new Size(35, 28);
            BtnEliminar.TabIndex = 95;
            BtnEliminar.UseVisualStyleBackColor = false;
            // 
            // iconButton3
            // 
            iconButton3.BackColor = Color.DarkOrange;
            iconButton3.Cursor = Cursors.Hand;
            iconButton3.FlatAppearance.BorderSize = 0;
            iconButton3.FlatAppearance.MouseDownBackColor = Color.Goldenrod;
            iconButton3.FlatAppearance.MouseOverBackColor = Color.Gold;
            iconButton3.FlatStyle = FlatStyle.Flat;
            iconButton3.ForeColor = SystemColors.ActiveCaptionText;
            iconButton3.IconChar = FontAwesome.Sharp.IconChar.Filter;
            iconButton3.IconColor = Color.Black;
            iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton3.IconSize = 28;
            iconButton3.Location = new Point(305, 136);
            iconButton3.Name = "iconButton3";
            iconButton3.Size = new Size(30, 26);
            iconButton3.TabIndex = 88;
            iconButton3.UseVisualStyleBackColor = false;
            // 
            // BtnEditar
            // 
            BtnEditar.BackColor = Color.DarkOrange;
            BtnEditar.Cursor = Cursors.Hand;
            BtnEditar.FlatAppearance.BorderSize = 0;
            BtnEditar.FlatAppearance.MouseDownBackColor = Color.Goldenrod;
            BtnEditar.FlatAppearance.MouseOverBackColor = Color.Gold;
            BtnEditar.FlatStyle = FlatStyle.Flat;
            BtnEditar.ForeColor = SystemColors.ActiveCaptionText;
            BtnEditar.IconChar = FontAwesome.Sharp.IconChar.PenToSquare;
            BtnEditar.IconColor = Color.Black;
            BtnEditar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnEditar.IconSize = 30;
            BtnEditar.Location = new Point(512, 99);
            BtnEditar.Name = "BtnEditar";
            BtnEditar.Size = new Size(35, 28);
            BtnEditar.TabIndex = 93;
            BtnEditar.UseVisualStyleBackColor = false;
            // 
            // CboTipo
            // 
            CboTipo.BackColor = Color.FromArgb(55, 55, 55);
            CboTipo.Cursor = Cursors.Hand;
            CboTipo.FlatStyle = FlatStyle.Flat;
            CboTipo.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            CboTipo.ForeColor = SystemColors.ButtonFace;
            CboTipo.FormattingEnabled = true;
            CboTipo.Location = new Point(21, 100);
            CboTipo.Name = "CboTipo";
            CboTipo.Size = new Size(134, 26);
            CboTipo.TabIndex = 82;
            CboTipo.Text = "Tipo";
            // 
            // BtnNuevo
            // 
            BtnNuevo.BackColor = Color.DarkOrange;
            BtnNuevo.Cursor = Cursors.Hand;
            BtnNuevo.FlatAppearance.BorderSize = 0;
            BtnNuevo.FlatAppearance.MouseDownBackColor = Color.Goldenrod;
            BtnNuevo.FlatAppearance.MouseOverBackColor = Color.Gold;
            BtnNuevo.FlatStyle = FlatStyle.Flat;
            BtnNuevo.ForeColor = SystemColors.ActiveCaptionText;
            BtnNuevo.IconChar = FontAwesome.Sharp.IconChar.PlusSquare;
            BtnNuevo.IconColor = Color.Black;
            BtnNuevo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnNuevo.IconSize = 30;
            BtnNuevo.Location = new Point(512, 65);
            BtnNuevo.Name = "BtnNuevo";
            BtnNuevo.Size = new Size(35, 28);
            BtnNuevo.TabIndex = 92;
            BtnNuevo.UseVisualStyleBackColor = false;
            // 
            // iconButton1
            // 
            iconButton1.BackColor = Color.DarkOrange;
            iconButton1.Cursor = Cursors.Hand;
            iconButton1.FlatAppearance.BorderSize = 0;
            iconButton1.FlatAppearance.MouseDownBackColor = Color.Goldenrod;
            iconButton1.FlatAppearance.MouseOverBackColor = Color.Gold;
            iconButton1.FlatStyle = FlatStyle.Flat;
            iconButton1.ForeColor = SystemColors.ActiveCaptionText;
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.Filter;
            iconButton1.IconColor = Color.Black;
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.IconSize = 28;
            iconButton1.Location = new Point(161, 100);
            iconButton1.Name = "iconButton1";
            iconButton1.Size = new Size(30, 26);
            iconButton1.TabIndex = 87;
            iconButton1.UseVisualStyleBackColor = false;
            // 
            // RadioEnabled
            // 
            RadioEnabled.AutoSize = true;
            RadioEnabled.Checked = true;
            RadioEnabled.Cursor = Cursors.Hand;
            RadioEnabled.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            RadioEnabled.ForeColor = SystemColors.ButtonFace;
            RadioEnabled.Location = new Point(216, 102);
            RadioEnabled.Name = "RadioEnabled";
            RadioEnabled.Size = new Size(119, 22);
            RadioEnabled.TabIndex = 83;
            RadioEnabled.TabStop = true;
            RadioEnabled.Text = "Habilitados";
            RadioEnabled.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.CalendarTitleBackColor = SystemColors.ControlText;
            dateTimePicker2.CalendarTitleForeColor = SystemColors.ButtonFace;
            dateTimePicker2.Font = new Font("Lucida Sans", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            dateTimePicker2.Format = DateTimePickerFormat.Short;
            dateTimePicker2.Location = new Point(161, 137);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(134, 25);
            dateTimePicker2.TabIndex = 86;
            // 
            // RadioDisabled
            // 
            RadioDisabled.AutoSize = true;
            RadioDisabled.Cursor = Cursors.Hand;
            RadioDisabled.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            RadioDisabled.ForeColor = SystemColors.ButtonFace;
            RadioDisabled.Location = new Point(341, 102);
            RadioDisabled.Name = "RadioDisabled";
            RadioDisabled.Size = new Size(147, 22);
            RadioDisabled.TabIndex = 84;
            RadioDisabled.Text = "Deshabilitados";
            RadioDisabled.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CalendarTitleBackColor = SystemColors.ControlText;
            dateTimePicker1.CalendarTitleForeColor = SystemColors.ButtonFace;
            dateTimePicker1.Font = new Font("Lucida Sans", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(21, 137);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(134, 25);
            dateTimePicker1.TabIndex = 85;
            // 
            // UsersView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(38, 38, 38);
            ClientSize = new Size(848, 599);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox9);
            Controls.Add(label18);
            Controls.Add(pictureBox8);
            Controls.Add(dataGridView1);
            Controls.Add(BtnDis);
            Controls.Add(label2);
            Controls.Add(BtnEna);
            Controls.Add(label17);
            Controls.Add(BtnImprimir);
            Controls.Add(TxtBuscar);
            Controls.Add(BtnEliminar);
            Controls.Add(iconButton3);
            Controls.Add(BtnEditar);
            Controls.Add(CboTipo);
            Controls.Add(BtnNuevo);
            Controls.Add(iconButton1);
            Controls.Add(RadioEnabled);
            Controls.Add(dateTimePicker2);
            Controls.Add(RadioDisabled);
            Controls.Add(dateTimePicker1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            Name = "UsersView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "UsersView";
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox2;
        private PictureBox pictureBox9;
        private Label label18;
        private PictureBox pictureBox8;
        private DataGridView dataGridView1;
        private FontAwesome.Sharp.IconButton BtnDis;
        private Label label2;
        private FontAwesome.Sharp.IconButton BtnEna;
        private Label label17;
        private FontAwesome.Sharp.IconButton BtnImprimir;
        private TextBox TxtBuscar;
        private FontAwesome.Sharp.IconButton BtnEliminar;
        private FontAwesome.Sharp.IconButton iconButton3;
        private FontAwesome.Sharp.IconButton BtnEditar;
        private ComboBox CboTipo;
        private FontAwesome.Sharp.IconButton BtnNuevo;
        private FontAwesome.Sharp.IconButton iconButton1;
        private RadioButton RadioEnabled;
        private DateTimePicker dateTimePicker2;
        private RadioButton RadioDisabled;
        private DateTimePicker dateTimePicker1;
    }
}