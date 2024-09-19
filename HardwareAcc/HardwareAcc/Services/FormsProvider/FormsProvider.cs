using System;
using System.Collections.Generic;
using HardwareAcc.ViewModels.Forms;

namespace HardwareAcc.Services.FormsProvider;

public class FormsProvider : IFormsProvider
{
    private readonly Dictionary<Type, object> _formsDictionary = new();

    public FormsProvider(AudiencesFormPageViewModel audiencesFormViewModel)
    {
        _formsDictionary.Add(typeof(AudiencesFormPageViewModel), audiencesFormViewModel);
    }

    public TViewModel GetFormViewModel<TViewModel>() where TViewModel : class
    {
        Type type = typeof(TViewModel);

        if (_formsDictionary.TryGetValue(type, out var value))
            return (value as TViewModel)!;

        throw new InvalidOperationException($"ViewModel of type {type} not found.");
    }
}