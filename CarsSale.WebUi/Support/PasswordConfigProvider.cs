using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CarsSale.WebUi.Support
{
    public static class PasswordConfigProvider
    {
        private static PasswordConfig _config;
        private static PasswordConfig Config
        {
            get
            {
                if (_config != null) return _config;
                _config = (PasswordConfig) ConfigurationManager.GetSection("PasswordConfig");
                return _config;
            }
        }

        public static int RequiredLength => int.Parse(Config.RequiredLength);
        public static bool RequireNonLetterOrDigit => bool.Parse(Config.RequireNonLetterOrDigit);

        public static bool RequireDigit => bool.Parse(Config.RequireDigit);
        public static bool RequireLowercase => bool.Parse(Config.RequireLowercase);
        public static bool RequireUppercase => bool.Parse(Config.RequireUppercase);
    }
}