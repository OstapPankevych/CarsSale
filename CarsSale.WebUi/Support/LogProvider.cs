using log4net;

namespace CarsSale.WebUi.Support
{
    public static class LogProvider
    {
        public static ILog Logger { get; private set; }

        static LogProvider()
        {
            Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }
    }
}