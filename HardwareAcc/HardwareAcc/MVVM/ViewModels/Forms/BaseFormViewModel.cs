namespace HardwareAcc.MVVM.ViewModels.Forms;

public abstract class BaseFormViewModel<TModel> : BaseViewModel where TModel : class
{
    protected TModel _model;
    protected FormMode _mode;
    
    public virtual void Initialize(TModel model)
    {
        _model = model;
    }
}