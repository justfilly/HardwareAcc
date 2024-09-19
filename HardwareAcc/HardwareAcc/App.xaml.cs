﻿using System;
using System.IO;
using System.Windows;
using HardwareAcc.Services.Auth;
using HardwareAcc.Services.DBConnection;
using HardwareAcc.Services.Navigation;
using HardwareAcc.Services.ViewLocator;
using HardwareAcc.ViewModels;
using HardwareAcc.Views;
using HardwareAcc.Services.Repositories;
using HardwareAcc.Services.Repositories.Audience;
using HardwareAcc.Services.Repositories.User;
using HardwareAcc.ViewModels.Forms;
using HardwareAcc.ViewModels.LoginRegister;
using HardwareAcc.ViewModels.Tabs;
using HardwareAcc.Views.Forms;
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

            RegisterViewsInViewLocator();
            
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

        private void RegisterServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddSingleton(configuration);

            serviceCollection.AddSingleton<IServiceProvider>(_ => _serviceProvider!);
            
            // Services.
            serviceCollection.AddSingleton<IDBConnectionService, DBConnectionService>();
            serviceCollection.AddSingleton<IUserRepository, UserRepository>();
            serviceCollection.AddSingleton<IAudienceRepository, AudienceRepository>();
            
            serviceCollection.AddSingleton<IAuthService, AuthService>();
            serviceCollection.AddSingleton<IViewLocator, ViewLocator>();
            serviceCollection.AddSingleton<INavigationService, NavigationService>();
            
            serviceCollection.AddSingleton<MainWindowView>();
            serviceCollection.AddSingleton<MainWindowViewModel>();

            // Registration Pages.
            serviceCollection.AddSingleton<LoginPageView>();
            serviceCollection.AddSingleton<LoginPageViewModel>();

            serviceCollection.AddSingleton<RegisterNamePageView>();
            serviceCollection.AddSingleton<RegisterNamePageViewModel>();
            
            serviceCollection.AddSingleton<RegisterContactInfoPageView>();
            serviceCollection.AddSingleton<RegisterContactInfoPageViewModel>();
            
            serviceCollection.AddSingleton<RegisterCredentialsPageView>();
            serviceCollection.AddSingleton<RegisterCredentialsPageViewModel>();
            
            // Accounting Page.
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
            
            // Forms.
            serviceCollection.AddSingleton<AudiencesFormPageView>();
            serviceCollection.AddSingleton<AudiencesFormPageViewModel>();
        }

        private void RegisterViewsInViewLocator()
        {
            IViewLocator viewLocator = _serviceProvider!.GetRequiredService<IViewLocator>();
            
            // Registration Pages.
            viewLocator.Register<LoginPageViewModel, LoginPageView>();
            viewLocator.Register<RegisterNamePageViewModel, RegisterNamePageView>();
            viewLocator.Register<RegisterContactInfoPageViewModel, RegisterContactInfoPageView>();
            viewLocator.Register<RegisterCredentialsPageViewModel, RegisterCredentialsPageView>();
            
            // Accounting Page.
            viewLocator.Register<AccountingPageViewModel, AccountingPageView>();
            
            // Tabs.
            viewLocator.Register<HardwareTabPageViewModel, HardwareTabPageView>();
            viewLocator.Register<UsersTabPageViewModel, UsersTabPageView>();
            viewLocator.Register<AudiencesTabPageViewModel, AudiencesTabPageView>();
            viewLocator.Register<StatusesTabPageViewModel, StatusesTabPageView>();
            
            // Forms.
            viewLocator.Register<AudiencesFormPageViewModel, AudiencesFormPageView>();
        }
    }
}