namespace HardwareAcc.Services.FormsProvider;

public interface IFormsProvider
{
    TViewModel GetFormViewModel<TViewModel>() where TViewModel : class;
}