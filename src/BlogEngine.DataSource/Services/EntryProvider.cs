using BlogEngine.DataSource.Extensions;
using BlogEngine.DataSource.Index;
using BlogEngine.DataSource.Models;

namespace BlogEngine.DataSource.Services;
public class EntryProvider : IEntryProvider
{
    private readonly IIndexManager _indexManager;
    private readonly IEntryLoader _entryLoader;

    public EntryProvider(IIndexManager indexManager, IEntryLoader entryLoader)
    {
        _indexManager = indexManager;
        _entryLoader = entryLoader;
    }

    public async Task<Entry?> GetEntryAsync(string slug)
    {
        var entryId = _indexManager.GetEntryId(slug);

        Entry? entry = null;

        if (!string.IsNullOrEmpty(entryId))
        {
            entry = await _entryLoader.LoadEntryAsync(entryId);
        }

        return entry;
    }

    public async Task<NavigationResult> GetEntriesAsync(NavigationCriteria navigationCriteria)
    {
        var indexes = _indexManager.GetIndexCopy().AsQueryable();

        if (navigationCriteria.OrderDirection == DateCreationOrderDirection.Ascending)
        {
            indexes = indexes.OrderBy(x => x.CreatedDate).AsQueryable();
        }
        else
        {
            indexes = indexes.OrderByDescending(x => x.CreatedDate).AsQueryable();
        }

        var entryIds = indexes
            .Skip(navigationCriteria.Skip)
            .Take(navigationCriteria.Take)
            .Select(x => x.Id).ToList();

        var count = indexes.Count();
        var entries = await _entryLoader.LoadEntriesAsync(entryIds);

        var result = new NavigationResult
        {
            Count = count,
            Entries = entries,
        };

        return result;
    }

    public Task<int> CountAsync()
    {
        var result = _indexManager.Indexes.Count;

        return Task.FromResult(result);
    }
}
