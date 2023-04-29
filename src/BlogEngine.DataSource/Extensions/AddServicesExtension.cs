using BlogEngine.DataSource.Index;
using BlogEngine.DataSource.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlogEngine.DataSource.Extensions;
public static class AddServicesExtension
{
    public static void AddDataSourceServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IEntryMetadataFileInfoExtractor, EntryMetadataFileInfoExtractorService>();
        serviceCollection.AddSingleton<IIndexManager, IndexManager>();
    }
}
