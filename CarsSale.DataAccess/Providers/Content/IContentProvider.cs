using System.IO;

namespace CarsSale.DataAccess.Providers.Content
{
    public interface IContentProvider
    {
        void Upload(string path, Stream stream);

        Stream Load(string path);
    }
}