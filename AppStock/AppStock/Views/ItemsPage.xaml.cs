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
using static AppStock.Views.ItemsPage;

namespace AppStock.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;
        public IList<list_cuenta> list_Cuentas { get; set; } 
        public ItemsPage()
        {
            InitializeComponent();
            data_list_cuenta();
            BindingContext = _viewModel = new ItemsViewModel();
        }

        public class list_cuenta { 
            public string Nombre { get; set; }
            public string DNI { get; set; }
            public string Telefono { get; set; }
            public string Domicilio { get; set; }
        }

        private void data_list_cuenta()
        {
            var conexion = new MySqlConnection(Properties.Resources.DBConnection);
            conexion.Open();
            var cmd = new MySqlCommand("SELECT * FROM cuentas WHERE Visible = true", conexion);
            var read = cmd.ExecuteReader();
            //
            list_Cuentas = new List<list_cuenta>();
            //
            while (read.Read()) 
            {
                list_Cuentas.Add(new list_cuenta
                {
                    Nombre = read.GetString("Nombre").ToString(),
                    DNI = read.GetString("DNI").ToString(),
                    Telefono = read.GetString("Telefono").ToString(),
                    Domicilio = read.GetString("Domicilio").ToString(),
                });
            }
            //
            read.Close();
            //
            ListViewCuentas.ItemsSource = list_Cuentas;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}