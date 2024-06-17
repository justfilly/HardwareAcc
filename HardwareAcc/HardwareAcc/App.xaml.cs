using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using HardwareAcc.Services.DBConnectionService;
using HardwareAcc.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace HardwareAcc
{
    public partial class App
    {
        private IServiceProvider? _serviceProvider;

        private void TEST_DB_CONNECTION(IServiceProvider serviceProvider)
        {
            IDBConnectionService dbConnectionService = serviceProvider.GetRequiredService<IDBConnectionService>();
            using MySqlConnection connection = dbConnectionService.GetConnection();

            using var command = new MySqlCommand("SELECT * FROM hardware;", connection);
            using var reader = command.ExecuteReader();

            string output;
            
            if (!reader.HasRows)
            {
                output = "No rows found.";
            }

            var result = new StringBuilder();
            while (reader.Read())
            {
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    result.Append(reader.GetName(i) + ": " + reader.GetValue(i) + "\t");
                }
                result.AppendLine();
            }

            output = result.ToString();
            
            Debug.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            Debug.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            Debug.WriteLine(output);
            Debug.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            Debug.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
        }
        
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IConfiguration configuration = CreateConfiguration();
            
            ServiceCollection serviceCollection = new();
            RegisterServices(serviceCollection, configuration);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            TestWindow testWindow = _serviceProvider.GetRequiredService<TestWindow>();
            testWindow.Show();



            TEST_DB_CONNECTION(_serviceProvider);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (_serviceProvider is IDisposable disposable) 
                disposable.Dispose();
            
            base.OnExit(e);
        }

        private IConfiguration CreateConfiguration()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }

        private void RegisterServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddSingleton(configuration);

            serviceCollection.AddSingleton<IDBConnectionService, DBConnectionService>();
            
            serviceCollection.AddSingleton<TestWindow>();
        }
    }
}