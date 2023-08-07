using AppStock.Models;
using AppStock.ViewModels;
using AppStock.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MySqlConnector;
using static AppStock.Views.Productos;

namespace AppStock.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Productos : ContentPage
	{
        ItemsViewModel _viewModel;

        public IList<list_producto> list_Prodcutos { get; set; }

        public Productos ()
		{
			InitializeComponent ();
            BindingContext = _viewModel = new ItemsViewModel();
            data_list_productos();

        }
        public class list_producto
        {
            public string Nombre { get; set; }
        }
        private void data_list_productos()
        {
            var conexion = new MySqlConnection(Properties.Resources.DBConnection);
            conexion.Open();
            var cmd = new MySqlCommand("SELECT * FROM marcas", conexion);
            var read = cmd.ExecuteReader();
            //
            list_Prodcutos = new List<list_producto>();
            //
            while (read.Read())
            {
                list_Prodcutos.Add(new list_producto
                {
                    Nombre = read.GetString("Nombre").ToString(),
                });
            }
            //
            read.Close();
            //
            ListaProductos.ItemsSource = list_Prodcutos;
        }
    }
}