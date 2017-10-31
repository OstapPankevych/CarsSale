using System.IO;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace CarsSale.DataAccess.Providers.Content
{
    public class AzureProvider : IContentProvider
    {
        private readonly CloudBlobClient _client;

        public AzureProvider()
        {
            _client = CreateClient();
        }

        public Stream Load(string path)
        {
            var container = _client.GetContainerReference(Path.GetDirectoryName(path));
            var blockBlob = container.GetBlockBlobReference(Path.GetFileName(path));
            var stream = new MemoryStream();
            blockBlob.DownloadToStream(stream);
            return stream;
        }

        public void Upload(string path, Stream stream)
        {
            var container = _client.GetContainerReference(Path.GetDirectoryName(path));
            container.CreateIfNotExists();
            var blobData = container.GetBlockBlobReference(Path.GetFileName(path));
            blobData.UploadFromStream(stream);
        }

        private CloudBlobClient CreateClient()
        {
            var account = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            return account.CreateCloudBlobClient();
        }
    }
}