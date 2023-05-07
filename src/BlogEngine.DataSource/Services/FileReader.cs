namespace BlogEngine.DataSource.Services;
public class FileReader : IFileReader
{
    public string Read(string path)
    {
        var result = File.ReadAllText(path);

        return result;
    }

    public async Task<string> ReadAsync(string path)
    {
        var result = await File.ReadAllTextAsync(path);

        return result;
    }
}
