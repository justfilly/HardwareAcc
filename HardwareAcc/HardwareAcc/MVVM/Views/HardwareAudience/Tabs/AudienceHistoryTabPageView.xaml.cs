using System.Windows;
using System.Windows.Controls;
using HardwareAcc.MVVM.ViewModels.Accounting.Tabs;
using HardwareAcc.MVVM.ViewModels.HardwareResponsibility.Tabs;

namespace HardwareAcc.MVVM.Views.HardwareResponsibility.Tabs;

public partial class AudienceHistoryTabPageView : Page
{
    private readonly AudienceHistoryTabPageViewModel _viewModel;

    public AudienceHistoryTabPageView(AudienceHistoryTabPageViewModel viewModel)
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