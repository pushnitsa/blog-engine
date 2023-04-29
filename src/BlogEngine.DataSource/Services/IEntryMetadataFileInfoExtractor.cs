using BlogEngine.DataSource.Models;

namespace BlogEngine.DataSource.Services;
public interface IEntryMetadataFileInfoExtractor
{
    Task<IReadOnlyCollection<EntryMetadataFileInfo>> GetInfoAsync();
}
