using BBS.Constants;
using BBS.Dto;
using Microsoft.AspNetCore.Http;

namespace BBS.Services.Contracts
{
    public interface IFileUploadService
    {
        BlobFiles UploadFileToBlob(IFormFile files);
        Task<List<string>> DownloadBlob(string downloadPath); 
    }
}
