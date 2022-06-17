using System;
using geektrust_family_demo.Services;
using Xunit;

namespace GeekTrustTests.Services
{
    public class FileReaderServiceTests
    {
        private readonly FileReaderService _fileReaderService;

        public FileReaderServiceTests()
        {
            _fileReaderService = new FileReaderService();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void FileNameIsNullOrWhitespaceThrowsException(string value)
        {
            var sut = Assert.ThrowsAsync<ArgumentNullException>(async () => await _fileReaderService.GetFileContentsAsync(value));
            Assert.Equal("fileName", sut.Result.ParamName);
        }
    }
}