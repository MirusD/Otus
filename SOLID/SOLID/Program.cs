using Microsoft.Extensions.DependencyInjection;
using SOLID.Infrastructure.Configuration;
using SOLID.Presentation;

namespace SOLID
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection();
            DependencyInjection.ConfigurationServices(serviceProvider);

            var serviceProviderBuild = serviceProvider.BuildServiceProvider();

            var presentation = serviceProviderBuild.GetRequiredService<PresentationRoot>();
            presentation.StartConsoleUI();
        }
    }
}