using System.Windows;
using System.Windows.Controls;
using HardwareAcc.MVVM.ViewModels.HardwareResponsibility.Tabs;

namespace HardwareAcc.MVVM.Views.HardwareResponsibility.Tabs;

public partial class ResponsibilityHistoryTabPageView : Page
{
    private readonly ResponsibilityHistoryTabPageViewModel _viewModel;

    public ResponsibilityHistoryTabPageView(ResponsibilityHistoryTabPageViewModel viewModel)
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