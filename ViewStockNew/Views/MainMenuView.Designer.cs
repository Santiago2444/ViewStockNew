namespace ViewStockNew.Views
{
    partial class MainMenuView
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
            menuStrip1 = new MenuStrip();
            IcoItemVentas = new FontAwesome.Sharp.IconMenuItem();
            RealizarVentaMenuItem = new FontAwesome.Sharp.IconMenuItem();
            iconMenuItem2 = new FontAwesome.Sharp.IconMenuItem();
            IcoMenuItemUser = new FontAwesome.Sharp.IconMenuItem();
            IcoItemProductos = new FontAwesome.Sharp.IconMenuItem();
            IcoMenuItemCuentas = new FontAwesome.Sharp.IconMenuItem();
            IcoMenuItemProveedor = new FontAwesome.Sharp.IconMenuItem();
            ProductosList = new FontAwesome.Sharp.IconMenuItem();
            iconMenuItem4 = new FontAwesome.Sharp.IconMenuItem();
            iconMenuItem5 = new FontAwesome.Sharp.IconMenuItem();
            MenuRemitoList = new FontAwesome.Sharp.IconMenuItem();
            MenuRealizarRemito = new FontAwesome.Sharp.IconMenuItem();
            IcoItemExit = new FontAwesome.Sharp.IconMenuItem();
            IcoBtnRemito = new FontAwesome.Sharp.IconButton();
            IcoBtnRealizarVenta = new FontAwesome.Sharp.IconButton();
            IcoBtnCrearUsuario = new FontAwesome.Sharp.IconButton();
            IcoBtnProducto = new FontAwesome.Sharp.IconButton();
            IcoBtnCuenta = new FontAwesome.Sharp.IconButton();
            IcoBtnTerminarTurno = new FontAwesome.Sharp.IconButton();
            pictureBox1 = new PictureBox();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.FromArgb(30, 30, 30);
            menuStrip1.Items.AddRange(new ToolStripItem[] { IcoItemVentas, IcoMenuItemUser, IcoItemProductos, IcoMenuItemCuentas, IcoMenuItemProveedor, IcoItemExit });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(8, 2, 0, 2);
            menuStrip1.Size = new Size(874, 26);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // IcoItemVentas
            // 
            IcoItemVentas.DropDownItems.AddRange(new ToolStripItem[] { RealizarVentaMenuItem, iconMenuItem2 });
            IcoItemVentas.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            IcoItemVentas.ForeColor = SystemColors.ControlLight;
            IcoItemVentas.IconChar = FontAwesome.Sharp.IconChar.Store;
            IcoItemVentas.IconColor = Color.Gold;
            IcoItemVentas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            IcoItemVentas.IconSize = 30;
            IcoItemVentas.Name = "IcoItemVentas";
            IcoItemVentas.Size = new Size(93, 22);
            IcoItemVentas.Text = "Ventas";
            // 
            // RealizarVentaMenuItem
            // 
            RealizarVentaMenuItem.BackColor = Color.FromArgb(30, 30, 30);
            RealizarVentaMenuItem.ForeColor = SystemColors.ControlLight;
            RealizarVentaMenuItem.IconChar = FontAwesome.Sharp.IconChar.ShoppingCart;
            RealizarVentaMenuItem.IconColor = Color.Gold;
            RealizarVentaMenuItem.IconFont = FontAwesome.Sharp.IconFont.Auto;
            RealizarVentaMenuItem.IconSize = 30;
            RealizarVentaMenuItem.Name = "RealizarVentaMenuItem";
            RealizarVentaMenuItem.Size = new Size(227, 22);
            RealizarVentaMenuItem.Text = "Realizar Venta";
            RealizarVentaMenuItem.Click += RealizarVentaMenuItem_Click;
            // 
            // iconMenuItem2
            // 
            iconMenuItem2.BackColor = Color.FromArgb(30, 30, 30);
            iconMenuItem2.ForeColor = SystemColors.ControlLight;
            iconMenuItem2.IconChar = FontAwesome.Sharp.IconChar.ShoppingBag;
            iconMenuItem2.IconColor = Color.Gold;
            iconMenuItem2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconMenuItem2.Name = "iconMenuItem2";
            iconMenuItem2.Size = new Size(227, 22);
            iconMenuItem2.Text = "Ventas Realizadas";
            iconMenuItem2.Click += iconMenuItem2_Click;
            // 
            // IcoMenuItemUser
            // 
            IcoMenuItemUser.BackColor = Color.FromArgb(30, 30, 30);
            IcoMenuItemUser.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            IcoMenuItemUser.ForeColor = SystemColors.ControlLight;
            IcoMenuItemUser.IconChar = FontAwesome.Sharp.IconChar.UserGroup;
            IcoMenuItemUser.IconColor = Color.Gold;
            IcoMenuItemUser.IconFont = FontAwesome.Sharp.IconFont.Auto;
            IcoMenuItemUser.IconSize = 30;
            IcoMenuItemUser.Name = "IcoMenuItemUser";
            IcoMenuItemUser.Size = new Size(108, 22);
            IcoMenuItemUser.Text = "Usuarios";
            IcoMenuItemUser.Click += IcoMenuItemUser_Click;
            // 
            // IcoItemProductos
            // 
            IcoItemProductos.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            IcoItemProductos.ForeColor = SystemColors.ControlLight;
            IcoItemProductos.IconChar = FontAwesome.Sharp.IconChar.PizzaSlice;
            IcoItemProductos.IconColor = Color.Gold;
            IcoItemProductos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            IcoItemProductos.IconSize = 30;
            IcoItemProductos.Name = "IcoItemProductos";
            IcoItemProductos.Size = new Size(119, 22);
            IcoItemProductos.Text = "Productos";
            IcoItemProductos.Click += IcoItemProductos_Click;
            // 
            // IcoMenuItemCuentas
            // 
            IcoMenuItemCuentas.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            IcoMenuItemCuentas.ForeColor = SystemColors.ControlLight;
            IcoMenuItemCuentas.IconChar = FontAwesome.Sharp.IconChar.Users;
            IcoMenuItemCuentas.IconColor = Color.Gold;
            IcoMenuItemCuentas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            IcoMenuItemCuentas.IconSize = 30;
            IcoMenuItemCuentas.Name = "IcoMenuItemCuentas";
            IcoMenuItemCuentas.Size = new Size(103, 22);
            IcoMenuItemCuentas.Text = "Cuentas";
            IcoMenuItemCuentas.Click += IcoMenuItemCuentas_Click;
            // 
            // IcoMenuItemProveedor
            // 
            IcoMenuItemProveedor.DropDownItems.AddRange(new ToolStripItem[] { ProductosList, iconMenuItem4, iconMenuItem5 });
            IcoMenuItemProveedor.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            IcoMenuItemProveedor.ForeColor = SystemColors.ControlLight;
            IcoMenuItemProveedor.IconChar = FontAwesome.Sharp.IconChar.Truck;
            IcoMenuItemProveedor.IconColor = Color.Gold;
            IcoMenuItemProveedor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            IcoMenuItemProveedor.IconSize = 30;
            IcoMenuItemProveedor.Name = "IcoMenuItemProveedor";
            IcoMenuItemProveedor.Size = new Size(139, 22);
            IcoMenuItemProveedor.Text = "Proveedores";
            IcoMenuItemProveedor.Click += IcoMenuItemProveedor_Click;
            // 
            // ProductosList
            // 
            ProductosList.BackColor = Color.FromArgb(30, 30, 30);
            ProductosList.ForeColor = SystemColors.ControlLight;
            ProductosList.IconChar = FontAwesome.Sharp.IconChar.None;
            ProductosList.IconColor = Color.Gold;
            ProductosList.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ProductosList.Name = "ProductosList";
            ProductosList.Size = new Size(249, 22);
            ProductosList.Text = "Lista de Proveedores";
            ProductosList.Click += ProductosList_Click;
            // 
            // iconMenuItem4
            // 
            iconMenuItem4.BackColor = Color.FromArgb(30, 30, 30);
            iconMenuItem4.ForeColor = SystemColors.ControlLight;
            iconMenuItem4.IconChar = FontAwesome.Sharp.IconChar.None;
            iconMenuItem4.IconColor = Color.Gold;
            iconMenuItem4.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconMenuItem4.Name = "iconMenuItem4";
            iconMenuItem4.Size = new Size(249, 22);
            iconMenuItem4.Text = "Crear Proveedor";
            iconMenuItem4.Click += iconMenuItem4_Click;
            // 
            // iconMenuItem5
            // 
            iconMenuItem5.BackColor = Color.FromArgb(30, 30, 30);
            iconMenuItem5.DropDownItems.AddRange(new ToolStripItem[] { MenuRemitoList, MenuRealizarRemito });
            iconMenuItem5.ForeColor = SystemColors.ControlLight;
            iconMenuItem5.IconChar = FontAwesome.Sharp.IconChar.None;
            iconMenuItem5.IconColor = Color.Gold;
            iconMenuItem5.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconMenuItem5.Name = "iconMenuItem5";
            iconMenuItem5.Size = new Size(249, 22);
            iconMenuItem5.Text = "Remitos";
            // 
            // MenuRemitoList
            // 
            MenuRemitoList.BackColor = Color.FromArgb(30, 30, 30);
            MenuRemitoList.ForeColor = SystemColors.ControlLight;
            MenuRemitoList.IconChar = FontAwesome.Sharp.IconChar.None;
            MenuRemitoList.IconColor = Color.Gold;
            MenuRemitoList.IconFont = FontAwesome.Sharp.IconFont.Auto;
            MenuRemitoList.Name = "MenuRemitoList";
            MenuRemitoList.Size = new Size(213, 22);
            MenuRemitoList.Text = "Lista de Remitos";
            // 
            // MenuRealizarRemito
            // 
            MenuRealizarRemito.BackColor = Color.FromArgb(30, 30, 30);
            MenuRealizarRemito.ForeColor = SystemColors.ControlLight;
            MenuRealizarRemito.IconChar = FontAwesome.Sharp.IconChar.None;
            MenuRealizarRemito.IconColor = Color.Gold;
            MenuRealizarRemito.IconFont = FontAwesome.Sharp.IconFont.Auto;
            MenuRealizarRemito.Name = "MenuRealizarRemito";
            MenuRealizarRemito.Size = new Size(213, 22);
            MenuRealizarRemito.Text = "Realizar Remito";
            MenuRealizarRemito.Click += MenuRealizarRemito_Click;
            // 
            // IcoItemExit
            // 
            IcoItemExit.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            IcoItemExit.ForeColor = SystemColors.ControlLight;
            IcoItemExit.IconChar = FontAwesome.Sharp.IconChar.DoorOpen;
            IcoItemExit.IconColor = Color.Gold;
            IcoItemExit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            IcoItemExit.IconSize = 30;
            IcoItemExit.Name = "IcoItemExit";
            IcoItemExit.Size = new Size(70, 22);
            IcoItemExit.Text = "Salir";
            IcoItemExit.Click += IcoItemExit_Click;
            // 
            // IcoBtnRemito
            // 
            IcoBtnRemito.BackColor = Color.Transparent;
            IcoBtnRemito.Cursor = Cursors.Hand;
            IcoBtnRemito.FlatAppearance.BorderSize = 0;
            IcoBtnRemito.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 30, 30);
            IcoBtnRemito.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 50, 50);
            IcoBtnRemito.FlatStyle = FlatStyle.Flat;
            IcoBtnRemito.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            IcoBtnRemito.ForeColor = Color.Black;
            IcoBtnRemito.IconChar = FontAwesome.Sharp.IconChar.TruckRampBox;
            IcoBtnRemito.IconColor = Color.Gold;
            IcoBtnRemito.IconFont = FontAwesome.Sharp.IconFont.Auto;
            IcoBtnRemito.IconSize = 50;
            IcoBtnRemito.Location = new Point(467, 45);
            IcoBtnRemito.Name = "IcoBtnRemito";
            IcoBtnRemito.Size = new Size(66, 56);
            IcoBtnRemito.TabIndex = 193;
            IcoBtnRemito.TextAlign = ContentAlignment.MiddleLeft;
            IcoBtnRemito.UseVisualStyleBackColor = false;
            IcoBtnRemito.Click += IcoBtnRemito_Click;
            // 
            // IcoBtnRealizarVenta
            // 
            IcoBtnRealizarVenta.BackColor = Color.Transparent;
            IcoBtnRealizarVenta.Cursor = Cursors.Hand;
            IcoBtnRealizarVenta.FlatAppearance.BorderSize = 0;
            IcoBtnRealizarVenta.FlatAppearance.MouseDownBackColor = Color.Goldenrod;
            IcoBtnRealizarVenta.FlatAppearance.MouseOverBackColor = Color.Gold;
            IcoBtnRealizarVenta.FlatStyle = FlatStyle.Flat;
            IcoBtnRealizarVenta.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            IcoBtnRealizarVenta.ForeColor = Color.Black;
            IcoBtnRealizarVenta.IconChar = FontAwesome.Sharp.IconChar.ShoppingCart;
            IcoBtnRealizarVenta.IconColor = Color.Black;
            IcoBtnRealizarVenta.IconFont = FontAwesome.Sharp.IconFont.Auto;
            IcoBtnRealizarVenta.IconSize = 50;
            IcoBtnRealizarVenta.Location = new Point(22, 45);
            IcoBtnRealizarVenta.Name = "IcoBtnRealizarVenta";
            IcoBtnRealizarVenta.Size = new Size(66, 56);
            IcoBtnRealizarVenta.TabIndex = 194;
            IcoBtnRealizarVenta.TextAlign = ContentAlignment.MiddleLeft;
            IcoBtnRealizarVenta.UseVisualStyleBackColor = false;
            IcoBtnRealizarVenta.Click += iconButton2_Click;
            // 
            // IcoBtnCrearUsuario
            // 
            IcoBtnCrearUsuario.BackColor = Color.Transparent;
            IcoBtnCrearUsuario.Cursor = Cursors.Hand;
            IcoBtnCrearUsuario.FlatAppearance.BorderSize = 0;
            IcoBtnCrearUsuario.FlatAppearance.MouseDownBackColor = Color.Goldenrod;
            IcoBtnCrearUsuario.FlatAppearance.MouseOverBackColor = Color.Gold;
            IcoBtnCrearUsuario.FlatStyle = FlatStyle.Flat;
            IcoBtnCrearUsuario.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            IcoBtnCrearUsuario.ForeColor = Color.Black;
            IcoBtnCrearUsuario.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
            IcoBtnCrearUsuario.IconColor = Color.Black;
            IcoBtnCrearUsuario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            IcoBtnCrearUsuario.IconSize = 50;
            IcoBtnCrearUsuario.Location = new Point(125, 45);
            IcoBtnCrearUsuario.Name = "IcoBtnCrearUsuario";
            IcoBtnCrearUsuario.Size = new Size(66, 56);
            IcoBtnCrearUsuario.TabIndex = 195;
            IcoBtnCrearUsuario.TextAlign = ContentAlignment.MiddleLeft;
            IcoBtnCrearUsuario.UseVisualStyleBackColor = false;
            IcoBtnCrearUsuario.Click += IcoBtnCrearUsuario_Click;
            // 
            // IcoBtnProducto
            // 
            IcoBtnProducto.BackColor = Color.Transparent;
            IcoBtnProducto.Cursor = Cursors.Hand;
            IcoBtnProducto.FlatAppearance.BorderSize = 0;
            IcoBtnProducto.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 30, 30);
            IcoBtnProducto.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 50, 50);
            IcoBtnProducto.FlatStyle = FlatStyle.Flat;
            IcoBtnProducto.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            IcoBtnProducto.ForeColor = Color.Black;
            IcoBtnProducto.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            IcoBtnProducto.IconColor = Color.Orange;
            IcoBtnProducto.IconFont = FontAwesome.Sharp.IconFont.Auto;
            IcoBtnProducto.IconSize = 50;
            IcoBtnProducto.Location = new Point(236, 45);
            IcoBtnProducto.Name = "IcoBtnProducto";
            IcoBtnProducto.Size = new Size(66, 56);
            IcoBtnProducto.TabIndex = 196;
            IcoBtnProducto.TextAlign = ContentAlignment.MiddleLeft;
            IcoBtnProducto.UseVisualStyleBackColor = false;
            IcoBtnProducto.Click += IcoBtnProducto_Click;
            // 
            // IcoBtnCuenta
            // 
            IcoBtnCuenta.BackColor = Color.Transparent;
            IcoBtnCuenta.Cursor = Cursors.Hand;
            IcoBtnCuenta.FlatAppearance.BorderSize = 0;
            IcoBtnCuenta.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 30, 30);
            IcoBtnCuenta.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 50, 50);
            IcoBtnCuenta.FlatStyle = FlatStyle.Flat;
            IcoBtnCuenta.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            IcoBtnCuenta.ForeColor = Color.Black;
            IcoBtnCuenta.IconChar = FontAwesome.Sharp.IconChar.UserGear;
            IcoBtnCuenta.IconColor = Color.Orange;
            IcoBtnCuenta.IconFont = FontAwesome.Sharp.IconFont.Auto;
            IcoBtnCuenta.IconSize = 50;
            IcoBtnCuenta.Location = new Point(346, 45);
            IcoBtnCuenta.Name = "IcoBtnCuenta";
            IcoBtnCuenta.Size = new Size(66, 56);
            IcoBtnCuenta.TabIndex = 197;
            IcoBtnCuenta.TextAlign = ContentAlignment.MiddleLeft;
            IcoBtnCuenta.UseVisualStyleBackColor = false;
            IcoBtnCuenta.Click += IcoBtnCuenta_Click;
            // 
            // IcoBtnTerminarTurno
            // 
            IcoBtnTerminarTurno.BackColor = Color.Transparent;
            IcoBtnTerminarTurno.Cursor = Cursors.Hand;
            IcoBtnTerminarTurno.FlatAppearance.BorderSize = 0;
            IcoBtnTerminarTurno.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 30, 30);
            IcoBtnTerminarTurno.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 50, 50);
            IcoBtnTerminarTurno.FlatStyle = FlatStyle.Flat;
            IcoBtnTerminarTurno.Font = new Font("Lucida Sans", 12F, FontStyle.Bold, GraphicsUnit.Point);
            IcoBtnTerminarTurno.ForeColor = Color.Black;
            IcoBtnTerminarTurno.IconChar = FontAwesome.Sharp.IconChar.UpRightFromSquare;
            IcoBtnTerminarTurno.IconColor = Color.Gold;
            IcoBtnTerminarTurno.IconFont = FontAwesome.Sharp.IconFont.Auto;
            IcoBtnTerminarTurno.IconSize = 50;
            IcoBtnTerminarTurno.Location = new Point(570, 45);
            IcoBtnTerminarTurno.Name = "IcoBtnTerminarTurno";
            IcoBtnTerminarTurno.Size = new Size(66, 56);
            IcoBtnTerminarTurno.TabIndex = 198;
            IcoBtnTerminarTurno.TextAlign = ContentAlignment.MiddleLeft;
            IcoBtnTerminarTurno.UseVisualStyleBackColor = false;
            IcoBtnTerminarTurno.Visible = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            pictureBox1.Image = Properties.Resources.MenuLogoDE1;
            pictureBox1.Location = new Point(610, 319);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(225, 229);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 199;
            pictureBox1.TabStop = false;
            // 
            // MainMenuView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(38, 38, 38);
            BackgroundImage = Properties.Resources.View1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(874, 575);
            Controls.Add(pictureBox1);
            Controls.Add(IcoBtnTerminarTurno);
            Controls.Add(IcoBtnCuenta);
            Controls.Add(IcoBtnProducto);
            Controls.Add(IcoBtnCrearUsuario);
            Controls.Add(IcoBtnRealizarVenta);
            Controls.Add(IcoBtnRemito);
            Controls.Add(menuStrip1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "MainMenuView";
            WindowState = FormWindowState.Maximized;
            Activated += MainMenuView_Activated;
            Load += MainMenuView_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private FontAwesome.Sharp.IconMenuItem IcoItemProductos;
        private FontAwesome.Sharp.IconMenuItem IcoItemVentas;
        private FontAwesome.Sharp.IconMenuItem IcoMenuItemProveedor;
        private FontAwesome.Sharp.IconMenuItem IcoMenuItemUser;
        private FontAwesome.Sharp.IconMenuItem IcoItemExit;
        private FontAwesome.Sharp.IconMenuItem IcoMenuItemCuentas;
        private FontAwesome.Sharp.IconMenuItem RealizarVentaMenuItem;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem2;
        private FontAwesome.Sharp.IconMenuItem ProductosList;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem4;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem5;
        private FontAwesome.Sharp.IconMenuItem MenuRemitoList;
        private FontAwesome.Sharp.IconMenuItem MenuRealizarRemito;
        private FontAwesome.Sharp.IconButton IcoBtnRemito;
        private FontAwesome.Sharp.IconButton IcoBtnRealizarVenta;
        private FontAwesome.Sharp.IconButton IcoBtnCrearUsuario;
        private FontAwesome.Sharp.IconButton IcoBtnProducto;
        private FontAwesome.Sharp.IconButton IcoBtnCuenta;
        private FontAwesome.Sharp.IconButton IcoBtnTerminarTurno;
        private PictureBox pictureBox1;
    }
}