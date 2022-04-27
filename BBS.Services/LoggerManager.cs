using BBS.Services.Contracts;
using NLog;

namespace BBS.Services.Repository
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        public void LogDebug(string message)
        {
            logger.Debug(message);
        }
        public void LogError(Exception ex)
        {
            logger.Error(ex, ExeptionHelper.GetaAllErrors(ex));
        }
        public void LogError_Wrapper(string api, string errorMessage)
        {
            logger.Error("ERROR from .net core wrapper :: " + api + " | Message:" + errorMessage);
        }
        public void LogError(string api, string errorCode, string errorMessage)
        {
            logger.Error("ERROR from Legacy API :: " + api + " | ErrorCode:" + errorCode + " | Message:" + errorMessage);
        }
        public void LogError(string api, string errorCode, string errorMessage, string errorCommendation, string errorRecordId)
        {
            logger.Error("ERROR from Legacy API :: " + api + " | ErrorCode:" + errorCode + " | Message:" + errorMessage
                + (errorCommendation != null ? " | ERRORRECOMMENDATION:" + errorCommendation : "")
                + (errorRecordId != null ? " | ERRORRECORDID:" + errorRecordId : ""));
        }
        public void LogInfo(string message)
        {
            logger.Info(message);
        }
        public void LogWarn(string message)
        {
            logger.Warn(message);
        }
    }

    public static class ExeptionHelper
    {
        // all error checking left out for brevity

        // a.k.a., linked list style enumerator
        public static IEnumerable<TSource> FromHierarchy<TSource>(
            this TSource source,
            Func<TSource, TSource> nextItem,
            Func<TSource, bool> canContinue)
        {
            for (var current = source; canContinue(current); current = nextItem(current))
            {
                yield return current;
            }
        }

        public static IEnumerable<TSource> FromHierarchy<TSource>(
            this TSource source,
            Func<TSource, TSource> nextItem)
            where TSource : class
        {
            return FromHierarchy(source, nextItem, s => s != null);
        }

        public static string GetaAllErrors(this Exception exception)
        {
            var messages = exception.FromHierarchy(ex => ex.InnerException!)
                .Select(ex => ex.Message);
            return String.Join(Environment.NewLine, messages);
        }
    }

}