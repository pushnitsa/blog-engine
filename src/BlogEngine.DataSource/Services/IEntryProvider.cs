using BlogEngine.DataSource.Models;

namespace BlogEngine.DataSource.Services;
public interface IEntryProvider
{
    Task<Entry?> GetEntryAsync(string slug);

    Task<NavigationResult> GetEntriesAsync(NavigationCriteria navigationCriteria);

    Task<int> CountAsync();
}
