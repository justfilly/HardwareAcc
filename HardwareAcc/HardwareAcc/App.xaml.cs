using System;
using System.IO;
using System.Windows;
using HardwareAcc.Services.DBConnectionService;
using HardwareAcc.Services.AuthService;
using HardwareAcc.ViewModels.LoginRegister;
using Microsoft.Extensions.Configuration;
using HardwareAcc.Views.LoginRegister;
using Microsoft.Extensions.DependencyInjection;

namespace HardwareAcc
{
    public partial class App
    {
        private IServiceProvider? _serviceProvider;
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IConfiguration configuration = CreateConfiguration();
            
            ServiceCollection serviceCollection = new();
            RegisterServices(serviceCollection, configuration);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            LoginRegisterWindowView loginRegisterWindow = _serviceProvider.GetRequiredService<LoginRegisterWindowView>();
            loginRegisterWindow.DataContext = _serviceProvider.GetRequiredService<LoginRegisterWindowViewModel>();
            loginRegisterWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (_serviceProvider is IDisposable disposable) 
                disposable.Dispose();
            
            base.OnExit(e);
        }
        
        private static IConfiguration CreateConfiguration()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }
        
        private static void RegisterServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddSingleton(configuration);

            serviceCollection.AddSingleton<IDBConnectionService, DBConnectionService>();
            serviceCollection.AddScoped<IAuthService, AuthService>();
            
            serviceCollection.AddSingleton<LoginRegisterWindowView>();
            serviceCollection.AddSingleton<LoginRegisterWindowViewModel>();
        }
    }
}