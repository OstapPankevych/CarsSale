using System;
using CarsSale.WebUi.Exceptions;
using log4net;

namespace CarsSale.WebUi.Logger
{
    public class Logger: ILogger
    {
        private readonly ILog _logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Log(Exception ex)
        {
            if (ex is AccountValidationException)
            {
                _logger.Warn(ex.Message);
            }
            else if (ex is AccountException)
            {
                _logger.Warn(ex.Message);
            }
            else if (ex is AdvertisementException)
            {
                _logger.Warn(ex.Message);
            }
            else
            {
                _logger.Error(ex.Message);
            }
        }
    }
}
