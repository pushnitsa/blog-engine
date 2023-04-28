using BlogEngine.DataSource.Models;
using BlogEngine.DataSource.Options;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace BlogEngine.DataSource.Index;

public class IndexManager : IIndexManager
{
    private readonly EntriesOptions _entriesOptions;

    private const string _metaDataFileExtension = "json";
    private const string _metaDataFileExtensionWithADot = $".{_metaDataFileExtension}";

    private readonly List<EntryMetadata> _indexes = new List<EntryMetadata>();

    public IndexManager(IOptions<EntriesOptions> entriesOptions)
    {
        _entriesOptions = entriesOptions.Value;

        BuildIndex();
    }

    public IReadOnlyCollection<EntryMetadata> Indexes => _indexes.AsReadOnly();

    private void BuildIndex()
    {
        var metadataFiles = Directory.GetFiles(_entriesOptions.DiscoveryPath, $"*{_metaDataFileExtensionWithADot}", SearchOption.AllDirectories);

        foreach (var metadataFile in metadataFiles)
        {
            var metadataFileInfo = new FileInfo(metadataFile);
            var entryMetadata = new EntryMetadata
            {
                Id = metadataFileInfo.Name[..(metadataFileInfo.Name.Length - _metaDataFileExtensionWithADot.Length)],
                AbsolutePath = metadataFileInfo.Directory.FullName,
                RelativePath = metadataFileInfo.Directory.FullName[_entriesOptions.DiscoveryPath.Length..],
                FullName = metadataFileInfo.FullName,
            };

            _indexes.Add(entryMetadata);
        }
    }

    private void ReadFileContent(string path)
    {
        var fileContent = File.ReadAllText(path);

        var obj = JsonSerializer.Deserialize<dynamic>(fileContent);
    }
}
