using System.Windows;
using System.Windows.Input;

namespace HardwareAcc.MVVM.Views.UserControls;

public partial class TabButton
{
    public TabButton()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty CommandDependencyProperty =
        DependencyProperty.Register(
            name: nameof(Command),
            propertyType: typeof(ICommand),
            ownerType: typeof(TabButton),
            new PropertyMetadata(default(ICommand)));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandDependencyProperty);
        set => SetValue(CommandDependencyProperty, value);
    }

    public static readonly DependencyProperty TextDependencyProperty =
        DependencyProperty.Register(
            name: nameof(Text),
            propertyType: typeof(string),
            ownerType: typeof(TabButton),
            new PropertyMetadata(default(string)));

    public string Text
    {
        get => (string)GetValue(TextDependencyProperty);
        set => SetValue(TextDependencyProperty, value);
    }

    public static readonly DependencyProperty IsTabActiveDependencyProperty =
        DependencyProperty.Register(
            name: nameof(IsTabActive),
            propertyType: typeof(bool),
            ownerType: typeof(TabButton),
            new PropertyMetadata(default(bool)));

    public bool IsTabActive
    {
        get => (bool)GetValue(IsTabActiveDependencyProperty);
        set => SetValue(IsTabActiveDependencyProperty, value);
    }
}