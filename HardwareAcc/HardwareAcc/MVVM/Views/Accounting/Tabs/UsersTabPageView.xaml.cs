using System.Windows;
using System.Windows.Controls;
using HardwareAcc.MVVM.ViewModels.Accounting.AdminTabs;

namespace HardwareAcc.MVVM.Views.Accounting.Tabs;

public partial class UsersTabPageView : Page
{
    private readonly UsersTabPageViewModel _viewModel;

    public UsersTabPageView(UsersTabPageViewModel viewModel)
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