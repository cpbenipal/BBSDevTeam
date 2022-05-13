using BBS.Constants;
using Microsoft.AspNetCore.Http;

namespace BBS.Services.Contracts
{
    public interface IFileUploadService
    {
        BlobFiles UploadFileToBlob(IFormFile files, List<string> validExtensions);
        Task<List<string>> DownloadBlob(string downloadPath);
        BlobFiles UploadCertificate(string content);
        string GetFilePublicUri(string fileName);
    }
}
