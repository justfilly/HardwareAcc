using System;
using System.Collections.Generic;
using HardwareAcc.ViewModels;

namespace HardwareAcc.Services.FormsProvider;

public class FormsProvider : IFormsProvider
{
    private readonly Dictionary<Type, object> _formsDictionary = new();

    public void Initialize(IEnumerable<BaseViewModel> formViewModels)
    {
        foreach (BaseViewModel viewModel in formViewModels)
        {
            _formsDictionary.Add(viewModel.GetType(), viewModel);
        }
    }

    public TViewModel GetFormViewModel<TViewModel>() where TViewModel : class
    {
        Type type = typeof(TViewModel);

        if (_formsDictionary.TryGetValue(type, out var value))
            return (value as TViewModel)!;

        throw new InvalidOperationException($"ViewModel of type {type} not found.");
    }
}