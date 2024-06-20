using System;
using System.IO;
using System.Windows;
using HardwareAcc.Services.DBConnectionService;
using HardwareAcc.Services.AuthService;
using HardwareAcc.Services.Navigation;
using HardwareAcc.Services.ViewLocator;
using HardwareAcc.ViewModels;
using HardwareAcc.ViewModels.Pages;
using HardwareAcc.Views;
using HardwareAcc.Views.Pages;
using HardwareAcc.Services.Repositories;
using Microsoft.Extensions.Configuration;
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

            RegisterViews();
            
            MainWindowView loginRegisterWindow = _serviceProvider.GetRequiredService<MainWindowView>();
            loginRegisterWindow.DataContext = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            loginRegisterWindow.Show();
            _serviceProvider.GetRequiredService<INavigationService>().Navigate<LoginPageViewModel>();
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

        private void RegisterViews()
        {
            IViewLocator viewLocator = _serviceProvider!.GetRequiredService<IViewLocator>();
            
            viewLocator.Register<LoginPageViewModel, LoginPageView>();
            viewLocator.Register<RegisterPageViewModel, RegisterPageView>();
        }
        
        private void RegisterServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddSingleton(configuration);

            serviceCollection.AddSingleton<IServiceProvider>(_ => _serviceProvider!);
            serviceCollection.AddSingleton<IDBConnectionService, DBConnectionService>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IAuthService, AuthService>();
            serviceCollection.AddScoped<IViewLocator, ViewLocator>();
            serviceCollection.AddScoped<INavigationService, NavigationService>();
            
            serviceCollection.AddSingleton<MainWindowView>();
            serviceCollection.AddSingleton<MainWindowViewModel>();

            serviceCollection.AddSingleton<LoginPageView>();
            serviceCollection.AddSingleton<LoginPageViewModel>();

            serviceCollection.AddSingleton<RegisterPageView>();
            serviceCollection.AddSingleton<RegisterPageViewModel>();
        }
    }
}