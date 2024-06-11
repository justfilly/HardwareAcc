using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HardwareAcc.Views;

public partial class BaseWindow : Window
{
    private enum ResizeDirection
    {
        Left = 61441,
        Right = 61442,
        Bottom = 61446,
        BottomLeft = 61447,
        BottomRight = 61448,
    }
    
    [DllImport( "user32.dll", CharSet = CharSet.Auto )]
    private static extern IntPtr SendMessage( IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam );
    
    private const int WM_SYSCOMMAND = 0x112;
    private HwndSource hwndSource;
    
    private void ResizeWindow(ResizeDirection direction ) => 
        SendMessage( hwndSource.Handle, WM_SYSCOMMAND, (IntPtr)direction, IntPtr.Zero );

    private void LeftResize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed) 
            ResizeWindow(ResizeDirection.Left);
    }

    private void RightResize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed) 
            ResizeWindow(ResizeDirection.Right);
    }

    private void BottomResize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed) 
            ResizeWindow(ResizeDirection.Bottom);
    }

    private void BottomLeftResize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed) 
            ResizeWindow(ResizeDirection.BottomLeft);
    }

    private void BottomRightResize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed) 
            ResizeWindow(ResizeDirection.BottomRight);
    }
        
    
    private void BaseWindowSourceInitialized(object sender, EventArgs e)
    {
        hwndSource = PresentationSource.FromVisual( (Visual)sender ) as HwndSource;
        SourceInitialized -= BaseWindowSourceInitialized;
    }
    
    public BaseWindow()
    {
        InitializeComponent();
        SourceInitialized += BaseWindowSourceInitialized;
    }
    
    
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

    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount == 2)
            Maximize_Click(sender, e);
        else
            DragMove();
    }
}