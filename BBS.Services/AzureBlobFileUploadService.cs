using Azure.Core.Pipeline;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BBS.Constants;
using BBS.Services.Contracts;
using CoreHtmlToImage;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BBS.Services.Repository
{
    public class AzureBlobFileUploadService : IFileUploadService
    {
        private readonly CloudStorageAccount _storageAccount;
        private readonly BlobContainerClient blobContainerClient;

        public string ContainerName { get; set; }
        public AzureBlobFileUploadService(string connectionString, string containerName, int timeSpan)
        {
            ContainerName = containerName;
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

        public BlobFile UploadFileToBlob(IFormFile files, List<string> validExtensions)
        {
            var blobfile = new BlobFile();
            var extension = Path.GetExtension(files.FileName);
            if (validExtensions.Contains(extension))
            {
                string systemFileName = (Guid.NewGuid().ToString().Replace("-", "") + extension).ToLower();
                blobfile.FileName = systemFileName;
                var blob = blobContainerClient.GetBlobClient(systemFileName);
                using (var memoryStream = new MemoryStream())
                {
                    files.CopyTo(memoryStream);
                    memoryStream.Position = 0;
                    blob.Upload(memoryStream, true);
                }

                blobfile.ContentType = files.ContentType;
                blobfile.ImageUrl = blob.Uri.AbsoluteUri;
                blobfile.FileName = GetFileName(blob.Uri.AbsoluteUri);
                blobfile.PublicPath = GetFilePublicUri(blob.Uri.AbsoluteUri);
            }
            return blobfile;
        }

        public BlobFile UploadCertificate(string content)
        {
            var blobfile = new BlobFile();

            string systemFileName = (Guid.NewGuid().ToString().Replace("-", "") + ".png").ToLower();
            blobfile.FileName = systemFileName;
            var blob = blobContainerClient.GetBlobClient(systemFileName);

            var converter = new HtmlConverter();

            var fileContent = converter.FromHtmlString(content, 1024, ImageFormat.Png,100);

            using (var memoryStream = new MemoryStream())
            {
                memoryStream.Write(fileContent, 0, fileContent.Length);
                memoryStream.Position = 0;
                blob.Upload(memoryStream, true);
            }
            blobfile.ContentType = "image/png";
            blobfile.ImageUrl = blob.Uri.AbsoluteUri;
            blobfile.FileName = GetFileName(blob.Uri.AbsoluteUri);
            blobfile.PublicPath = GetFilePublicUri(blob.Uri.AbsoluteUri);

            return blobfile;
        }


        public static string GetFileName(string path)
        {
            return String.IsNullOrEmpty(path.Trim()) || !path.Contains('.') ? string.Empty : Path.GetFileName(new Uri(path).AbsolutePath);
        }

        public async Task<List<string>> DownloadBlob(string downloadPath)
        {
            var downloadedFiles = new List<string>();
            if (!Directory.Exists(downloadPath))
                Directory.CreateDirectory(downloadPath);

            var blobs = blobContainerClient.GetBlobsAsync();
            IAsyncEnumerator<BlobItem> enumerator = blobs.GetAsyncEnumerator();

            while (await enumerator.MoveNextAsync())
            {
                var blob = enumerator.Current;

                Console.WriteLine($"{blob.Name} ({blob.Properties.ContentLength} bytes) Downloading ...", ConsoleColor.Green);
                var blobClient = blobContainerClient.GetBlobClient(blob.Name);

                using (var fileStream = File.Create($"{downloadPath}\\{blob.Name}"))
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
            fileName = GetFileName(fileName);
            CloudBlobClient serviceClient = _storageAccount.CreateCloudBlobClient();

            var container = serviceClient.GetContainerReference(ContainerName);
            container.CreateIfNotExistsAsync().Wait();

            CloudBlockBlob blob = container.GetBlockBlobReference(fileName);

            SharedAccessBlobPolicy policy = new()
            {
                SharedAccessExpiryTime = DateTime.UtcNow.AddDays(1),
                Permissions = SharedAccessBlobPermissions.Read
            };

            string signature = blob.GetSharedAccessSignature(policy);
            return blob.Uri + signature;

        }

    }
}