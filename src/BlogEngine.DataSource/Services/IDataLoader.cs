namespace BlogEngine.DataSource.Services;
public interface IDataLoader
{
    Task<T> LoadAsync<T>(string path);
    T Load<T>(string path);
}
