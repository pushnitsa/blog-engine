using BlogEngine.DataSource.Models;
using BlogEngine.DataSource.Options;
using Microsoft.Extensions.Options;

namespace BlogEngine.DataSource.Index;

public class IndexManager : IIndexManager
{
    private readonly EntriesOptions _entriesOptions;

    public IndexManager(IOptions<EntriesOptions> entriesOptions)
    {
        _entriesOptions = entriesOptions.Value;

        BuildIndex();
    }

    public IReadOnlyCollection<EntryMetadata> Indexes => throw new NotImplementedException();

    private void BuildIndex()
    {
        var metadataFiles = Directory.GetFiles(_entriesOptions.DiscoveryPath, "*.json", SearchOption.AllDirectories);

        foreach (var metadataFile in metadataFiles)
        {
            var metadataFileInfo = new FileInfo(metadataFile);
            var entryMetadata = new EntryMetadata
            {

            };
        }
    }
}
