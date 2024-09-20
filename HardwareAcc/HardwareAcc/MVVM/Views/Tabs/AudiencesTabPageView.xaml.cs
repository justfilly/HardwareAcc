using System.Windows;
using System.Windows.Controls;
using HardwareAcc.MVVM.ViewModels.Tabs;

namespace HardwareAcc.MVVM.Views.Tabs;

public partial class AudiencesTabPageView : Page
{
    private readonly AudiencesTabPageViewModel _viewModel;

    public AudiencesTabPageView(AudiencesTabPageViewModel viewModel)
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