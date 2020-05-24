using NLog;
using NorthwindDatabaseApp.UI;

namespace NorthwindDatabaseApp
{
    public class ManagerConfig
    {
        public IDisplay Display;
        public IInput Input;

        public ManagerConfig()
        {
            Display = new CommandLineDisplay();
            Input = new KeyboardInput(Display);
            
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "logfile.txt" };
            
            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
            
            // Apply config           
            NLog.LogManager.Configuration = config;
        }
    }
}