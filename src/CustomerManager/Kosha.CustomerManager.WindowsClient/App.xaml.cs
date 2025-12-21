using System.Windows;
using Kosha.CustomerManager.WindowsClient.Models.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kosha.CustomerManager.WindowsClient;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(
                (context, services) =>
                {
                    services.AddSingleton<MainWindow>();

                    Kosha.CustomerManager.WindowsClient.Startup.ConfigurationServices(
                        services, 
                        context.Configuration
                    );
                }
            )
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        try
        {
            base.OnStartup(e);

            await _host.StartAsync();

            MainWindow mainWindow =
                _host.Services.GetRequiredService<MainWindow>();

            mainWindow.Show();
        }
        catch
        {
            //
        }
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        try
        {
            using (_host)
            {
                await _host.StopAsync();
            }

            base.OnExit(e);
        }
        catch 
        {
            //
        }
    }
}