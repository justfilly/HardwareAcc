namespace HardwareAcc.ViewModels.Forms;

public abstract class BaseFormViewModel<TModel> : BaseViewModel where TModel : class
{
    protected TModel? _model;
    protected FormMode _mode;
    
    public virtual void SetModel(TModel? model)
    {
        _model = model;
    }
}