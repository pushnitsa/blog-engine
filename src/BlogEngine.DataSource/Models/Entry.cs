namespace BlogEngine.DataSource.Models;
public class Entry
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public IReadOnlyCollection<string> Tags { get; set; } = new List<string>();
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
