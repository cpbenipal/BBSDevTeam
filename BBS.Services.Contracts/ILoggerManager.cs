
namespace BBS.Services.Contracts
{
    public interface ILoggerManager
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError_Wrapper(string api, string errorMessage);
        void LogError(Exception ex);
        void LogError(string api, string errorCode, string errorMessage);
        void LogError(string api, string errorCode, string errorMessage, string errorCommendation, string errorRecordId);

    }
}
