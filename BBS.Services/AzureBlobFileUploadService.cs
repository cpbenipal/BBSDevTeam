using Azure.Core.Pipeline;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BBS.Constants;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace BBS.Services.Repository
{
    public class AzureBlobFileUploadService : IFileUploadService
    {
        private readonly BlobContainerClient blobContainerClient;

        public AzureBlobFileUploadService(string connectionString, string containerName, int timeSpan)
        {

            var blobClientOptions = new BlobClientOptions
            {
                Transport = new HttpClientTransport(new HttpClient { Timeout = TimeSpan.FromMinutes(timeSpan) }),
                Retry = { NetworkTimeout = TimeSpan.FromMinutes(timeSpan) }
            };

            blobContainerClient = new BlobContainerClient(connectionString, containerName, blobClientOptions);
            var result = blobContainerClient.CreateIfNotExists();
            if (result?.GetRawResponse()?.Status == 201)
            {
                Console.WriteLine($"New container {containerName} created.");
            }
        }

        public BlobFiles UploadFileToBlob(IFormFile item, List<string> validFileExtensions)
        {
            var blobfile = new BlobFiles();
            var extension = Path.GetExtension(item.FileName);
            if(validFileExtensions.Contains(extension))
            {
                string systemFileName = Guid.NewGuid() + extension;
                var blob = blobContainerClient.GetBlobClient(systemFileName.Replace("-", "").ToLower());
                using (var memoryStream = new MemoryStream())
                {
                    item.CopyTo(memoryStream);
                    memoryStream.Position = 0;
                    blob.Upload(memoryStream, true);
                }

                blobfile.ContentType = item.ContentType;
                blobfile.ImageUrl = blob.Uri.AbsoluteUri;
                
                return blobfile;
            }
            else
            {
                throw new Exception("Invalid File Format");
            }

        }

        public BlobFiles UploadCertificate(string fileContent)
        {
            var blobfile = new BlobFiles();
            
            string systemFileName = Guid.NewGuid() + ".html";
            var blob = blobContainerClient.GetBlobClient(systemFileName.Replace("-", "").ToLower());
            using (var memoryStream = new MemoryStream())
            {
                byte[] content = new UTF8Encoding(true).GetBytes(fileContent);
                memoryStream.Write(content, 0, content.Length);
                memoryStream.Position = 0;
                blob.Upload(memoryStream, true);
            }

            blobfile.ContentType = "text/html";
            blobfile.ImageUrl = blob.Uri.AbsoluteUri;
            
            return blobfile;
        }

        public async Task<List<string>> DownloadBlob(string downloadFolder)
        {
            var downloadedFiles = new List<string>();
            if (!Directory.Exists(downloadFolder))
                Directory.CreateDirectory(downloadFolder);

            var blobs = blobContainerClient.GetBlobsAsync();
            IAsyncEnumerator<BlobItem> enumerator = blobs.GetAsyncEnumerator();

            while (await enumerator.MoveNextAsync())
            {
                var blob = enumerator.Current;

                Console.WriteLine($"{blob.Name} ({blob.Properties.ContentLength} bytes) Downloading ...", ConsoleColor.Green);
                var blobClient = blobContainerClient.GetBlobClient(blob.Name);

                using (var fileStream = File.Create($"{downloadFolder}\\{blob.Name}"))
                {
                    await blobClient.DownloadToAsync(fileStream);
                }

                Console.WriteLine($"{blob.Name} Downloaded");
                downloadedFiles.Add(blob.Name);
            }

            return downloadedFiles;
        }
    }
}