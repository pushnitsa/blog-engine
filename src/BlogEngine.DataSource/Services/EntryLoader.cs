using BlogEngine.DataSource.Extensions;
using BlogEngine.DataSource.Index;
using BlogEngine.DataSource.Models;
using BlogEngine.DataSource.Options;
using Microsoft.Extensions.Options;

namespace BlogEngine.DataSource.Services;
public class EntryLoader : IEntryLoader
{
    private readonly EntriesOptions _entriesOptions;
    private readonly IIndexManager _indexManager;
    private readonly IDataLoader _dataLoader;

    public EntryLoader(IOptions<EntriesOptions> options, IIndexManager indexManager, IDataLoader dataLoader)
    {
        _entriesOptions = options.Value;
        _indexManager = indexManager;
        _dataLoader = dataLoader;
    }

    public async Task<Entry> LoadEntryAsync(string id)
    {
        var entryMetadata = _indexManager.GetById(id);

        if (entryMetadata == null)
        {
            throw new InvalidOperationException($"Entry {id} index does not found.");
        }

        var fullEntryPath = $"{entryMetadata.AbsolutePath}{Path.DirectorySeparatorChar}{id}.{_entriesOptions.EntryFileExtension}";

        var content = await _dataLoader.LoadAsync<string>(fullEntryPath);

        var entry = new Entry
        {
            Id = entryMetadata.Id,
            CreatedDate = entryMetadata.CreatedDate,
            ModifiedDate = entryMetadata.ModifiedDate,
            Slug = entryMetadata.Slug,
            Title = entryMetadata.Title,
            Tags = entryMetadata.Tags,
            Content = content,
        };

        return entry;
    }
}
