using BlogEngine.DataSource.Index;
using BlogEngine.DataSource.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlogEngine.DataSource.Extensions;
public static class AddServicesExtension
{
    public static void AddDataSourceServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IEntryMetadataFileInfoExtractor, EntryMetadataFileInfoExtractorService>();

        serviceCollection.AddSingleton<IndexManager>();
        serviceCollection.AddSingleton<IIndexManager>(x => x.GetRequiredService<IndexManager>());
        serviceCollection.AddSingleton<IHasBuildingIndexState>(x => x.GetRequiredService<IndexManager>());
        serviceCollection.AddSingleton<ICanBuildIndex>(x => x.GetRequiredService<IndexManager>());

        serviceCollection.AddTransient<IDataLoader, DataLoader>();
        serviceCollection.AddTransient<IEntryLoader, EntryLoader>();
        serviceCollection.AddTransient<IEntryProvider, EntryProvider>();
        serviceCollection.AddTransient<IFileReader, FileReader>();
    }
}
