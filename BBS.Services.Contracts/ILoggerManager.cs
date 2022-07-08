
namespace BBS.Services.Contracts
{
    public interface ILoggerManager
    {
        void LogInfo(string message, int personId);
        void LogWarn(string message, int personId);
        void LogDebug(string message, int personId);
        void LogError_Wrapper(string api, string errorMessage, int personId);
        void LogError(Exception ex, int personId);
        void LogError(string api, string errorCode, string errorMessage, int personId);
        void LogError(string api, string errorCode, string errorMessage, string errorCommendation, string errorRecordId, int personId);

    }
}
