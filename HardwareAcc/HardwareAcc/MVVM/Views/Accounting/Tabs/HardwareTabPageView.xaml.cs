using System.Windows;
using System.Windows.Controls;
using HardwareAcc.MVVM.ViewModels.Accounting.AdminTabs;

namespace HardwareAcc.MVVM.Views.Accounting.Tabs;

public partial class HardwareTabPageView : Page
{
    private readonly HardwareTabPageViewModel _viewModel;

    public HardwareTabPageView(HardwareTabPageViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = _viewModel;
        Loaded += ViewLoaded; 
    }

    private async void ViewLoaded(object sender, RoutedEventArgs e)
    {
        Loaded -= ViewLoaded;
        await _viewModel.InitializeAsync();
    }
}