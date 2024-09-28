using System.Windows;
using System.Windows.Controls;
using HardwareAcc.MVVM.ViewModels.Tabs;

namespace HardwareAcc.MVVM.Views.Tabs;

public partial class StatusesTabPageView : Page
{
    private readonly StatusesTabPageViewModel _viewModel;

    public StatusesTabPageView(StatusesTabPageViewModel viewModel)
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