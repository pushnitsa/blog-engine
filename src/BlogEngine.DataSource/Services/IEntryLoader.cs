using BlogEngine.DataSource.Models;

namespace BlogEngine.DataSource.Services;
public interface IEntryLoader
{
    Task<Entry> LoadEntryAsync(string id);
}
