using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarsSale.WebUi.Exceptions
{
    public class AccountValidationException: AccountException
    {
        public AccountValidationException(string message)
            : base(message) { }
    }
}