using BlogEngine.DataSource.Index;
using BlogEngine.DataSource.Models;

namespace BlogEngine.DataSource.Extensions;
public static class IndexManagerExtensions
{
    public static string? GetEntryId(this IIndexManager indexManager, string slug)
    {
        var result = indexManager.Indexes.FirstOrDefault(x => x.Slug == slug);

        return result?.Id;
    }

    public static EntryMetadata? GetById(this IIndexManager indexManager, string id)
    {
        var result = indexManager.Indexes.FirstOrDefault(x => x.Id == id);

        return result;
    }
}
