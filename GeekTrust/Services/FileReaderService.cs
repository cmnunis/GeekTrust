using System;
using System.IO;
using System.Threading.Tasks;

namespace geektrust_family_demo.Services
{
    public interface IFileReaderService
    {
        Task<string[]> GetFileContentsAsync(string fileName);
    }
    public class FileReaderService : IFileReaderService
    {
        public async Task<string[]> GetFileContentsAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            return await File.ReadAllLinesAsync(fileName);
        }
    }
}
