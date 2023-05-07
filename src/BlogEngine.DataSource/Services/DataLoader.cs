using BlogEngine.DataSource.Serialization;
using System.Text.Json;

namespace BlogEngine.DataSource.Services;
public class DataLoader : IDataLoader
{
    private readonly IFileReader _fileReader;

    public DataLoader(IFileReader fileReader)
    {
        _fileReader = fileReader;
    }

    public T Load<T>(string path)
    {
        var content = _fileReader.Read(path);
        var result = Deserialize<T>(content);

        return result ?? throw new InvalidOperationException($"File {path} cannot be desearialized to type {typeof(T).Name}.");
    }

    public async Task<T> LoadAsync<T>(string path)
    {
        var content = await _fileReader.ReadAsync(path);
        var result = Deserialize<T>(content);

        return result ?? throw new InvalidOperationException($"File {path} cannot be desearialized to type {typeof(T).Name}.");
    }

    private static T? Deserialize<T>(string content)
    {

        if (typeof(T) == typeof(string))
        {
            return (T)(object)content;
        }

        const string dateTimeFormat = "dd/MM/yyyy HH:mm:ss";

        var result = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = {
                new DateTimeNullableSerializer(dateTimeFormat),
                new DateTimeSerializer(dateTimeFormat)
            },
        });

        return result;
    }
}
