using BlogEngine.DataSource.Models;
using BlogEngine.DataSource.Options;
using Microsoft.Extensions.Options;

namespace BlogEngine.DataSource.Services;
public class EntryMetadataFileInfoExtractorService : IEntryMetadataFileInfoExtractor
{
    private readonly EntriesOptions _entriesOptions;

    public EntryMetadataFileInfoExtractorService(IOptions<EntriesOptions> entriesOptions)
    {
        _entriesOptions = entriesOptions.Value;
    }

    public Task<IReadOnlyCollection<EntryMetadataFileInfo>> GetInfoAsync()
    {
        return Task.FromResult(GetInfo());
    }

    public IReadOnlyCollection<EntryMetadataFileInfo> GetInfo()
    {
        var metadataFiles = Directory.GetFiles(
            _entriesOptions.DiscoveryPath,
            $"*.{_entriesOptions.MetadataFileExtension}",
            SearchOption.AllDirectories);

        var fileInfos = metadataFiles.Select(x =>
        {
            var fileInfo = new FileInfo(x);

            var result = new EntryMetadataFileInfo
            {
                Name = fileInfo.Name,
                FullName = fileInfo.FullName,
                FullPath = fileInfo.Directory.FullName,
            };

            return result;
        }).ToList();

        return fileInfos.AsReadOnly();
    }
}
