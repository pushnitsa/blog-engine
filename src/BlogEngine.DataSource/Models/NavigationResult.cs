namespace BlogEngine.DataSource.Models;
public class NavigationResult
{
    public int Count { get; set; }
    public IReadOnlyCollection<Entry> Entries { get; set; } = new List<Entry>();
}
