namespace BlogEngine.DataSource.Services;
public interface IFileReader
{
    Task<string> ReadAsync(string path);
    string Read(string path);
}
