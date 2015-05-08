using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Web
{
    public class AppConfig
    {
        public static string ApplicationName
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ApplicationName"];
            }
        }

        public static string ClientId
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ClientId"];
            }
        }

        public static string APIUrlBase
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["APIUrlBase"];
            }
        }

        public static string UrlBase
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["UrlBase"];
            }
        }

    }
}
