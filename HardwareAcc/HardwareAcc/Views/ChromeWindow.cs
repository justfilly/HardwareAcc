using System.Windows;

namespace HardwareAcc.Views;

public class ChromeWindow : Window
{
    private readonly ChromeFullscreen _chromeFullscreen = new();
    
    protected ChromeWindow()
    {
        _chromeFullscreen.Init(this);
    }

    protected void Minimize_Click(object sender, RoutedEventArgs e) => 
        WindowState = WindowState.Minimized;

    protected void Maximize_Click(object sender, RoutedEventArgs e)
    {
        if (WindowState == WindowState.Maximized)
            WindowState = WindowState.Normal;
        else
            WindowState = WindowState.Maximized;
    }

    protected void Close_Click(object sender, RoutedEventArgs e) => 
        Close();
}