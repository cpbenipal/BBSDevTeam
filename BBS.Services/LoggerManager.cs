using BBS.Services.Contracts;
using NLog;

namespace BBS.Services.Repository
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        public void LogDebug(string message, int personId)
        {
            logger.Debug("{" + message + " { personId : " + personId + "}, } \n");
        }
        public void LogError(Exception ex, int personId)
        {
            logger.Error(ex, "{" + ExeptionHelper.GetAllErrors(ex) + " { personId : " + personId + "}, } \n");
        }
        public void LogError_Wrapper(string api, string errorMessage, int personId)
        {
            logger.Error("ERROR from .net core wrapper :: " + api + " | Message:" + errorMessage + " | personId:" + personId + "\n");
        }
        public void LogError(string api, string errorCode, string errorMessage, int personId)
        {
            logger.Error("ERROR from Legacy API :: " + api + " | ErrorCode:" + errorCode + " | Message:" + errorMessage + " | personId:" + personId + "\n");
        }
        public void LogError(string api, string errorCode, string errorMessage, string errorCommendation, string errorRecordId, int personId)
        {
            logger.Error("ERROR from Legacy API :: " + api + " | ErrorCode:" + errorCode + " | Message:" + errorMessage
                + (errorCommendation != null ? " | ERRORRECOMMENDATION:" + errorCommendation : "")
                + (errorRecordId != null ? " | ERRORRECORDID:" + errorRecordId : "" + " | personId:" + personId + "\n"));
        }
        public void LogInfo(string message, int personId)
        {
            logger.Info("{" + message + " { personId : " + personId + "}, } \n");
        }
        public void LogWarn(string message, int personId)
        {
            logger.Warn("{" + message + " { personId : " + personId + "}, } \n");
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

        public static string GetAllErrors(this Exception exception)
        {
            var messages = exception.FromHierarchy(ex => ex.InnerException!)
                .Select(ex => ex.Message);
            return String.Join(Environment.NewLine, messages);
        }
    }

}