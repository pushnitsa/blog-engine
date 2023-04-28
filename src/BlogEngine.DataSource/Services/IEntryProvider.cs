using BlogEngine.DataSource.Models;

namespace BlogEngine.DataSource.Services;
public interface IEntryProvider
{
    Task<Entry> GetEntryAsync(string id);
}
