namespace HardwareAcc.MVVM.ViewModels.Forms.Base;

public abstract class BaseFormViewModel<TModel> : BaseViewModel where TModel : class
{
    protected TModel _model;
    protected FormMode _mode;
    
    public virtual void Initialize(TModel model)
    {
        _model = model;
    }
}