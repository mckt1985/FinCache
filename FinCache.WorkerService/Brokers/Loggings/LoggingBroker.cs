using Serilog;
using System.Diagnostics;

namespace FinCache.WorkerService.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
    {
        private readonly Serilog.ILogger logger;

        public LoggingBroker(IConfiguration configuration)
        {
            var logpath = configuration["LogPath"];

            logger = new LoggerConfiguration()
             .WriteTo.Console()
             .WriteTo.File($"{logpath}/fincache.workerservice.log", rollingInterval: RollingInterval.Day).CreateLogger();

            Serilog.Debugging.SelfLog.Enable(msg =>
                Debug.WriteLine(msg));
        }

        public void LogInformation(string message)
        {
            this.logger.Information(message);
        }

        public void LogTrace(string message)
        {
            this.logger.Verbose(message);
        }

        public void LogDebug(string message)
        {
            this.logger.Debug(message);
        }

        public void LogWarning(string message)
        {
            this.logger.Warning(message);
        }

        public void LogError(Exception exception)
        {
            this.logger.Error(exception.Message, exception);
        }

        public void LogCritical(Exception exception)
        {
            this.logger.Fatal(exception, exception.Message);
        }
    }
}
