using BlogEngine.DataSource.Models;

namespace BlogEngine.DataSource.Services;
public interface IEntryLoader
{
    Task<Entry> LoadEntryAsync(string id);

    Task<IReadOnlyCollection<Entry>> LoadEntriesAsync(IEnumerable<string> ids);
}
