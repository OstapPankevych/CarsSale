using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CarsSale.WebUi.Support
{
    public class PasswordConfig: ConfigurationSection
    {
        [ConfigurationProperty("requiredLength", IsRequired = true)]
        public string RequiredLength => this["requiredLength"] as string;

        [ConfigurationProperty("requireNonLetterOrDigit", IsRequired = true)]
        public string RequireNonLetterOrDigit => this["requireNonLetterOrDigit"] as string;

        [ConfigurationProperty("requireDigit", IsRequired = true)]
        public string RequireDigit => this["requireDigit"] as string;

        [ConfigurationProperty("requireLowercase", IsRequired = true)]
        public string RequireLowercase => this["requireLowercase"] as string;

        [ConfigurationProperty("requireUppercase", IsRequired = true)]
        public string RequireUppercase => this["requireUppercase"] as string;
    }
}