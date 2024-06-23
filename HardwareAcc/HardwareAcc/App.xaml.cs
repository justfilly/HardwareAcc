using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using HardwareAcc.Commands;
using HardwareAcc.Services.Auth;
using HardwareAcc.Services.DBConnection;
using HardwareAcc.Services.Navigation;
using HardwareAcc.Services.ViewLocator;
using HardwareAcc.ViewModels;
using HardwareAcc.Views;
using HardwareAcc.Services.Repositories;
using HardwareAcc.ViewModels.LoginRegister;
using HardwareAcc.ViewModels.Tabs;
using HardwareAcc.Views.LoginRegister;
using HardwareAcc.Views.Tabs;
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
            _serviceProvider.GetRequiredService<INavigationService>().Navigate<AccountingPageViewModel>();
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
            viewLocator.Register<RegisterNamePageViewModel, RegisterNamePageView>();
            viewLocator.Register<RegisterContactInfoPageViewModel, RegisterContactInfoPageView>();
            viewLocator.Register<RegisterCredentialsPageViewModel, RegisterCredentialsPageView>();
            viewLocator.Register<AccountingPageViewModel, AccountingPageView>();
            
            viewLocator.Register<HardwareTabPageViewModel, HardwareTabPageView>();
            viewLocator.Register<UsersTabPageViewModel, UsersTabPageView>();
            viewLocator.Register<AudiencesTabPageViewModel, AudiencesTabPageView>();
            viewLocator.Register<StatusesTabPageViewModel, StatusesTabPageView>();
        }
        
        private void RegisterServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddSingleton(configuration);

            serviceCollection.AddSingleton<IServiceProvider>(_ => _serviceProvider!);
            
            // Services.
            serviceCollection.AddSingleton<IDBConnectionService, DBConnectionService>();
            serviceCollection.AddSingleton<IUserRepository, UserRepository>();
            serviceCollection.AddSingleton<IAuthService, AuthService>();
            serviceCollection.AddSingleton<IViewLocator, ViewLocator>();
            serviceCollection.AddSingleton<INavigationService, NavigationService>();
            
            serviceCollection.AddSingleton<MainWindowView>();
            serviceCollection.AddSingleton<MainWindowViewModel>();

            // Pages.
            serviceCollection.AddSingleton<LoginPageView>();
            serviceCollection.AddSingleton<LoginPageViewModel>();

            serviceCollection.AddSingleton<RegisterNamePageView>();
            serviceCollection.AddSingleton<RegisterNamePageViewModel>();
            
            serviceCollection.AddSingleton<RegisterContactInfoPageView>();
            serviceCollection.AddSingleton<RegisterContactInfoPageViewModel>();
            
            serviceCollection.AddSingleton<RegisterCredentialsPageView>();
            serviceCollection.AddSingleton<RegisterCredentialsPageViewModel>();
            
            serviceCollection.AddSingleton<AccountingPageView>();
            serviceCollection.AddSingleton<AccountingPageViewModel>();
            
            // Tabs.
            serviceCollection.AddSingleton<HardwareTabPageView>();
            serviceCollection.AddSingleton<HardwareTabPageViewModel>();
            
            serviceCollection.AddSingleton<UsersTabPageView>();
            serviceCollection.AddSingleton<UsersTabPageViewModel>();
            
            serviceCollection.AddSingleton<AudiencesTabPageView>();
            serviceCollection.AddSingleton<AudiencesTabPageViewModel>();
            
            serviceCollection.AddSingleton<StatusesTabPageView>();
            serviceCollection.AddSingleton<StatusesTabPageViewModel>();
        }
    }
}