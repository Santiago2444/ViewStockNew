using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ViewStockNew.Interfaces;
using ViewStockNew.Models;
using ViewStockNew.Utils;

namespace ViewStockNew.Views
{
    public partial class SaveView : Form
    {
        private int rowsCount;
        private int rowsProductCount;
        private decimal precioAcum;
        private int cantidad;
        private int cuentaId;
        private int articulos;
        private decimal vuelto;
        private decimal dinero;
        private decimal importe;

        private IUnitOfWork unitOfWork;
        private decimal ganancia;
        private int idproducto;
        private bool saldoVuelto;
        private int codigoVespecifico;
        private int iddeuda;
        private bool selection;
        private bool filter;
        private decimal descuento;
        private Remito remito;

        public SaveView()
        {
            InitializeComponent();
        }

        public SaveView(int rowsCount, int rowsProductCount, IUnitOfWork unitOfWork, int _cuentaId, int _articulos, decimal _vuelto, decimal _dinero, decimal _importe)
        {
            InitializeComponent();
            //
            this.rowsCount = rowsCount;
            this.rowsProductCount = rowsProductCount;
            this.unitOfWork = unitOfWork;
            this.cuentaId = _cuentaId;
            this.articulos = _articulos;
            this.vuelto = _vuelto;
            this.dinero = _dinero;
            this.importe = _importe;
            //
            GuardarVenta();
        }

        private async void GuardarVenta()
        {
            int VentaId = 0;
            //
            int IdProducto = 0;
            //
            int IdVentaDetalle = 0;
            //
            int _codigoVenta = 0;
            //
            var ventaDetalleList = ClasesCompartidas.VentaDetalle;
            var productoList = ClasesCompartidas.productosList;
            //
            foreach (Producto productoItem in productoList)
            {
                if (Convert.ToInt32(productoItem.Carrito) > 0)
                {
                    IdProducto = (int)productoItem.Id;
                }
                //
                if (IdProducto != 0)
                {
                    var producto = unitOfWork.ProductoRepository.GetByID(IdProducto);
                    producto.Carrito = 0;
                    //
                    try
                    {
                        new ModelsValidator().Validate(producto);
                        //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        //if (respuesta == DialogResult.Yes)
                        //{
                        unitOfWork.ProductoRepository.Update(producto);
                        unitOfWork.Save();
                        IdProducto = 0;
                    }
                    catch (Exception ex)
                    {
                        Debug.Print(ex.Message);
                        Debug.Print(ex.InnerException?.Message);
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            //
            var oldCodigoVenta = unitOfWork.CodigoVentaRepository.GetByID(1);
            _codigoVenta = oldCodigoVenta.Codigo + 1;
            //
            CodigoVenta codigoVenta = new CodigoVenta()
            {
                Id = 1,
                Codigo = _codigoVenta
            };
            try
            {
                new ModelsValidator().Validate(codigoVenta);
                //
                unitOfWork.CodigoVentaRepository.Update(codigoVenta);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                Debug.Print(ex.InnerException?.Message);
                MessageBox.Show(ex.Message);
            }
            //
            Venta venta = new Venta()
            {
                Importe = importe,
                Dinero = dinero,
                Vuelto = vuelto,
                Articulos = articulos,
                CuentaId = cuentaId,
                Estado = "Pagado",
                Fecha = DateTime.Now,
                Codigo = _codigoVenta,
                UsuarioId = ClasesCompartidas.UserId,
                PagoId = null
            };
            try
            {
                new ModelsValidator().Validate(venta);
                //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (respuesta == DialogResult.Yes)
                //{
                unitOfWork.VentaRepository.Add(venta);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                Debug.Print(ex.InnerException?.Message);
                MessageBox.Show(ex.Message);
            }
            //
            VentaId = venta.Id;
            //
            foreach (VentaDetalle ventaDetalleItem in ventaDetalleList)
            {
                if (Convert.ToString(ventaDetalleItem.Pagado) == "Carrito")
                {
                    IdVentaDetalle = (int)ventaDetalleItem.Id;
                }
                //
                if (IdVentaDetalle != 0)
                {
                    var ventaDetalle = unitOfWork.VentaDetalleRepository.GetByID(IdVentaDetalle);
                    ventaDetalle.Pagado = "Si";
                    ventaDetalle.CodigoDeVenta = _codigoVenta;
                    ventaDetalle.FechaDePago = DateTime.Now;
                    ventaDetalle.CuentaId = cuentaId;
                    ventaDetalle.VentaId = VentaId;
                    //
                    try
                    {
                        new ModelsValidator().Validate(ventaDetalle);
                        //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        //if (respuesta == DialogResult.Yes)
                        //{
                        unitOfWork.VentaDetalleRepository.Update(ventaDetalle);
                        unitOfWork.Save();
                        IdVentaDetalle = 0;
                    }
                    catch (Exception ex)
                    {
                        Debug.Print(ex.Message);
                        Debug.Print(ex.InnerException?.Message);
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            //
            ClasesCompartidas.CodigoDeVenta = _codigoVenta;
            //
            // A continuación se actualizan los datos recientemente cargados
            ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
            ClasesCompartidas.ventasList.DataSource = await unitOfWork.VentaRepository.GetAllAsync(include: q => q.Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Estado == "Pagado");
            ClasesCompartidas.ventaDetallesList.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Pagado == "Si");
            ClasesCompartidas.VentaDetalle.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor), filter: q => q.Pagado == "Carrito");
            //
            Close();
        }

        public SaveView(int rowsCount, int rowsProductCount, IUnitOfWork unitOfWork, decimal _precioAcum, int _cantidad, int _cuentaId)
        {
            InitializeComponent();
            //
            this.rowsCount = rowsCount;
            this.rowsProductCount = rowsProductCount;
            this.unitOfWork = unitOfWork;
            this.precioAcum = _precioAcum;
            this.cantidad = _cantidad;
            this.cuentaId = _cuentaId;
            //
            AñadirVentaCuenta();
        }

        public SaveView(IUnitOfWork unitOfWork, decimal importe, decimal dinero, decimal vuelto, int cuentaId)
        {
            InitializeComponent();
            //
            this.unitOfWork = unitOfWork;
            this.importe = importe;
            this.dinero = dinero;
            this.vuelto = vuelto;
            this.cuentaId = cuentaId;
            //
            PagarDeudaTotal();
        }

        public SaveView(IUnitOfWork unitOfWork, decimal dinero, int cuentaId)
        {
            InitializeComponent();
            //
            this.unitOfWork = unitOfWork;
            this.dinero = dinero;
            this.cuentaId = cuentaId;
            //
            AñadirSaldo();
        }

        public SaveView(IUnitOfWork unitOfWork, decimal importe, decimal dinero, decimal vuelto, int cuentaId, bool saldoVuelto)
        {
            InitializeComponent();
            //
            this.unitOfWork = unitOfWork;
            this.importe = importe;
            this.dinero = dinero;
            this.vuelto = vuelto;
            this.cuentaId = cuentaId;
            this.saldoVuelto = saldoVuelto;
            //
            PagarDeudaParcial();
        }

        public SaveView(IUnitOfWork unitOfWork, decimal importe, decimal dinero, decimal vuelto, int cuentaId, int codigoVespecifico, int iddeuda)
        {
            InitializeComponent();
            //
            this.unitOfWork = unitOfWork;
            this.importe = importe;
            this.dinero = dinero;
            this.vuelto = vuelto;
            this.cuentaId = cuentaId;
            this.codigoVespecifico = codigoVespecifico;
            this.iddeuda = iddeuda;
            //
            PagarDeudaEspecifica();
        }

        public SaveView(IUnitOfWork unitOfWork, decimal ganancia, int idproducto, bool selection, bool filter)
        {
            InitializeComponent();
            //
            this.unitOfWork = unitOfWork;
            this.ganancia = ganancia;
            this.idproducto = idproducto;
            this.selection = selection;
            this.filter = filter;
            //
            CambiarPrecioProductos();
        }

        public SaveView(decimal descuento, int idProducto, bool selection, bool filter, IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            //
            this.descuento = descuento;
            this.idproducto = idProducto;
            this.selection = selection;
            this.filter = filter;
            this.unitOfWork = unitOfWork;
            //
            CambiarPrecioBulto();
        }

        public SaveView(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            //
            this.unitOfWork = unitOfWork;
            //   
            CancelarRemito();
        }

        public SaveView(IUnitOfWork unitOfWork, Remito remito)
        {
            InitializeComponent();
            //
            this.unitOfWork = unitOfWork;
            this.remito = remito;
            //
            GuardarRemito();
        }

        private async void GuardarRemito()
        {
            var productosList = ClasesCompartidas.productosList;
            var remitoDetalleList = ClasesCompartidas.RemitosDetalle;
            //
            try
            {
                new ModelsValidator().Validate(remito);
                //
                unitOfWork.RemitoRepository.Add(remito);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                Debug.Print(ex.InnerException?.Message);
                MessageBox.Show(ex.Message);
            }
            //
            var remitoId = remito.Id;
            var comprobante = remito.TipoComprobante;
            //
            foreach (RemitoDetalle item in remitoDetalleList)
            {
                var remitoDetalle = unitOfWork.RemitoDetalleRepository.GetByID(item.Id);
                remitoDetalle.RemitoId = remitoId;
                ClasesCompartidas.RemitoId = remitoId;
                //
                var TipoId = Convert.ToInt32(item.TipoProductoId);
                var MarcaId = Convert.ToInt32(item.MarcaId);
                var SPECId = Convert.ToInt32(item.SPECId);
                var Detalles = Convert.ToString(item.Detalles);
                //
                foreach (Producto cosito in productosList)
                {
                    if (Convert.ToInt32(cosito.TipoProductoId) == remitoDetalle.TipoProductoId && Convert.ToInt32(cosito.SPECId) == remitoDetalle.SPECId && Convert.ToInt32(cosito.MarcaId) == remitoDetalle.MarcaId && Convert.ToString(cosito.Detalles) == remitoDetalle.Detalles)
                    {
                        var producto = unitOfWork.ProductoRepository.GetByID(cosito.Id);
                        producto.Carrito = 0;
                        //
                        if (comprobante == "Ingreso")
                        {
                            producto.Stock = producto.Stock + (int)remitoDetalle.CantidadTotal;
                        }
                        else if (comprobante == "Egreso")
                        {
                            producto.Stock = producto.Stock - (int)remitoDetalle.CantidadTotal;
                        }
                        //
                        try
                        {
                            new ModelsValidator().Validate(remito);
                            new ModelsValidator().Validate(remito);
                            //
                            unitOfWork.RemitoDetalleRepository.Update(remitoDetalle);
                            unitOfWork.ProductoRepository.Update(producto);
                            //
                            unitOfWork.Save();
                        }
                        catch (Exception ex)
                        {
                            Debug.Print(ex.Message);
                            Debug.Print(ex.InnerException?.Message);
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            //
            ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
            ClasesCompartidas.RemitosDetalle.DataSource = await unitOfWork.RemitoDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC), filter: q => q.RemitoId == null);
            ClasesCompartidas.remitos.DataSource = await unitOfWork.RemitoRepository.GetAllAsync(include: q => q.Include(q => q.Usuario).Include(q => q.Proveedor));
            ClasesCompartidas.remitosDetalle.DataSource = await unitOfWork.RemitoDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC));
            //
            Close();
        }

        private async void CancelarRemito()
        {
            var productosList = ClasesCompartidas.productosList;
            var remitoDetalleList = ClasesCompartidas.RemitosDetalle;
            //
            foreach (Producto item in productosList)
            {
                if (item.Carrito > 0)
                {
                    var producto = unitOfWork.ProductoRepository.GetByID(item.Id);
                    producto.Carrito = 0;
                    //
                    try
                    {
                        new ModelsValidator().Validate(producto);
                        //
                        unitOfWork.ProductoRepository.Update(producto);
                        unitOfWork.Save();
                    }
                    catch (Exception ex)
                    {
                        Debug.Print(ex.Message);
                        Debug.Print(ex.InnerException?.Message);
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            //
            foreach (RemitoDetalle item in remitoDetalleList)
            {
                var remitoDetalle = unitOfWork.RemitoDetalleRepository.GetByID(item.Id);
                //
                try
                {
                    new ModelsValidator().Validate(remitoDetalle);
                    //
                    unitOfWork.RemitoDetalleRepository.Delete(remitoDetalle);
                    unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                    Debug.Print(ex.InnerException?.Message);
                    MessageBox.Show(ex.Message);
                }
            }
            //
            ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
            ClasesCompartidas.RemitosDetalle.DataSource = await unitOfWork.RemitoDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC), filter: q => q.RemitoId == null);
            //
            Close();
        }

        private async void CambiarPrecioBulto()
        {
            if (selection == true)
            {
                var productoNewPrecio = unitOfWork.ProductoRepository.GetByID(idproducto);
                //
                var precioBulto = productoNewPrecio.PVP * productoNewPrecio.CantidadBulto;
                var precioNuevo = precioBulto - ((precioBulto * descuento) / 100);
                //
                productoNewPrecio.PrecioBulto = precioNuevo;
                productoNewPrecio.Descuento = descuento;
                //
                try
                {
                    new ModelsValidator().Validate(productoNewPrecio);
                    //
                    {
                        unitOfWork.ProductoRepository.Update(productoNewPrecio);
                        unitOfWork.Save();
                    }

                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                    Debug.Print(ex.InnerException?.Message);
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                var listProductos = ClasesCompartidas.FilterSend;
                //
                if (filter == true)
                {
                    listProductos = ClasesCompartidas.FilterSend;
                }
                else
                {
                    listProductos = ClasesCompartidas.productosList;
                }
                //
                foreach (Producto item in listProductos)
                {
                    var IdProducto = Convert.ToInt32(item.Id);
                    //
                    var productoNewPrecio = unitOfWork.ProductoRepository.GetByID(IdProducto);
                    //
                    var precioBulto = productoNewPrecio.PVP * productoNewPrecio.CantidadBulto;
                    var precioNuevo = precioBulto - ((precioBulto * descuento) / 100);
                    //
                    productoNewPrecio.PrecioBulto = precioNuevo;
                    productoNewPrecio.Descuento = descuento;
                    //
                    try
                    {
                        new ModelsValidator().Validate(productoNewPrecio);
                        //
                        unitOfWork.ProductoRepository.Update(productoNewPrecio);
                        unitOfWork.Save();
                    }
                    catch (Exception ex)
                    {
                        Debug.Print(ex.Message);
                        Debug.Print(ex.InnerException?.Message);
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            //
            ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
            Close();
        }

        private async void CambiarPrecioProductos()
        {
            if (selection == true)
            {
                var productoNewPrecio = unitOfWork.ProductoRepository.GetByID(idproducto);
                //
                var precioBulto = productoNewPrecio.PVP * productoNewPrecio.CantidadBulto;
                var precioNuevo = precioBulto - ((precioBulto * descuento) / 100);
                //
                productoNewPrecio.PVP = precioNuevo;
                productoNewPrecio.Ganancia = ganancia;
                //
                try
                {
                    new ModelsValidator().Validate(productoNewPrecio);
                    //
                    {
                        unitOfWork.ProductoRepository.Update(productoNewPrecio);
                        unitOfWork.Save();
                    }

                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                    Debug.Print(ex.InnerException?.Message);
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                var listProductos = ClasesCompartidas.FilterSend;
                //
                if (filter == true)
                {
                    listProductos = ClasesCompartidas.FilterSend;
                }
                else
                {
                    listProductos = ClasesCompartidas.productosList;
                }
                //
                foreach (Producto item in listProductos)
                {
                    var IdProducto = Convert.ToInt32(item.Id);
                    //
                    var productoNewPrecio = unitOfWork.ProductoRepository.GetByID(IdProducto);
                    //
                    var precioNuevo = productoNewPrecio.PrecioUnidad + ((productoNewPrecio.PrecioUnidad * ganancia) / 100);
                    productoNewPrecio.PVP = precioNuevo;
                    productoNewPrecio.Ganancia = ganancia;
                    //
                    try
                    {
                        new ModelsValidator().Validate(productoNewPrecio);
                        //
                        unitOfWork.ProductoRepository.Update(productoNewPrecio);
                        unitOfWork.Save();
                    }
                    catch (Exception ex)
                    {
                        Debug.Print(ex.Message);
                        Debug.Print(ex.InnerException?.Message);
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            //
            ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
            Close();
        }

        private async void PagarDeudaEspecifica()
        {
            var comprasDeudas = await unitOfWork.VentaRepository.GetAllAsync(include: q => q.Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Estado == "Deuda" && q.CuentaId.Equals(cuentaId));
            var productosDeudas = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Pagado == "No" && q.CuentaId.Equals(cuentaId));
            //
            var cuentaActual = unitOfWork.CuentaRepository.GetByID(cuentaId);
            cuentaActual.Deuda = cuentaActual.Deuda - importe;
            cuentaActual.Saldo = 0;
            //
            try
            {
                new ModelsValidator().Validate(cuentaActual);
                //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (respuesta == DialogResult.Yes)
                //{
                unitOfWork.CuentaRepository.Update(cuentaActual);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                Debug.Print(ex.InnerException?.Message);
                MessageBox.Show(ex.Message);
            }
            //
            Pago pago = new Pago()
            {
                Importe = importe,
                Dinero = dinero,
                Vuelto = vuelto,
                Tipo = "Específico",
                Fecha = DateTime.Now,
                CuentaId = cuentaId,
                UsuarioId = ClasesCompartidas.UserId
            };
            //
            try
            {
                new ModelsValidator().Validate(pago);
                //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (respuesta == DialogResult.Yes)
                //{
                unitOfWork.PagoRepository.Add(pago);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                Debug.Print(ex.InnerException?.Message);
                MessageBox.Show(ex.Message);
            }
            //
            var pagoid = pago.Id;
            ClasesCompartidas.PagoId = pago.Id;
            //
            var deudaPagada = unitOfWork.VentaRepository.GetByID(iddeuda);
            deudaPagada.Estado = "Pagado";
            deudaPagada.PagoId = pagoid;
            //
            try
            {
                new ModelsValidator().Validate(deudaPagada);
                //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (respuesta == DialogResult.Yes)
                //{
                unitOfWork.VentaRepository.Update(deudaPagada);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                Debug.Print(ex.InnerException?.Message);
                MessageBox.Show(ex.Message);
            }
            //
            foreach (VentaDetalle producto in productosDeudas)
            {
                int codigoVenta = producto.CodigoDeVenta;
                if (codigoVenta == codigoVespecifico)
                {
                    var productoPagado = unitOfWork.VentaDetalleRepository.GetByID(producto.Id);
                    productoPagado.Pagado = "Si";
                    //
                    try
                    {
                        new ModelsValidator().Validate(productoPagado);
                        //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        //if (respuesta == DialogResult.Yes)
                        //{
                        unitOfWork.VentaDetalleRepository.Update(productoPagado);
                        unitOfWork.Save();
                    }
                    catch (Exception ex)
                    {
                        Debug.Print(ex.Message);
                        Debug.Print(ex.InnerException?.Message);
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            //
            ClasesCompartidas.ComprasList.DataSource = await unitOfWork.VentaRepository.GetAllAsync(include: q => q.Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Estado == "Pagado" && q.CuentaId.Equals(cuentaId));
            ClasesCompartidas.ProductosList.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Pagado == "Si" && q.CuentaId.Equals(cuentaId));
            ClasesCompartidas.PagosList.DataSource = await unitOfWork.PagoRepository.GetAllAsync(include: q => q.Include(q => q.Cuenta).Include(q => q.Usuario), filter: q => q.CuentaId.Equals(cuentaId));
            //
            ClasesCompartidas.ComprasDeudaList.DataSource = await unitOfWork.VentaRepository.GetAllAsync(include: q => q.Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Estado == "Deuda" && q.CuentaId.Equals(cuentaId));
            ClasesCompartidas.ProductosDeudaList.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Pagado == "No" && q.CuentaId.Equals(cuentaId));
            // PagosDeudaList.DataSource PENDIENTE
            //
            Close();
        }

        private async void PagarDeudaParcial()
        {
            var comprasDeudas = await unitOfWork.VentaRepository.GetAllAsync(include: q => q.Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Estado == "Deuda" && q.CuentaId.Equals(cuentaId));
            var productosDeudas = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Pagado == "No" && q.CuentaId.Equals(cuentaId));
            //
            var cuentaActual = unitOfWork.CuentaRepository.GetByID(cuentaId);
            cuentaActual.Deuda = cuentaActual.Deuda - importe;
            //
            if (saldoVuelto == true)
                cuentaActual.Saldo = vuelto;
            else
                cuentaActual.Saldo = 0;
            //
            try
            {
                new ModelsValidator().Validate(cuentaActual);
                //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (respuesta == DialogResult.Yes)
                //{
                unitOfWork.CuentaRepository.Update(cuentaActual);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                Debug.Print(ex.InnerException?.Message);
                MessageBox.Show(ex.Message);
            }
            //
            decimal pagoVuelto;
            if (saldoVuelto == true)
            {
                pagoVuelto = 0;
            }
            else
            {
                pagoVuelto = vuelto;
            }
            //
            Pago pago = new Pago()
            {
                Importe = importe,
                Dinero = dinero,
                Vuelto = pagoVuelto,
                Tipo = "Parcial",
                Fecha = DateTime.Now,
                CuentaId = cuentaId,
                UsuarioId = ClasesCompartidas.UserId
            };
            //
            try
            {
                new ModelsValidator().Validate(pago);
                //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (respuesta == DialogResult.Yes)
                //{
                unitOfWork.PagoRepository.Add(pago);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                Debug.Print(ex.InnerException?.Message);
                MessageBox.Show(ex.Message);
            }
            //
            var pagoid = pago.Id;
            ClasesCompartidas.PagoId = pago.Id;
            //
            foreach (Venta deuda in comprasDeudas)
            {
                if (dinero > deuda.Importe)
                {
                    dinero = dinero - deuda.Importe;
                    //
                    var deudaPagada = unitOfWork.VentaRepository.GetByID(deuda.Id);
                    deudaPagada.Estado = "Pagado";
                    deudaPagada.PagoId = pagoid;
                    //
                    try
                    {
                        new ModelsValidator().Validate(deudaPagada);
                        //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        //if (respuesta == DialogResult.Yes)
                        //{
                        unitOfWork.VentaRepository.Update(deudaPagada);
                        unitOfWork.Save();
                    }
                    catch (Exception ex)
                    {
                        Debug.Print(ex.Message);
                        Debug.Print(ex.InnerException?.Message);
                        MessageBox.Show(ex.Message);
                    }
                    //
                    foreach (VentaDetalle producto in productosDeudas)
                    {
                        if (producto.CodigoDeVenta == deuda.Codigo)
                        {
                            var productoPagado = unitOfWork.VentaDetalleRepository.GetByID(producto.Id);
                            productoPagado.Pagado = "Si";
                            //
                            try
                            {
                                new ModelsValidator().Validate(productoPagado);
                                //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                //if (respuesta == DialogResult.Yes)
                                //{
                                unitOfWork.VentaDetalleRepository.Update(productoPagado);
                                unitOfWork.Save();
                            }
                            catch (Exception ex)
                            {
                                Debug.Print(ex.Message);
                                Debug.Print(ex.InnerException?.Message);
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
            }
            //
            ClasesCompartidas.ComprasList.DataSource = await unitOfWork.VentaRepository.GetAllAsync(include: q => q.Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Estado == "Pagado" && q.CuentaId.Equals(cuentaId));
            ClasesCompartidas.ProductosList.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Pagado == "Si" && q.CuentaId.Equals(cuentaId));
            ClasesCompartidas.PagosList.DataSource = await unitOfWork.PagoRepository.GetAllAsync(include: q => q.Include(q => q.Cuenta).Include(q => q.Usuario), filter: q => q.CuentaId.Equals(cuentaId));
            //
            ClasesCompartidas.ComprasDeudaList.DataSource = await unitOfWork.VentaRepository.GetAllAsync(include: q => q.Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Estado == "Deuda" && q.CuentaId.Equals(cuentaId));
            ClasesCompartidas.ProductosDeudaList.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Pagado == "No" && q.CuentaId.Equals(cuentaId));
            // PagosDeudaList.DataSource PENDIENTE
            //
            Close();
        }

        private async void AñadirSaldo()
        {
            var cuentaActual = unitOfWork.CuentaRepository.GetByID(cuentaId);
            cuentaActual.Saldo = cuentaActual.Saldo + dinero;
            try
            {
                new ModelsValidator().Validate(cuentaActual);
                //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (respuesta == DialogResult.Yes)
                //{
                unitOfWork.CuentaRepository.Update(cuentaActual);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                Debug.Print(ex.InnerException?.Message);
                MessageBox.Show(ex.Message);
            }
            //
            Pago pago = new Pago()
            {
                Importe = 0,
                Dinero = dinero,
                Vuelto = 0,
                Tipo = "Saldo",
                Fecha = DateTime.Now,
                CuentaId = cuentaId,
                UsuarioId = ClasesCompartidas.UserId
            };
            try
            {
                new ModelsValidator().Validate(pago);
                //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (respuesta == DialogResult.Yes)
                //{
                unitOfWork.PagoRepository.Add(pago);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                Debug.Print(ex.InnerException?.Message);
                MessageBox.Show(ex.Message);
            }
            ClasesCompartidas.SaldoId = pago.Id;
            //
            ClasesCompartidas.PagosList.DataSource = await unitOfWork.PagoRepository.GetAllAsync(include: q => q.Include(q => q.Cuenta).Include(q => q.Usuario), filter: q => q.CuentaId.Equals(cuentaId));
            ClasesCompartidas.cuentasList.DataSource = await unitOfWork.CuentaRepository.GetAllAsync(include: c => c.Include(c => c.Usuario).Include(c => c.Provincia).Include(c => c.Localidad), filter: v => v.Visible.Equals(true));
            //
            Close();
        }

        private async void PagarDeudaTotal()
        {
            var comprasDeudas = await unitOfWork.VentaRepository.GetAllAsync(include: q => q.Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Estado == "Deuda" && q.CuentaId.Equals(cuentaId));
            var productosDeudas = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Pagado == "No" && q.CuentaId.Equals(cuentaId));
            //
            var cuentaActual = unitOfWork.CuentaRepository.GetByID(cuentaId);
            cuentaActual.Deuda = 0;
            cuentaActual.Saldo = 0;
            try
            {
                new ModelsValidator().Validate(cuentaActual);
                //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (respuesta == DialogResult.Yes)
                //{
                unitOfWork.CuentaRepository.Update(cuentaActual);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                Debug.Print(ex.InnerException?.Message);
                MessageBox.Show(ex.Message);
            }
            //
            Pago pago = new Pago()
            {
                Importe = importe,
                Dinero = dinero,
                Vuelto = vuelto,
                Tipo = "Total",
                Fecha = DateTime.Now,
                CuentaId = cuentaId,
                UsuarioId = ClasesCompartidas.UserId
            };
            try
            {
                new ModelsValidator().Validate(pago);
                //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (respuesta == DialogResult.Yes)
                //{
                unitOfWork.PagoRepository.Add(pago);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                Debug.Print(ex.InnerException?.Message);
                MessageBox.Show(ex.Message);
            }
            //
            var pagoid = pago.Id;
            ClasesCompartidas.PagoId = pago.Id;
            //
            foreach (Venta venta in comprasDeudas)
            {
                var deudaPagada = unitOfWork.VentaRepository.GetByID(venta.Id);
                deudaPagada.Estado = "Pagado";
                deudaPagada.PagoId = pagoid;
                //
                try
                {
                    new ModelsValidator().Validate(deudaPagada);
                    //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //if (respuesta == DialogResult.Yes)
                    //{
                    unitOfWork.VentaRepository.Update(deudaPagada);
                    unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                    Debug.Print(ex.InnerException?.Message);
                    MessageBox.Show(ex.Message);
                }
            }
            //
            foreach (VentaDetalle producto in productosDeudas)
            {
                var productoPagado = unitOfWork.VentaDetalleRepository.GetByID(producto.Id);
                productoPagado.Pagado = "Si";
                //
                try
                {
                    new ModelsValidator().Validate(productoPagado);
                    //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //if (respuesta == DialogResult.Yes)
                    //{
                    unitOfWork.VentaDetalleRepository.Update(productoPagado);
                    unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                    Debug.Print(ex.InnerException?.Message);
                    MessageBox.Show(ex.Message);
                }
            }
            //
            ClasesCompartidas.ComprasList.DataSource = await unitOfWork.VentaRepository.GetAllAsync(include: q => q.Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Estado == "Pagado" && q.CuentaId.Equals(cuentaId));
            ClasesCompartidas.ProductosList.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Pagado == "Si" && q.CuentaId.Equals(cuentaId));
            ClasesCompartidas.PagosList.DataSource = await unitOfWork.PagoRepository.GetAllAsync(include: q => q.Include(q => q.Cuenta).Include(q => q.Usuario), filter: q => q.CuentaId.Equals(cuentaId));
            ClasesCompartidas.cuentasList.DataSource = await unitOfWork.CuentaRepository.GetAllAsync(include: c => c.Include(c => c.Usuario).Include(c => c.Provincia).Include(c => c.Localidad), filter: v => v.Visible.Equals(true));
            //
            ClasesCompartidas.ComprasDeudaList.DataSource = await unitOfWork.VentaRepository.GetAllAsync(include: q => q.Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Estado == "Deuda" && q.CuentaId.Equals(cuentaId));
            ClasesCompartidas.ProductosDeudaList.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Pagado == "No" && q.CuentaId.Equals(cuentaId));
            // PagosDeudaList.DataSource PENDIENTE
            //
            Close();
        }

        private async void AñadirVentaCuenta()
        {
            int VentaId = 0;
            //
            int IdProducto = 0;
            //
            int IdVentaDetalle = 0;
            //
            int _codigoVenta = 0;
            //
            var ventaDetalleList = ClasesCompartidas.VentaDetalle;
            var productoList = ClasesCompartidas.productosList;
            //
            foreach (Producto itemProducto in productoList)
            {
                    if (Convert.ToInt32(itemProducto.Carrito) > 0)
                    {
                        IdProducto = (int)itemProducto.Id;
                    }
                    //
                    if (IdProducto != 0)
                    {
                        var productoVendido = unitOfWork.ProductoRepository.GetByID(IdProducto);
                        productoVendido.Carrito = 0;
                        //
                        try
                        {
                            new ModelsValidator().Validate(productoVendido);
                            //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            //if (respuesta == DialogResult.Yes)
                            //{
                            unitOfWork.ProductoRepository.Update(productoVendido);
                            unitOfWork.Save();
                            IdProducto = 0;
                        }
                        catch (Exception ex)
                        {
                            Debug.Print(ex.Message);
                            Debug.Print(ex.InnerException?.Message);
                            MessageBox.Show(ex.Message);
                        }
                    }
            }
            //
            var oldCodigoVenta = unitOfWork.CodigoVentaRepository.GetByID(1);
            _codigoVenta = oldCodigoVenta.Codigo + 1;
            //
            CodigoVenta codigoVenta = new CodigoVenta()
            {
                Id = 1,
                Codigo = _codigoVenta
            };
            try
            {
                new ModelsValidator().Validate(codigoVenta);
                //
                unitOfWork.CodigoVentaRepository.Update(codigoVenta);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                Debug.Print(ex.InnerException?.Message);
                MessageBox.Show(ex.Message);
            }
            //
            Venta venta = new Venta()
            {
                Importe = precioAcum,
                Articulos = cantidad,
                CuentaId = cuentaId,
                Estado = "Deuda",
                Fecha = DateTime.Now,
                Codigo = _codigoVenta,
                UsuarioId = ClasesCompartidas.UserId,
                PagoId = null
            };
            try
            {
                new ModelsValidator().Validate(venta);
                //
                //
                unitOfWork.VentaRepository.Add(venta);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                Debug.Print(ex.InnerException?.Message);
                MessageBox.Show(ex.Message);
            }
            VentaId = venta.Id;
            //
            foreach (VentaDetalle itemVentaDetalle in ventaDetalleList)
            {
                for (int v = 0; v < rowsCount; v++)
                {

                }
                if (Convert.ToString(itemVentaDetalle.Pagado) == "Carrito")
                {
                    IdVentaDetalle = (int)itemVentaDetalle.Id;
                }
                //
                if (IdVentaDetalle != 0)
                {
                    var ventaDetalle = unitOfWork.VentaDetalleRepository.GetByID(IdVentaDetalle);
                    ventaDetalle.Pagado = "No";
                    ventaDetalle.CuentaId = cuentaId;
                    ventaDetalle.UsuarioId = (int)ClasesCompartidas.UserId;
                    ventaDetalle.FechaDePago = DateTime.Now;
                    ventaDetalle.CodigoDeVenta = _codigoVenta;
                    ventaDetalle.VentaId = VentaId;
                    //                    
                    try
                    {
                        new ModelsValidator().Validate(ventaDetalle);
                        //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        //if (respuesta == DialogResult.Yes)
                        //{
                        unitOfWork.VentaDetalleRepository.Update(ventaDetalle);
                        unitOfWork.Save();
                        IdVentaDetalle = 0;
                    }
                    catch (Exception ex)
                    {
                        Debug.Print(ex.Message);
                        Debug.Print(ex.InnerException?.Message);
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            //
            // Es indispensable añadir el precio acumulado a la cuenta a la que se le ha agregado la deuda
            var cuenta = unitOfWork.CuentaRepository.GetByID(cuentaId);
            //
            if (cuenta.Deuda == null)
            {
                cuenta.Deuda = precioAcum;
            }
            else
            {
                cuenta.Deuda = cuenta.Deuda + precioAcum;
            }
            //
            try
            {
                new ModelsValidator().Validate(cuenta);
                //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (respuesta == DialogResult.Yes)
                //{
                unitOfWork.CuentaRepository.Update(cuenta);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                Debug.Print(ex.InnerException?.Message);
                MessageBox.Show(ex.Message);
            }
            //
            ClasesCompartidas.cuentasList.DataSource = await unitOfWork.CuentaRepository.GetAllAsync(include: c => c.Include(c => c.Usuario).Include(c => c.Provincia).Include(c => c.Localidad), filter: v => v.Visible.Equals(true));
            ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
            ClasesCompartidas.VentaDetalle.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor), filter: q => q.Pagado == "Carrito");
            //
            Close();
        }
    }
}
