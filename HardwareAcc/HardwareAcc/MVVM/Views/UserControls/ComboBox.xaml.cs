using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace HardwareAcc.MVVM.Views.UserControls;

public partial class ComboBox : UserControl
{
    public ComboBox()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty LabelTextProperty =
        DependencyProperty.Register(
            nameof(LabelText),
            typeof(string),
            typeof(ComboBox),
            new PropertyMetadata(string.Empty));

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    public static readonly DependencyProperty ComboBoxItemsProperty =
        DependencyProperty.Register(
            nameof(ComboBoxItems),
            typeof(ObservableCollection<string>),
            typeof(ComboBox),
            new PropertyMetadata(null));

    public ObservableCollection<string> ComboBoxItems
    {
        get => (ObservableCollection<string>)GetValue(ComboBoxItemsProperty);
        set => SetValue(ComboBoxItemsProperty, value);
    }

    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register(
            nameof(SelectedItem),
            typeof(string),
            typeof(ComboBox),
            new PropertyMetadata(null));

    public string SelectedItem
    {
        get => (string)GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public static readonly DependencyProperty ErrorTextProperty =
        DependencyProperty.Register(
            nameof(ErrorText),
            typeof(string),
            typeof(ComboBox),
            new PropertyMetadata(string.Empty));

    public string ErrorText
    {
        get => (string)GetValue(ErrorTextProperty);
        set => SetValue(ErrorTextProperty, value);
    }
}