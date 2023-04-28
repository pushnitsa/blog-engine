using BlogEngine.DataSource.Index;
using Microsoft.Extensions.DependencyInjection;

namespace BlogEngine.DataSource.Extensions;
public static class AddServicesExtension
{
    public static void AddDataSourceServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IndexManager>();
        serviceCollection.AddSingleton<IIndexManager>(x => x.GetRequiredService<IndexManager>());
    }
}
