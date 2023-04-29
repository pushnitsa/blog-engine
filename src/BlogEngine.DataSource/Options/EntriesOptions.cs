namespace BlogEngine.DataSource.Options;
public class EntriesOptions
{
    public const string Entries = nameof(Entries);

    public string DiscoveryPath { get; set; }
    public string MetadataFileExtension { get; set; }
    public string EntryFileExtension { get; set; }
}
