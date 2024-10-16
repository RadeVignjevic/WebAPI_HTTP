namespace WebAPI_HTTP.Extensions
{
    public static class LoggerExtension
    {
        public static void LogError(this ILogger logger, Exception ex, string message)
        {
            logger.LogError(ex, message);
        }

        public static void LogCustomInformation(this ILogger logger, string message)
        {
            // Check for null to avoid NullReferenceException
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            // Call the actual LogInformation method from the ILogger interface
            logger.LogInformation(message);
        }
    }
}
