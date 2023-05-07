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
}
