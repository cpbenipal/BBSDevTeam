using Azure.Core.Pipeline;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BBS.Constants;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Text;

namespace BBS.Services.Repository
{
    public class AzureBlobFileUploadService : IFileUploadService
    {
        private readonly CloudStorageAccount _storageAccount;
        private readonly BlobContainerClient blobContainerClient;
      
        public string _ContainerName { get; set; }
        public AzureBlobFileUploadService(string connectionString, string containerName, int timeSpan )
        {
            _ContainerName = containerName;
            _storageAccount = CloudStorageAccount.Parse(connectionString);
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
                string systemFileName = (Guid.NewGuid().ToString().Replace("-", "") + extension).ToLower();
                blobfile.FileName=systemFileName;
                var blob = blobContainerClient.GetBlobClient(systemFileName);
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
            
            string systemFileName = (Guid.NewGuid().ToString().Replace("-", "") + ".html").ToLower();
            blobfile.FileName = systemFileName;
            var blob = blobContainerClient.GetBlobClient(systemFileName);
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

        public string GetFilePublicUri(string fileName)
        {  
            CloudBlobClient serviceClient = _storageAccount.CreateCloudBlobClient();

            var container = serviceClient.GetContainerReference(_ContainerName);container.CreateIfNotExistsAsync().Wait();

            CloudBlockBlob blob = container.GetBlockBlobReference(fileName);

            SharedAccessBlobPolicy policy = new SharedAccessBlobPolicy();
            // define the expiration time
            policy.SharedAccessExpiryTime = DateTime.UtcNow.AddDays(1);

            // define the permission
            policy.Permissions = SharedAccessBlobPermissions.Read;

            // create signature
            string signature = blob.GetSharedAccessSignature(policy);

            // get full temporary uri
            return blob.Uri + signature;

        }

    }
}