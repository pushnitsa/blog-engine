using BlogEngine.DataSource.Index;
using BlogEngine.DataSource.Models;
using BlogEngine.DataSource.Options;
using BlogEngine.DataSource.Services;
using Microsoft.Extensions.Options;
using Moq;

namespace BlogEngine.Tests;
public class IndexManagerTests
{
    [Fact]
    public void IndexShouldCoutainRightNames()
    {
        var entriesOptions = Options.Create(new EntriesOptions
        {
            DiscoveryPath = "/var/www",
            MetadataFileExtension = "json",
        });
        var entryMetadataFileInfo = new EntryMetadataFileInfo()
        {
            Name = "testName.json",
            FullName = "/var/www/content/testName.json",
            FullPath = "/var/www/content",
        };

        var metadataFileInfoExtractorMock = new Mock<IEntryMetadataFileInfoExtractor>();
        metadataFileInfoExtractorMock.Setup(x => x.GetInfo()).Returns(new[] { entryMetadataFileInfo });

        var dataLoaderMock = new Mock<IDataLoader>();
        dataLoaderMock.Setup(x => x.Load<EntryMetadata>(It.IsAny<string>())).Returns(new EntryMetadata());

        var indexManager = new IndexManager(entriesOptions, metadataFileInfoExtractorMock.Object, dataLoaderMock.Object);

        var index = indexManager.Indexes.First();

        Assert.Equal("testName", index.Id);
        Assert.Equal("content/testName.json", index.RelativePath);
        Assert.Equal("/var/www/content", index.AbsolutePath);
    }

    [Fact]
    public void RelativePathShouldNotContainLeadSlash()
    {
        var entriesOptions = Options.Create(new EntriesOptions
        {
            DiscoveryPath = "/var/www/",
            MetadataFileExtension = "",
        });
        var entryMetadataFileInfo = new EntryMetadataFileInfo()
        {
            Name = "testName",
            FullName = "/var/www/content/testName.json",
            FullPath = "",
        };

        var metadataFileInfoExtractorMock = new Mock<IEntryMetadataFileInfoExtractor>();
        metadataFileInfoExtractorMock.Setup(x => x.GetInfo()).Returns(new[] { entryMetadataFileInfo });

        var dataLoaderMock = new Mock<IDataLoader>();
        dataLoaderMock.Setup(x => x.Load<EntryMetadata>(It.IsAny<string>())).Returns(new EntryMetadata());

        var indexManager = new IndexManager(entriesOptions, metadataFileInfoExtractorMock.Object, dataLoaderMock.Object);

        var index = indexManager.Indexes.First();

        Assert.Equal("content/testName.json", index.RelativePath);
    }

    [Fact]
    public void RelativePathShouldNotContainLeadBackSlash()
    {
        var entriesOptions = Options.Create(new EntriesOptions
        {
            DiscoveryPath = "c:\\var\\www",
            MetadataFileExtension = "",
        });
        var entryMetadataFileInfo = new EntryMetadataFileInfo()
        {
            Name = "testName",
            FullName = "c:\\var\\www\\content\\testName.json",
            FullPath = "",
        };

        var metadataFileInfoExtractorMock = new Mock<IEntryMetadataFileInfoExtractor>();
        metadataFileInfoExtractorMock.Setup(x => x.GetInfo()).Returns(new[] { entryMetadataFileInfo });

        var dataLoaderMock = new Mock<IDataLoader>();
        dataLoaderMock.Setup(x => x.Load<EntryMetadata>(It.IsAny<string>())).Returns(new EntryMetadata());

        var indexManager = new IndexManager(entriesOptions, metadataFileInfoExtractorMock.Object, dataLoaderMock.Object);

        var index = indexManager.Indexes.First();

        Assert.Equal("content\\testName.json", index.RelativePath);
    }
}
