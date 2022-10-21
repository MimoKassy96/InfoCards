using Client.Interfaces;
using Client.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Windows;

namespace Client
{
    public partial class App : Application
    {
        public static IHost _host { get; private set; }

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<AddNewCardWindow>();
                    services.AddTransient<IInfoCardsService, InfoCardsService>();
                })
                .UseSerilog((host, loggerConfiguration) => 
                {
                    loggerConfiguration.WriteTo.File("Client_logger.txt")
                    .MinimumLevel.Error();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host!.StartAsync();

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
