using BlogEngine.DataSource.Extensions;
using BlogEngine.DataSource.Models;
using BlogEngine.DataSource.Options;
using BlogEngine.DataSource.Services;
using Microsoft.Extensions.Options;

namespace BlogEngine.DataSource.Index;

public class IndexManager : IIndexManager, ICanBuildIndex, IHasBuildingIndexState
{
    private readonly EntriesOptions _entriesOptions;

    private readonly List<EntryMetadata> _indexes = new List<EntryMetadata>();
    private readonly IEntryMetadataFileInfoExtractor _entryMetadataFileInfoExtractor;
    private readonly IDataLoader _dataLoader;

    public IndexManager(
        IOptions<EntriesOptions> entriesOptions,
        IEntryMetadataFileInfoExtractor entryMetadataFileInfoExtractor,
        IDataLoader dataLoader)
    {
        _entriesOptions = entriesOptions.Value;
        _entryMetadataFileInfoExtractor = entryMetadataFileInfoExtractor;
        _dataLoader = dataLoader;

        BuildIndex();
    }

    public IReadOnlyCollection<EntryMetadata> Indexes => _indexes.AsReadOnly();

    public bool IsBuilding { get; private set; }

    public void BuildIndex()
    {
        IsBuilding = true;

        _indexes.Clear();

        var metadataFileInfos = _entryMetadataFileInfoExtractor.GetInfo();

        var extensionWithDot = $".{_entriesOptions.MetadataFileExtension}";

        foreach (var metadataFileInfo in metadataFileInfos)
        {
            var entryMetadata = _dataLoader.Load<EntryMetadata>(metadataFileInfo.FullName);

            entryMetadata.Id = metadataFileInfo.Name[..^extensionWithDot.Length];
            entryMetadata.AbsolutePath = metadataFileInfo.FullPath;
            entryMetadata.RelativePath = metadataFileInfo.FullName[_entriesOptions.DiscoveryPath.Length..].RemoveLeadSlash();
            entryMetadata.FullName = metadataFileInfo.FullName;

            _indexes.Add(entryMetadata);
        }

        IsBuilding = false;
    }
}
