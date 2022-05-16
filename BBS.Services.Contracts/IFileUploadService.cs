using BBS.Constants;
using Microsoft.AspNetCore.Http;

namespace BBS.Services.Contracts
{
    public interface IFileUploadService
    {
        BlobFile UploadFileToBlob(IFormFile files, List<string> validExtensions);
        Task<List<string>> DownloadBlob(string downloadPath);
        BlobFile UploadCertificate(string content);
        string GetFilePublicUri(string fileName);
    }
}
