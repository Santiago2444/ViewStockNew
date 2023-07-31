namespace ViewStockNew.Views
{
    partial class RemitoView
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            label1 = new Label();
            ComboTipoCom = new ComboBox();
            label2 = new Label();
            ComboProveedor = new ComboBox();
            label3 = new Label();
            LblCantidadRemito = new TextBox();
            pictureBox1 = new PictureBox();
            pictureBox3 = new PictureBox();
            label5 = new Label();
            tabPage2 = new TabPage();
            GridRemitoDetalle = new DataGridView();
            BtnQuitar = new FontAwesome.Sharp.IconButton();
            BtnAgregar = new FontAwesome.Sharp.IconButton();
            NumCantidad = new NumericUpDown();
            RadioBulto = new RadioButton();
            RadioUnidad = new RadioButton();
            tabPage1 = new TabPage();
            GridProductos = new DataGridView();
            tabControl1 = new TabControl();
            BtnBuscar = new FontAwesome.Sharp.IconButton();
            BtnFiltrar = new FontAwesome.Sharp.IconButton();
            TxtBuscar = new TextBox();
            label4 = new Label();
            CheckTipo = new CheckBox();
            CheckMarca = new CheckBox();
            CheckSpec = new CheckBox();
            ComboTipo = new ComboBox();
            ComboMarca = new ComboBox();
            BtnRecargarData = new FontAwesome.Sharp.IconButton();
            ComboSpec = new ComboBox();
            BtnGuardar = new FontAwesome.Sharp.IconButton();
            BtnCancelar = new FontAwesome.Sharp.IconButton();
            BtnNuevoProveedor = new FontAwesome.Sharp.IconButton();
            BtnAgregarProducto = new FontAwesome.Sharp.IconButton();
            pictureBox2 = new PictureBox();
            pictureBox4 = new PictureBox();
            label6 = new Label();
            LblImporte = new TextBox();
            BtnClose = new FontAwesome.Sharp.IconButton();
            BtnImprimir = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GridRemitoDetalle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumCantidad).BeginInit();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GridProductos).BeginInit();
            tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(344, 28);
            label1.Name = "label1";
            label1.Size = new Size(122, 18);
            label1.TabIndex = 179;
            label1.Text = "Comprobante:";
            // 
            // ComboTipoCom
            // 
            ComboTipoCom.BackColor = Color.FromArgb(50, 50, 50);
            ComboTipoCom.Cursor = Cursors.Hand;
            ComboTipoCom.FlatStyle = FlatStyle.Flat;
            ComboTipoCom.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            ComboTipoCom.ForeColor = SystemColors.ControlLightLight;
            ComboTipoCom.FormattingEnabled = true;
            ComboTipoCom.Items.AddRange(new object[] { "Ingreso", "Egreso" });
            ComboTipoCom.Location = new Point(344, 49);
            ComboTipoCom.Name = "ComboTipoCom";
            ComboTipoCom.Size = new Size(177, 26);
            ComboTipoCom.TabIndex = 178;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ButtonFace;
            label2.Location = new Point(539, 28);
            label2.Name = "label2";
            label2.Size = new Size(96, 18);
            label2.TabIndex = 201;
            label2.Text = "Proveedor:";
            // 
            // ComboProveedor
            // 
            ComboProveedor.BackColor = Color.FromArgb(50, 50, 50);
            ComboProveedor.Cursor = Cursors.Hand;
            ComboProveedor.FlatStyle = FlatStyle.Flat;
            ComboProveedor.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            ComboProveedor.ForeColor = SystemColors.ControlLightLight;
            ComboProveedor.FormattingEnabled = true;
            ComboProveedor.Location = new Point(539, 49);
            ComboProveedor.Name = "ComboProveedor";
            ComboProveedor.Size = new Size(177, 26);
            ComboProveedor.TabIndex = 200;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ButtonFace;
            label3.Location = new Point(344, 82);
            label3.Name = "label3";
            label3.Size = new Size(167, 18);
            label3.TabIndex = 202;
            label3.Text = "Cant. de Productos:";
            // 
            // LblCantidadRemito
            // 
            LblCantidadRemito.BackColor = Color.FromArgb(55, 55, 55);
            LblCantidadRemito.BorderStyle = BorderStyle.None;
            LblCantidadRemito.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            LblCantidadRemito.ForeColor = SystemColors.ButtonFace;
            LblCantidadRemito.Location = new Point(344, 103);
            LblCantidadRemito.Name = "LblCantidadRemito";
            LblCantidadRemito.Size = new Size(177, 19);
            LblCantidadRemito.TabIndex = 203;
            LblCantidadRemito.TextAlign = HorizontalAlignment.Center;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.OrangeDivider;
            pictureBox1.Location = new Point(194, 135);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(286, 10);
            pictureBox1.TabIndex = 204;
            pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.OrangeDivider;
            pictureBox3.Location = new Point(443, 135);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(314, 10);
            pictureBox3.TabIndex = 205;
            pictureBox3.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Lucida Sans", 36F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label5.ForeColor = SystemColors.ControlLightLight;
            label5.Location = new Point(31, 28);
            label5.Name = "label5";
            label5.Size = new Size(196, 55);
            label5.TabIndex = 210;
            label5.Text = "Remito";
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.FromArgb(38, 38, 38);
            tabPage2.Controls.Add(GridRemitoDetalle);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(724, 400);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Detalles del Remito";
            // 
            // GridRemitoDetalle
            // 
            GridRemitoDetalle.AllowUserToAddRows = false;
            GridRemitoDetalle.AllowUserToDeleteRows = false;
            GridRemitoDetalle.AllowUserToResizeColumns = false;
            GridRemitoDetalle.AllowUserToResizeRows = false;
            GridRemitoDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            GridRemitoDetalle.BackgroundColor = Color.FromArgb(50, 50, 50);
            GridRemitoDetalle.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.Orange;
            dataGridViewCellStyle1.Font = new Font("Lucida Sans", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            GridRemitoDetalle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            GridRemitoDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            GridRemitoDetalle.Cursor = Cursors.Hand;
            GridRemitoDetalle.Location = new Point(6, 6);
            GridRemitoDetalle.Name = "GridRemitoDetalle";
            GridRemitoDetalle.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle2.Font = new Font("Lucida Sans", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlLight;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            GridRemitoDetalle.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.SeaGreen;
            dataGridViewCellStyle3.Font = new Font("Lucida Sans", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = Color.MediumSeaGreen;
            GridRemitoDetalle.RowsDefaultCellStyle = dataGridViewCellStyle3;
            GridRemitoDetalle.RowTemplate.Height = 25;
            GridRemitoDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridRemitoDetalle.Size = new Size(712, 385);
            GridRemitoDetalle.TabIndex = 27;
            GridRemitoDetalle.DataBindingComplete += GridRemitoDetalle_DataBindingComplete;
            // 
            // BtnQuitar
            // 
            BtnQuitar.BackColor = Color.FromArgb(249, 43, 43);
            BtnQuitar.Cursor = Cursors.Hand;
            BtnQuitar.FlatAppearance.BorderSize = 0;
            BtnQuitar.FlatAppearance.MouseDownBackColor = Color.DarkRed;
            BtnQuitar.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 70, 70);
            BtnQuitar.FlatStyle = FlatStyle.Flat;
            BtnQuitar.ForeColor = SystemColors.Control;
            BtnQuitar.IconChar = FontAwesome.Sharp.IconChar.SquareMinus;
            BtnQuitar.IconColor = Color.Black;
            BtnQuitar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnQuitar.IconSize = 28;
            BtnQuitar.Location = new Point(724, 225);
            BtnQuitar.Name = "BtnQuitar";
            BtnQuitar.Size = new Size(35, 28);
            BtnQuitar.TabIndex = 222;
            BtnQuitar.UseVisualStyleBackColor = false;
            BtnQuitar.Click += BtnQuitar_Click;
            // 
            // BtnAgregar
            // 
            BtnAgregar.BackColor = Color.FromArgb(13, 216, 71);
            BtnAgregar.Cursor = Cursors.Hand;
            BtnAgregar.FlatAppearance.BorderSize = 0;
            BtnAgregar.FlatAppearance.MouseDownBackColor = Color.Green;
            BtnAgregar.FlatAppearance.MouseOverBackColor = Color.LightGreen;
            BtnAgregar.FlatStyle = FlatStyle.Flat;
            BtnAgregar.ForeColor = SystemColors.Control;
            BtnAgregar.IconChar = FontAwesome.Sharp.IconChar.PlusSquare;
            BtnAgregar.IconColor = Color.Black;
            BtnAgregar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnAgregar.IconSize = 28;
            BtnAgregar.Location = new Point(620, 225);
            BtnAgregar.Name = "BtnAgregar";
            BtnAgregar.Size = new Size(35, 28);
            BtnAgregar.TabIndex = 221;
            BtnAgregar.UseVisualStyleBackColor = false;
            BtnAgregar.Click += BtnAgregar_Click;
            // 
            // NumCantidad
            // 
            NumCantidad.BackColor = SystemColors.Control;
            NumCantidad.BorderStyle = BorderStyle.None;
            NumCantidad.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            NumCantidad.ForeColor = SystemColors.ActiveCaptionText;
            NumCantidad.Location = new Point(661, 230);
            NumCantidad.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            NumCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NumCantidad.Name = "NumCantidad";
            NumCantidad.Size = new Size(57, 22);
            NumCantidad.TabIndex = 223;
            NumCantidad.TextAlign = HorizontalAlignment.Center;
            NumCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // RadioBulto
            // 
            RadioBulto.AutoSize = true;
            RadioBulto.BackColor = SystemColors.Control;
            RadioBulto.Checked = true;
            RadioBulto.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            RadioBulto.ForeColor = SystemColors.ActiveCaptionText;
            RadioBulto.Location = new Point(456, 230);
            RadioBulto.Name = "RadioBulto";
            RadioBulto.Size = new Size(69, 22);
            RadioBulto.TabIndex = 224;
            RadioBulto.TabStop = true;
            RadioBulto.Text = "Bulto";
            RadioBulto.UseVisualStyleBackColor = false;
            RadioBulto.CheckedChanged += RadioBulto_CheckedChanged;
            // 
            // RadioUnidad
            // 
            RadioUnidad.AutoSize = true;
            RadioUnidad.BackColor = SystemColors.Control;
            RadioUnidad.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            RadioUnidad.ForeColor = SystemColors.ActiveCaptionText;
            RadioUnidad.Location = new Point(531, 230);
            RadioUnidad.Name = "RadioUnidad";
            RadioUnidad.Size = new Size(83, 22);
            RadioUnidad.TabIndex = 225;
            RadioUnidad.Text = "Unidad";
            RadioUnidad.UseVisualStyleBackColor = false;
            RadioUnidad.CheckedChanged += RadioUnidad_CheckedChanged;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(38, 38, 38);
            tabPage1.Controls.Add(GridProductos);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(724, 400);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Productos";
            // 
            // GridProductos
            // 
            GridProductos.AllowUserToAddRows = false;
            GridProductos.AllowUserToDeleteRows = false;
            GridProductos.AllowUserToResizeColumns = false;
            GridProductos.AllowUserToResizeRows = false;
            GridProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            GridProductos.BackgroundColor = Color.FromArgb(50, 50, 50);
            GridProductos.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = Color.Orange;
            dataGridViewCellStyle4.Font = new Font("Lucida Sans", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            GridProductos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            GridProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            GridProductos.Cursor = Cursors.Hand;
            GridProductos.Location = new Point(6, 6);
            GridProductos.Name = "GridProductos";
            GridProductos.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle5.Font = new Font("Lucida Sans", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlLight;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            GridProductos.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(50, 50, 50);
            dataGridViewCellStyle6.Font = new Font("Lucida Sans", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = Color.White;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            GridProductos.RowsDefaultCellStyle = dataGridViewCellStyle6;
            GridProductos.RowTemplate.Height = 25;
            GridProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridProductos.Size = new Size(712, 385);
            GridProductos.TabIndex = 28;
            GridProductos.DataBindingComplete += GridProductos_DataBindingComplete;
            // 
            // tabControl1
            // 
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Font = new Font("Lucida Sans", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            tabControl1.Location = new Point(31, 225);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(732, 433);
            tabControl1.TabIndex = 215;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // BtnBuscar
            // 
            BtnBuscar.BackColor = Color.DarkOrange;
            BtnBuscar.Cursor = Cursors.Hand;
            BtnBuscar.FlatAppearance.BorderSize = 0;
            BtnBuscar.FlatAppearance.MouseDownBackColor = Color.Goldenrod;
            BtnBuscar.FlatAppearance.MouseOverBackColor = Color.Gold;
            BtnBuscar.FlatStyle = FlatStyle.Flat;
            BtnBuscar.ForeColor = SystemColors.ActiveCaptionText;
            BtnBuscar.IconChar = FontAwesome.Sharp.IconChar.SearchDollar;
            BtnBuscar.IconColor = Color.Black;
            BtnBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnBuscar.IconSize = 28;
            BtnBuscar.Location = new Point(724, 194);
            BtnBuscar.Name = "BtnBuscar";
            BtnBuscar.Size = new Size(35, 27);
            BtnBuscar.TabIndex = 236;
            BtnBuscar.UseVisualStyleBackColor = false;
            BtnBuscar.Click += BtnBuscar_Click;
            // 
            // BtnFiltrar
            // 
            BtnFiltrar.BackColor = Color.DarkOrange;
            BtnFiltrar.Cursor = Cursors.Hand;
            BtnFiltrar.FlatAppearance.BorderSize = 0;
            BtnFiltrar.FlatAppearance.MouseDownBackColor = Color.Goldenrod;
            BtnFiltrar.FlatAppearance.MouseOverBackColor = Color.Gold;
            BtnFiltrar.FlatStyle = FlatStyle.Flat;
            BtnFiltrar.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            BtnFiltrar.ForeColor = Color.Black;
            BtnFiltrar.IconChar = FontAwesome.Sharp.IconChar.Filter;
            BtnFiltrar.IconColor = Color.Black;
            BtnFiltrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnFiltrar.IconSize = 28;
            BtnFiltrar.Location = new Point(683, 161);
            BtnFiltrar.Name = "BtnFiltrar";
            BtnFiltrar.Size = new Size(35, 27);
            BtnFiltrar.TabIndex = 235;
            BtnFiltrar.TextAlign = ContentAlignment.MiddleLeft;
            BtnFiltrar.UseVisualStyleBackColor = false;
            BtnFiltrar.Click += BtnFiltrar_Click;
            // 
            // TxtBuscar
            // 
            TxtBuscar.BackColor = Color.FromArgb(55, 55, 55);
            TxtBuscar.BorderStyle = BorderStyle.None;
            TxtBuscar.Font = new Font("Lucida Sans", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TxtBuscar.ForeColor = SystemColors.ButtonFace;
            TxtBuscar.Location = new Point(104, 197);
            TxtBuscar.Name = "TxtBuscar";
            TxtBuscar.Size = new Size(614, 19);
            TxtBuscar.TabIndex = 234;
            TxtBuscar.TextChanged += TxtBuscar_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.ButtonFace;
            label4.Location = new Point(31, 198);
            label4.Name = "label4";
            label4.Size = new Size(67, 18);
            label4.TabIndex = 233;
            label4.Text = "Buscar:";
            // 
            // CheckTipo
            // 
            CheckTipo.AutoSize = true;
            CheckTipo.Location = new Point(169, 158);
            CheckTipo.Name = "CheckTipo";
            CheckTipo.Size = new Size(15, 14);
            CheckTipo.TabIndex = 232;
            CheckTipo.UseVisualStyleBackColor = true;
            CheckTipo.CheckedChanged += CheckTipo_CheckedChanged;
            // 
            // CheckMarca
            // 
            CheckMarca.AutoSize = true;
            CheckMarca.Location = new Point(330, 158);
            CheckMarca.Name = "CheckMarca";
            CheckMarca.Size = new Size(15, 14);
            CheckMarca.TabIndex = 231;
            CheckMarca.UseVisualStyleBackColor = true;
            CheckMarca.CheckedChanged += CheckMarca_CheckedChanged;
            // 
            // CheckSpec
            // 
            CheckSpec.AutoSize = true;
            CheckSpec.Location = new Point(490, 158);
            CheckSpec.Name = "CheckSpec";
            CheckSpec.Size = new Size(15, 14);
            CheckSpec.TabIndex = 230;
            CheckSpec.UseVisualStyleBackColor = true;
            CheckSpec.CheckedChanged += CheckSpec_CheckedChanged;
            // 
            // ComboTipo
            // 
            ComboTipo.BackColor = Color.FromArgb(55, 55, 55);
            ComboTipo.Cursor = Cursors.Hand;
            ComboTipo.Enabled = false;
            ComboTipo.FlatStyle = FlatStyle.Flat;
            ComboTipo.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            ComboTipo.ForeColor = SystemColors.ButtonFace;
            ComboTipo.FormattingEnabled = true;
            ComboTipo.Location = new Point(31, 151);
            ComboTipo.Name = "ComboTipo";
            ComboTipo.Size = new Size(134, 26);
            ComboTipo.TabIndex = 229;
            ComboTipo.Text = "Tipo";
            // 
            // ComboMarca
            // 
            ComboMarca.BackColor = Color.FromArgb(55, 55, 55);
            ComboMarca.Cursor = Cursors.Hand;
            ComboMarca.Enabled = false;
            ComboMarca.FlatStyle = FlatStyle.Flat;
            ComboMarca.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            ComboMarca.ForeColor = SystemColors.ButtonFace;
            ComboMarca.FormattingEnabled = true;
            ComboMarca.Location = new Point(192, 151);
            ComboMarca.Name = "ComboMarca";
            ComboMarca.Size = new Size(134, 26);
            ComboMarca.TabIndex = 228;
            ComboMarca.Text = "Marcas";
            // 
            // BtnRecargarData
            // 
            BtnRecargarData.BackColor = Color.DarkOrange;
            BtnRecargarData.Cursor = Cursors.Hand;
            BtnRecargarData.FlatAppearance.BorderSize = 0;
            BtnRecargarData.FlatAppearance.MouseDownBackColor = Color.Goldenrod;
            BtnRecargarData.FlatAppearance.MouseOverBackColor = Color.Gold;
            BtnRecargarData.FlatStyle = FlatStyle.Flat;
            BtnRecargarData.ForeColor = SystemColors.ActiveCaptionText;
            BtnRecargarData.IconChar = FontAwesome.Sharp.IconChar.RotateBackward;
            BtnRecargarData.IconColor = Color.Black;
            BtnRecargarData.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnRecargarData.IconSize = 28;
            BtnRecargarData.Location = new Point(724, 161);
            BtnRecargarData.Name = "BtnRecargarData";
            BtnRecargarData.Size = new Size(35, 27);
            BtnRecargarData.TabIndex = 227;
            BtnRecargarData.UseVisualStyleBackColor = false;
            BtnRecargarData.Click += BtnRecargarData_Click;
            // 
            // ComboSpec
            // 
            ComboSpec.BackColor = Color.FromArgb(55, 55, 55);
            ComboSpec.Cursor = Cursors.Hand;
            ComboSpec.Enabled = false;
            ComboSpec.FlatStyle = FlatStyle.Flat;
            ComboSpec.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            ComboSpec.ForeColor = SystemColors.ButtonFace;
            ComboSpec.FormattingEnabled = true;
            ComboSpec.Location = new Point(352, 151);
            ComboSpec.Name = "ComboSpec";
            ComboSpec.Size = new Size(134, 26);
            ComboSpec.TabIndex = 226;
            ComboSpec.Text = "SPEC";
            // 
            // BtnGuardar
            // 
            BtnGuardar.BackColor = Color.FromArgb(13, 216, 71);
            BtnGuardar.Cursor = Cursors.Hand;
            BtnGuardar.Enabled = false;
            BtnGuardar.FlatAppearance.BorderSize = 0;
            BtnGuardar.FlatAppearance.MouseDownBackColor = Color.DarkGreen;
            BtnGuardar.FlatAppearance.MouseOverBackColor = Color.Green;
            BtnGuardar.FlatStyle = FlatStyle.Flat;
            BtnGuardar.ForeColor = SystemColors.ActiveCaptionText;
            BtnGuardar.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            BtnGuardar.IconColor = Color.Black;
            BtnGuardar.IconFont = FontAwesome.Sharp.IconFont.Solid;
            BtnGuardar.IconSize = 28;
            BtnGuardar.Location = new Point(113, 117);
            BtnGuardar.Name = "BtnGuardar";
            BtnGuardar.Size = new Size(35, 28);
            BtnGuardar.TabIndex = 240;
            BtnGuardar.UseVisualStyleBackColor = false;
            BtnGuardar.Click += BtnGuardar_Click;
            // 
            // BtnCancelar
            // 
            BtnCancelar.BackColor = Color.FromArgb(249, 43, 43);
            BtnCancelar.Cursor = Cursors.Hand;
            BtnCancelar.Enabled = false;
            BtnCancelar.FlatAppearance.BorderSize = 0;
            BtnCancelar.FlatAppearance.MouseDownBackColor = Color.DarkRed;
            BtnCancelar.FlatAppearance.MouseOverBackColor = Color.Firebrick;
            BtnCancelar.FlatStyle = FlatStyle.Flat;
            BtnCancelar.ForeColor = SystemColors.Window;
            BtnCancelar.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            BtnCancelar.IconColor = Color.Black;
            BtnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnCancelar.IconSize = 28;
            BtnCancelar.Location = new Point(72, 117);
            BtnCancelar.Name = "BtnCancelar";
            BtnCancelar.Size = new Size(35, 28);
            BtnCancelar.TabIndex = 239;
            BtnCancelar.UseVisualStyleBackColor = false;
            BtnCancelar.Click += BtnCancelar_Click;
            // 
            // BtnNuevoProveedor
            // 
            BtnNuevoProveedor.BackColor = Color.DarkOrange;
            BtnNuevoProveedor.Cursor = Cursors.Hand;
            BtnNuevoProveedor.FlatAppearance.BorderSize = 0;
            BtnNuevoProveedor.FlatAppearance.MouseDownBackColor = Color.Goldenrod;
            BtnNuevoProveedor.FlatAppearance.MouseOverBackColor = Color.Gold;
            BtnNuevoProveedor.FlatStyle = FlatStyle.Flat;
            BtnNuevoProveedor.ForeColor = SystemColors.ActiveCaptionText;
            BtnNuevoProveedor.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            BtnNuevoProveedor.IconColor = Color.Black;
            BtnNuevoProveedor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnNuevoProveedor.IconSize = 28;
            BtnNuevoProveedor.Location = new Point(722, 49);
            BtnNuevoProveedor.Name = "BtnNuevoProveedor";
            BtnNuevoProveedor.Size = new Size(31, 26);
            BtnNuevoProveedor.TabIndex = 241;
            BtnNuevoProveedor.UseVisualStyleBackColor = false;
            BtnNuevoProveedor.Click += BtnNuevoProveedor_Click;
            // 
            // BtnAgregarProducto
            // 
            BtnAgregarProducto.BackColor = Color.DarkOrange;
            BtnAgregarProducto.Cursor = Cursors.Hand;
            BtnAgregarProducto.FlatAppearance.BorderSize = 0;
            BtnAgregarProducto.FlatAppearance.MouseDownBackColor = Color.Goldenrod;
            BtnAgregarProducto.FlatAppearance.MouseOverBackColor = Color.Gold;
            BtnAgregarProducto.FlatStyle = FlatStyle.Flat;
            BtnAgregarProducto.ForeColor = SystemColors.ActiveCaptionText;
            BtnAgregarProducto.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            BtnAgregarProducto.IconColor = Color.Black;
            BtnAgregarProducto.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnAgregarProducto.IconSize = 30;
            BtnAgregarProducto.Location = new Point(642, 161);
            BtnAgregarProducto.Name = "BtnAgregarProducto";
            BtnAgregarProducto.Size = new Size(35, 27);
            BtnAgregarProducto.TabIndex = 242;
            BtnAgregarProducto.UseVisualStyleBackColor = false;
            BtnAgregarProducto.Click += BtnAgregarProducto_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.OrangeDivider;
            pictureBox2.Location = new Point(31, 178);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(449, 10);
            pictureBox2.TabIndex = 243;
            pictureBox2.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.OrangeDivider;
            pictureBox4.Location = new Point(443, 178);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(192, 10);
            pictureBox4.TabIndex = 244;
            pictureBox4.TabStop = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = SystemColors.ButtonFace;
            label6.Location = new Point(539, 82);
            label6.Name = "label6";
            label6.Size = new Size(76, 18);
            label6.TabIndex = 245;
            label6.Text = "Importe:";
            // 
            // LblImporte
            // 
            LblImporte.BackColor = Color.FromArgb(55, 55, 55);
            LblImporte.BorderStyle = BorderStyle.None;
            LblImporte.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            LblImporte.ForeColor = SystemColors.ButtonFace;
            LblImporte.Location = new Point(539, 103);
            LblImporte.Name = "LblImporte";
            LblImporte.Size = new Size(218, 19);
            LblImporte.TabIndex = 246;
            LblImporte.TextAlign = HorizontalAlignment.Center;
            // 
            // BtnClose
            // 
            BtnClose.BackColor = Color.DarkOrange;
            BtnClose.Cursor = Cursors.Hand;
            BtnClose.FlatAppearance.BorderSize = 0;
            BtnClose.FlatAppearance.MouseDownBackColor = Color.Goldenrod;
            BtnClose.FlatAppearance.MouseOverBackColor = Color.Gold;
            BtnClose.FlatStyle = FlatStyle.Flat;
            BtnClose.ForeColor = SystemColors.ActiveCaptionText;
            BtnClose.IconChar = FontAwesome.Sharp.IconChar.Backspace;
            BtnClose.IconColor = Color.Black;
            BtnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnClose.IconSize = 28;
            BtnClose.Location = new Point(31, 117);
            BtnClose.Name = "BtnClose";
            BtnClose.Size = new Size(35, 27);
            BtnClose.TabIndex = 247;
            BtnClose.UseVisualStyleBackColor = false;
            BtnClose.Click += BtnClose_Click;
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
            BtnImprimir.IconSize = 28;
            BtnImprimir.Location = new Point(154, 117);
            BtnImprimir.Name = "BtnImprimir";
            BtnImprimir.Size = new Size(35, 28);
            BtnImprimir.TabIndex = 248;
            BtnImprimir.UseVisualStyleBackColor = false;
            BtnImprimir.Click += BtnImprimir_Click;
            // 
            // RemitoView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(38, 38, 38);
            ClientSize = new Size(796, 681);
            Controls.Add(BtnImprimir);
            Controls.Add(BtnClose);
            Controls.Add(LblImporte);
            Controls.Add(label6);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox4);
            Controls.Add(BtnAgregarProducto);
            Controls.Add(BtnNuevoProveedor);
            Controls.Add(BtnGuardar);
            Controls.Add(BtnCancelar);
            Controls.Add(BtnBuscar);
            Controls.Add(BtnFiltrar);
            Controls.Add(TxtBuscar);
            Controls.Add(label4);
            Controls.Add(CheckTipo);
            Controls.Add(CheckMarca);
            Controls.Add(CheckSpec);
            Controls.Add(ComboTipo);
            Controls.Add(ComboMarca);
            Controls.Add(BtnRecargarData);
            Controls.Add(ComboSpec);
            Controls.Add(RadioUnidad);
            Controls.Add(RadioBulto);
            Controls.Add(NumCantidad);
            Controls.Add(BtnQuitar);
            Controls.Add(BtnAgregar);
            Controls.Add(pictureBox1);
            Controls.Add(label5);
            Controls.Add(pictureBox3);
            Controls.Add(LblCantidadRemito);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(ComboProveedor);
            Controls.Add(label1);
            Controls.Add(ComboTipoCom);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "RemitoView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Remito";
            Activated += RemitoView_Activated;
            Load += RemitoView_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)GridRemitoDetalle).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumCantidad).EndInit();
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)GridProductos).EndInit();
            tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private ComboBox ComboTipoCom;
        private Label label2;
        private ComboBox ComboProveedor;
        private Label label3;
        private TextBox LblCantidadRemito;
        private PictureBox pictureBox1;
        private PictureBox pictureBox3;
        private Label label5;
        private TabPage tabPage2;
        private DataGridView GridRemitoDetalle;
        private FontAwesome.Sharp.IconButton BtnQuitar;
        private FontAwesome.Sharp.IconButton BtnAgregar;
        private NumericUpDown NumCantidad;
        private RadioButton RadioBulto;
        private RadioButton RadioUnidad;
        private TabPage tabPage1;
        private DataGridView GridProductos;
        private TabControl tabControl1;
        private FontAwesome.Sharp.IconButton BtnBuscar;
        private FontAwesome.Sharp.IconButton BtnFiltrar;
        private TextBox TxtBuscar;
        private Label label4;
        private CheckBox CheckTipo;
        private CheckBox CheckMarca;
        private CheckBox CheckSpec;
        private ComboBox ComboTipo;
        private ComboBox ComboMarca;
        private FontAwesome.Sharp.IconButton BtnRecargarData;
        private ComboBox ComboSpec;
        private FontAwesome.Sharp.IconButton BtnGuardar;
        private FontAwesome.Sharp.IconButton BtnCancelar;
        private FontAwesome.Sharp.IconButton BtnNuevoProveedor;
        private FontAwesome.Sharp.IconButton BtnAgregarProducto;
        private PictureBox pictureBox2;
        private PictureBox pictureBox4;
        private Label label6;
        private TextBox LblImporte;
        private FontAwesome.Sharp.IconButton BtnClose;
        private FontAwesome.Sharp.IconButton BtnImprimir;
    }
}