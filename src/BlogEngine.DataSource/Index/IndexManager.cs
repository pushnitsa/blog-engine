using BlogEngine.DataSource.Extensions;
using BlogEngine.DataSource.Models;
using BlogEngine.DataSource.Options;
using BlogEngine.DataSource.Services;
using Microsoft.Extensions.Options;

namespace BlogEngine.DataSource.Index;

public class IndexManager : IIndexManager
{
    private readonly EntriesOptions _entriesOptions;

    private readonly List<EntryMetadata> _indexes = new List<EntryMetadata>();
    private readonly IEntryMetadataFileInfoExtractor _entryMetadataFileInfoExtractor;

    public IndexManager(IOptions<EntriesOptions> entriesOptions, IEntryMetadataFileInfoExtractor entryMetadataFileInfoExtractor)
    {
        _entriesOptions = entriesOptions.Value;
        _entryMetadataFileInfoExtractor = entryMetadataFileInfoExtractor;

        BuildIndex();
    }

    public IReadOnlyCollection<EntryMetadata> Indexes => _indexes.AsReadOnly();

    private void BuildIndex()
    {
        var metadataFileInfos = _entryMetadataFileInfoExtractor.GetInfo();

        var extensionWithDot = $".{_entriesOptions.MetadataFileExtension}";

        foreach (var metadataFileInfo in metadataFileInfos)
        {
            var entryMetadata = new EntryMetadata
            {
                Id = metadataFileInfo.Name[..^extensionWithDot.Length],
                AbsolutePath = metadataFileInfo.FullPath,
                RelativePath = metadataFileInfo.FullName[_entriesOptions.DiscoveryPath.Length..].RemoveLeadSlash(),
                FullName = metadataFileInfo.FullName,
            };

            _indexes.Add(entryMetadata);
        }
    }
}
