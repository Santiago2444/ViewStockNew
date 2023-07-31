namespace WebStock.Utils
{
    public class Helper
    {
        public static string GetConnectionString()
        {
            var server = "bvpghk30tjjalsdr4eel-mysql.services.clever-cloud.com";
            var database = "bvpghk30tjjalsdr4eel";
            var user = "uuszol3ba4e9gp4s";
            var pass = "QYy2Uh4gTCyrWe5TsbBm";
            return $"Server={server};Database={database};Uid={user};Pwd={pass};";
        }
    }
}
