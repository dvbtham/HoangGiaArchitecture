using System.Configuration;

namespace HoangGia.Utilities.Helpers
{
    public class ConfigHelper
    {
        public static string GetByKey(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
