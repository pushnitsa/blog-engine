using BlogEngine.DataSource.Options;
using BlogEngine.DataSource.Services;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace BlogEngine.Tests;
public class MetadataFileInfoExtractorServiceTests
{

    [Fact]
    public async Task GetFileInfoTests()
    {
        var entriesOptions = Options.Create(new EntriesOptions
        {
            DiscoveryPath = $"{new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName}{Path.DirectorySeparatorChar}TestData{Path.DirectorySeparatorChar}TestEntriesMetadata",
            MetadataFileExtension = "json"
        });
        var infoExtractor = new EntryMetadataFileInfoExtractorService(entriesOptions);

        var result = await infoExtractor.GetInfoAsync();

        Assert.Equal(2, result.Count);
        Assert.Equal("test1.json", result.First().Name);
    }
}
