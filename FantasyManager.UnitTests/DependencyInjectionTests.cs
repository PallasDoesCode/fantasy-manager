using FantasyManager.Infrastructure.Context;
using FantasyManager.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FantasyManager.UnitTests
{
    [TestClass]
    public class DependencyInjectionTests
    {
        [TestMethod]
        public void ConfigureServices_HasServices()
        {
            //var scope = new DefaultScope();

            //var serviceCollection = scope.Services;
            //scope.InstanceUnderTest.ConfigureServices( serviceCollection );

            //Assert.IsTrue( serviceCollection.Count > 0 );
        }

        [TestMethod]
        public void ConfigureServices_CanProduce_ServiceProvider()
        {
            //var scope = new DefaultScope();

            //var serviceCollection = scope.Services;
            //scope.InstanceUnderTest.ConfigureServices( serviceCollection );

            //var provider = serviceCollection.BuildServiceProvider();
            //Assert.IsNotNull( provider );
        }
    }

    //private class DefaultScope : TestScope<Startup> {
    //    public IServiceCollection Services { get; private set; }

    //    public DefaultScope( IServiceCollection services = null ) {
    //        var startup = new Startup();

    //        Services = services ?? new ServiceCollection();

    //        // any mocks needed
    //        AddMockServices();

    //        // so that DI does not choke on anything that has ILogger<T> as a dependency call AddLogging but turn off logging
    //        Services.AddLogging( options => options.SetMinimumLevel( LogLevel.None ) );

    //        // Configuration object cannot be null so just give an empty builder  
    //        var configurationBuilder = new ConfigurationBuilder();
    //        startup.ApplyConfiguration( configurationBuilder.Build() );

    //        InstanceUnderTest = startup;
    //    }

    //    // we dont care about functionality of these services but required
    //    // to be added or DI will fail
    //    private void AddMockServices() {
    //        var mockDbContext = new Mock<FantasyManagerContext>();
    //        Services.AddScoped( provider => mockDbContext.Object );

    //        var mockLogger = new Mock<ILogger>();
    //        Services.AddTransient( provider => mockLogger.Object );

    //        var mockCosmosDbFactory = new Mock<ICosmosDbClientFactory>();
    //        Services.AddSingleton( provider => mockCosmosDbFactory.Object );

    //        var mockCosmosDbBulkExecutor = new Mock<ICosmosBulkExecutorClientFactory>();
    //        Services.AddSingleton( provider => mockCosmosDbBulkExecutor.Object );
    //    }
    //}
}
