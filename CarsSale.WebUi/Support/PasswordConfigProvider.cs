using System.Configuration;

namespace CarsSale.WebUi.Support
{
    public static class PasswordConfigProvider
    {
        private static PasswordConfig Config => (PasswordConfig) ConfigurationManager.GetSection("PasswordConfig");

        public static int RequiredLength => int.Parse(Config.RequiredLength);
        public static bool RequireNonLetterOrDigit => bool.Parse(Config.RequireNonLetterOrDigit);

        public static bool RequireDigit => bool.Parse(Config.RequireDigit);
        public static bool RequireLowercase => bool.Parse(Config.RequireLowercase);
        public static bool RequireUppercase => bool.Parse(Config.RequireUppercase);
    }
}