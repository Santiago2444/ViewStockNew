using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ViewStockNew.Interfaces;
using ViewStockNew.Models;
using ViewStockNew.Utils;

namespace ViewStockNew.Views
{
    public partial class CreateDataView : Form
    {
        IUnitOfWork unitOfWork;
        string dataRecibed;
        bool editando;
        public CreateDataView(string DataRecibed, IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            this.dataRecibed = DataRecibed;
            this.unitOfWork = unitOfWork;

            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.BtnGuardar, "Guardar");
            //
            System.Windows.Forms.ToolTip ToolTip2 = new System.Windows.Forms.ToolTip();
            ToolTip2.SetToolTip(this.iconButton2, "Cancelar");
       


            if (dataRecibed == "specs")
            {
                LblTitle.Text = "Nueva SPEC";
            }
            else if (dataRecibed == "tipoDeProductos")
            {
                LblTitle.Text = "Nuevo Tipo";
            }
            else if (dataRecibed == "marcas")
            {
                LblTitle.Text = "Nueva Marca";
            }
            else if (dataRecibed == "provincia")
            {
                LblTitle.Text = "Nueva Provincia";
            }
            else if (dataRecibed == "localidad")
            {
                LblTitle.Text = "Nueva Localidad";
            }
        }

        private void CreateDataView_Load(object sender, EventArgs e)
        {

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (dataRecibed == "tipoDeProductos")
            {
                #region CargaDeNuevosTipoDeProducto
                TipoProducto tipoProducto = new TipoProducto()
                {
                    Nombre = TxtNombre.Text,
                    Modificacion = DateTime.Now,
                    Visible = true
                };

                bool tipoProductoExistente = unitOfWork.TipoProductoRepository.dbSet.Any(n => n.Nombre.Equals(tipoProducto.Nombre));

                if (!tipoProductoExistente)
                {
                    try
                    {
                        new ModelsValidator().Validate(tipoProducto);

                        DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar este nuevo Tipo de Producto?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta == DialogResult.Yes)
                        {
                            unitOfWork.TipoProductoRepository.Add(tipoProducto);
                            unitOfWork.Save();
                            //
                            ClasesCompartidas.TipoNuevo = TxtNombre.Text;
                            //
                            ClasesCompartidas.tiposProductosList.DataSource = await unitOfWork.TipoProductoRepository.GetAllAsync();
                            MessageBox.Show("¡El Tipo de Producto fue guardado con éxito!", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Close();
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
                    MessageBox.Show("El tipo de Producto que intenta agregar, ya se encuentra cargada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                #endregion
            }
            else if (dataRecibed == "marcas")
            {
                #region CargaDeNuevasMarcas
                Marca marcas = new Marca()
                {
                    Nombre = TxtNombre.Text,
                    Modificacion = DateTime.Now,
                    Visible = true
                };

                bool marcaExistente = unitOfWork.MarcaRepository.dbSet.Any(n => n.Nombre.Equals(marcas.Nombre));

                if (!marcaExistente)
                {
                    try
                    {
                        new ModelsValidator().Validate(marcas);

                        DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta nueva Marca?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta == DialogResult.Yes)
                        {
                            unitOfWork.MarcaRepository.Add(marcas);
                            unitOfWork.Save();
                            //
                            ClasesCompartidas.MarcaNueva = TxtNombre.Text;
                            //
                            ClasesCompartidas.marcasList.DataSource = await unitOfWork.MarcaRepository.GetAllAsync();
                            MessageBox.Show("¡La Marca fue guardada con éxito!", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Close();
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
                    //Si existe en la lista de Marcas, se muestra un mensaje de advertencia  y no es cargada
                    MessageBox.Show("La Marca que intenta agregar, ya se encuentra cargada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                #endregion
            }
            else if (dataRecibed == "specs")
            {
                #region CargaDeNuevoSPEC
                SPEC spec = new SPEC()
                {
                    Nombre = TxtNombre.Text,
                    Modificacion = DateTime.Now,
                    Visible = true
                };
                //
                bool specExistente = unitOfWork.SPECRepository.dbSet.Any(n => n.Nombre.Equals(spec.Nombre));
                //
                if (!specExistente)
                {
                    try
                    {
                        new ModelsValidator().Validate(spec);

                        DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta nueva Especificación?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta == DialogResult.Yes)
                        {
                            unitOfWork.SPECRepository.Add(spec);
                            unitOfWork.Save();
                            //
                            ClasesCompartidas.SpecNuevo = TxtNombre.Text;
                            //
                            ClasesCompartidas.specsList.DataSource = await unitOfWork.SPECRepository.GetAllAsync();
                            MessageBox.Show("¡La Especificación fue guardada con éxito!", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Close();
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
                    MessageBox.Show("La Espicificación que intenta agregar, ya se encuentra cargada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                #endregion

            }
            else if (dataRecibed == "provincia")
            {
                #region CargaDeNuevaProvincia
                Provincia provincia = new Provincia()
                {
                    Nombre = TxtNombre.Text,
                    Modificacion = DateTime.Now,
                    Visible = true
                };
                //
                bool provinciaExistente = unitOfWork.ProvinciaRepository.dbSet.Any(n => n.Nombre.Equals(provincia.Nombre));
                //
                if (!provinciaExistente)
                {
                    try
                    {
                        new ModelsValidator().Validate(provincia);

                        DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta nueva Provincia?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta == DialogResult.Yes)
                        {
                            unitOfWork.ProvinciaRepository.Add(provincia);
                            unitOfWork.Save();
                            //
                            ClasesCompartidas.ProvinciaNueva = TxtNombre.Text;
                            //
                            ClasesCompartidas.provinciasList.DataSource = await unitOfWork.ProvinciaRepository.GetAllAsync();
                            MessageBox.Show("¡La Provincia fue guardada con éxito!", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Close();
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
                    MessageBox.Show("La Provincia que intenta agregar, ya se encuentra cargada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                #endregion
            }
            else if (dataRecibed == "localidad")
            {
                #region CargaDeNuevaLocalidad
                Localidad localidad = new Localidad()
                {
                    Nombre = TxtNombre.Text,
                    Modificacion = DateTime.Now,
                    Visible = true
                };
                //
                bool localidadExistente = unitOfWork.LocalidadRepository.dbSet.Any(n => n.Nombre.Equals(localidad.Nombre));
                //
                if (!localidadExistente)
                {
                    try
                    {
                        new ModelsValidator().Validate(localidad);

                        DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta nueva Localidad?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta == DialogResult.Yes)
                        {
                            unitOfWork.LocalidadRepository.Add(localidad);
                            unitOfWork.Save();
                            //
                            ClasesCompartidas.LocalidadNueva = TxtNombre.Text;
                            //
                            ClasesCompartidas.localidadList.DataSource = await unitOfWork.LocalidadRepository.GetAllAsync();
                            MessageBox.Show("¡La Localidad fue guardada con éxito!", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Close();
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
                    MessageBox.Show("La Localidad que intenta agregar, ya se encuentra cargada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                #endregion

            }
        }
    }
}
