using ConstruSys.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace ConstruSys.Desktop
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, configuration) =>
                {
                    configuration.SetBasePath(AppContext.BaseDirectory);

                    configuration.AddJsonFile(
                        "appsettings.json",
                        optional: false,
                        reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    string connectionString =
                        context.Configuration.GetConnectionString("DefaultConnection")
                        ?? throw new InvalidOperationException(
                            "A ConnectionString 'DefaultConnection' não foi encontrada.");

                    services.AddDbContext<AppDbContext>(options =>
                    {
                        options.UseSqlServer(connectionString);
                    });

                    services.AddTransient<MainWindow>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            using (IServiceScope scope = _host.Services.CreateScope())
            {
                AppDbContext dbContext =
                    scope.ServiceProvider.GetRequiredService<AppDbContext>();

                await dbContext.Database.MigrateAsync();
            }

            MainWindow mainWindow =
                _host.Services.GetRequiredService<MainWindow>();

            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();

            _host.Dispose();

            base.OnExit(e);
        }
    }
}