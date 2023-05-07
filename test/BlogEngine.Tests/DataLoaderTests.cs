using BlogEngine.DataSource.Services;
using Moq;

namespace BlogEngine.Tests;
public class DataLoaderTests
{
    [Fact]
    public void DataLoaderShouldDeserealizeObject()
    {
        var fileReaderMock = new Mock<IFileReader>();
        fileReaderMock.Setup(x => x.Read(It.IsAny<string>())).Returns("{\"Id\":\"testId\"}");
        var dataLoader = new DataLoader(fileReaderMock.Object);

        var result = dataLoader.Load<TestDto>("testPath");

        Assert.Equal("testId", result.Id);
    }

    private class TestDto
    {
        public string Id { get; set; }
    }
}
