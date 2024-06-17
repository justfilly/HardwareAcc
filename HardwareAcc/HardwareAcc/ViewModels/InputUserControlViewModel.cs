namespace HardwareAcc.ViewModels;

public class InputUserControlViewModel : BaseViewModel
{
    private string _inputText = "";
    public string InputText
    {
        get => _inputText;
    
        set
        {
            _inputText = value;
            OnPropertyChanged(nameof(InputText));
        }
    }   
}