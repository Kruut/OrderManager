using log4net;

namespace OrderManager
{
    internal class Logger
    {
        private static ILog Instance;

        public static ILog GetLogger()
        {
            if (Instance is null) 
                Instance = LogManager.GetLogger("logger");
            return Instance;
        }
    }
}
