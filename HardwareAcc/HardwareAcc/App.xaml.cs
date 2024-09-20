using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using HardwareAcc.MVVM.ViewModels;
using HardwareAcc.MVVM.ViewModels.Forms;
using HardwareAcc.MVVM.ViewModels.LoginRegister;
using HardwareAcc.MVVM.ViewModels.Tabs;
using HardwareAcc.MVVM.Views;
using HardwareAcc.MVVM.Views.Forms;
using HardwareAcc.MVVM.Views.LoginRegister;
using HardwareAcc.MVVM.Views.Tabs;
using HardwareAcc.Services.Auth;
using HardwareAcc.Services.DBConnection;
using HardwareAcc.Services.FormsProvider;
using HardwareAcc.Services.Navigation;
using HardwareAcc.Services.ViewLocator;
using HardwareAcc.Services.Repositories.Audience;
using HardwareAcc.Services.Repositories.Status;
using HardwareAcc.Services.Repositories.User;
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

            InitializeFormsProvider();
            
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
            
            serviceCollection.AddSingleton<IAuthService, AuthService>();
            serviceCollection.AddSingleton<IViewLocator, ViewLocator>();
            serviceCollection.AddSingleton<INavigationService, NavigationService>();
            serviceCollection.AddSingleton<IFormsProvider, FormsProvider>();
            
            // Repositories.
            serviceCollection.AddSingleton<IUserRepository, UserRepository>();
            serviceCollection.AddSingleton<IAudienceRepository, AudienceRepository>();
            serviceCollection.AddSingleton<IStatusRepository, StatusRepository>();
            
            // Main Window.
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
            
            serviceCollection.AddSingleton<StatusesFormPageView>();
            serviceCollection.AddSingleton<StatusesFormPageViewModel>();
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
            viewLocator.Register<StatusesFormPageViewModel, StatusesFormPageView>();
        }
        
        private void InitializeFormsProvider()
        {
            IFormsProvider formsProvider = _serviceProvider.GetRequiredService<IFormsProvider>();

            List<BaseViewModel> formViewModels = new()
            {
                _serviceProvider.GetRequiredService<AudiencesFormPageViewModel>(),
                _serviceProvider.GetRequiredService<StatusesFormPageViewModel>(),
            };

            formsProvider.Initialize(formViewModels);
        }
    }
}