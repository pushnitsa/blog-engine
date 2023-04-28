using BlogEngine.DataSource.Models;

namespace BlogEngine.DataSource.Index;
public interface IIndexManager
{
    IReadOnlyCollection<EntryMetadata> Indexes { get; }
}
