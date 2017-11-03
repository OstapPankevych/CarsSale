using System;

namespace CarsSale.WebUi.Exceptions
{
    public class AccountException: Exception
    {
        public AccountException(string message):
            base(message) { }
    }
}