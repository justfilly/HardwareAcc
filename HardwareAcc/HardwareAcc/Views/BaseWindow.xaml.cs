using System.Windows;

namespace HardwareAcc.Views;

public partial class BaseWindow : Window
{
    private void Minimize_Click(object sender, RoutedEventArgs e) => 
        WindowState = WindowState.Minimized;

    private void Maximize_Click(object sender, RoutedEventArgs e)
    {
        if (WindowState == WindowState.Maximized)
            WindowState = WindowState.Normal;
        else
            WindowState = WindowState.Maximized;
    }

    private void Close_Click(object sender, RoutedEventArgs e) => 
        Close();
}