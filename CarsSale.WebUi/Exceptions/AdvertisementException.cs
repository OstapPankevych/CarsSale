using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarsSale.WebUi.Exceptions
{
    public class AdvertisementException: Exception
    {
        public AdvertisementException(string message)
            : base(message) { }
    }
}