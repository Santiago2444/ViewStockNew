using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Utils
{
    public class Helper
    {
        public static string GetConnectionString()
        {
            var server = ViewStockNew.Properties.Settings1.Default.server;
            var database = ViewStockNew.Properties.Settings1.Default.database;
            var user = ViewStockNew.Properties.Settings1.Default.user;
            var pass = ViewStockNew.Properties.Settings1.Default.pass;
            return $"Server={server};Database={database};Uid={user};Pwd={pass};";
        }
    }
}
