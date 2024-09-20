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
        Loaded += AudiencesTabPageViewLoaded; 
    }

    private async void AudiencesTabPageViewLoaded(object sender, RoutedEventArgs e)
    {
        Loaded -= AudiencesTabPageViewLoaded;
        await _viewModel.InitializeAsync();
    }
}