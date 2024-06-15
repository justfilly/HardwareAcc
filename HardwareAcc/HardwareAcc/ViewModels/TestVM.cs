using HardwareAcc.Commands;

namespace HardwareAcc.ViewModels;

public class TestVM : BaseViewModel
{
    public TestVM()
    {
        TestCommand = new();
    }

    public TestCommand TestCommand { get; }
}