using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DLL.Context;
using DLL.IRepository;
using BLL.Services;

namespace Downloader
{
    public partial class App : Application
    {
        public static ServiceProvider provider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            provider = services.BuildServiceProvider();
        }
        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<EntityContext>(option => { option.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Downloader;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"); });

            services.AddTransient<MyFileRepository>();
            services.AddTransient<DownloadManager>();
            services.AddTransient<MainWindow>();
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var mainWindow = provider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
