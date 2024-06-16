using System;
using System.Windows;
using HardwareAcc.ViewModels;
using HardwareAcc.Views;
using Microsoft.Extensions.DependencyInjection;

namespace HardwareAcc
{
    public partial class App
    {
        private IServiceProvider? _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ServiceCollection serviceCollection = new();
            RegisterServices(serviceCollection);
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
        
        private void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<LoginRegisterWindowView>();
            serviceCollection.AddSingleton<LoginRegisterWindowViewModel>();
        }
    }
}