using System;
using System.IO;
using CarsSale.DataAccess.Providers.Content;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace CarsSale.DataAccess.Servicess.Content
{
    public class AzureProvider : IContentProvider
    {
        private const char _separator = '\\';

        private readonly CloudBlobClient _client;

        public AzureProvider()
        {
            _client = CreateClient();
        }

        public Stream Load(string path)
        {
            var container = _client.GetContainerReference(GetContainerName(path));
            var blockBlob = container.GetBlockBlobReference(GetBlobName(path));
            var stream = new MemoryStream();
            blockBlob.DownloadToStream(stream);
            return stream;
        }

        public void Upload(string path, Stream stream)
        {
            var container = _client.GetContainerReference(GetContainerName(path));
            container.CreateIfNotExists();
            var blobData = container.GetBlockBlobReference(GetBlobName(path));
            stream.Seek(0, SeekOrigin.Begin);
            blobData.UploadFromStream(stream);
        }

        public Uri GetUri(string path)
        {
            var container = _client.GetContainerReference(GetContainerName(path));
            var blockBlob = container.GetBlockBlobReference(GetBlobName(path));
            return blockBlob.Uri;
        }

        private CloudBlobClient CreateClient()
        {
            var account = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            return account.CreateCloudBlobClient();
        }

        private string GetContainerName(string fullPath) =>
            fullPath.Substring(0, fullPath.IndexOf(_separator));

        private string GetBlobName(string fullPath)
        {
            var from = fullPath.IndexOf(_separator) + 1;
            var to = fullPath.Length - from;
            return fullPath.Substring(from, to);
        }
    }
}