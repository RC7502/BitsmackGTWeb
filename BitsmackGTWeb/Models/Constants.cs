using System.Configuration;

namespace BitsmackGTWeb.Models
{
    class Constants
    {
        public static string APIURL()
        {
            return ConfigurationManager.AppSettings["APIURL"];
        }

        public static string Token()
        {
            return ConfigurationManager.AppSettings["SecurityToken"];
        }

        public const string ApiPedometer = "api/Pedometer";
    }
}
