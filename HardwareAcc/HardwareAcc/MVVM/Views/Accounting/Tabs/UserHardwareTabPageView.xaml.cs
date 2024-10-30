using System.Windows;
using System.Windows.Controls;
using HardwareAcc.MVVM.ViewModels.Accounting.UserTabs;

namespace HardwareAcc.MVVM.Views.Accounting.Tabs;

public partial class UserHardwareTabPageView : Page
{
    private readonly UserHardwareTabPageViewModel _viewModel;

    public UserHardwareTabPageView(UserHardwareTabPageViewModel viewModel)
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