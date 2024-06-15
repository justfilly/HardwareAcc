using System;
using System.Windows;
using HardwareAcc.Views;
using Microsoft.Extensions.DependencyInjection;

namespace HardwareAcc
{
    public partial class App : Application
    {
        private IServiceProvider? _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ServiceCollection serviceCollection = new();
            RegisterServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            TestWindow testWindow = _serviceProvider.GetRequiredService<TestWindow>();
            testWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (_serviceProvider is IDisposable disposable) 
                disposable.Dispose();
            
            base.OnExit(e);
        }
        
        private void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<TestWindow>();
            
        }
    }
}