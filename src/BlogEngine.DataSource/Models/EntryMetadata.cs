namespace BlogEngine.DataSource.Models;

public class EntryMetadata
{
    public string? RelativePath { get; set; }
    public string? AbsolutePath { get; set; }
    public string? FullName { get; set; }

    public string Id { get; set; }
    public string Slug { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public List<string> Tags { get; set; }
    public string Title { get; set; }
}
