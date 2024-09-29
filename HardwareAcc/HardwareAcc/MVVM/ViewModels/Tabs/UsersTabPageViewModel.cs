using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HardwareAcc.Commands;
using HardwareAcc.MVVM.Models;
using HardwareAcc.MVVM.ViewModels.Forms;
using HardwareAcc.Services.Navigation;
using HardwareAcc.Services.Repositories.User;

namespace HardwareAcc.MVVM.ViewModels.Tabs;

public class UsersTabPageViewModel : BaseViewModel, IDisposable
{
    private readonly IUserRepository _repository;

    public UsersTabPageViewModel(IUserRepository repository, INavigationService navigationService)
    {
        _repository = repository;
        _users = new ObservableCollection<UserModel>();
        FormNavigateCommand = new NavigateToFormCommand<UsersFormPageViewModel, UserModel>(navigationService);
        DeleteRecordCommand = new RelayCommandWithParameter(DeleteRecord, CanDeleteRecord);
        
    }

    private ObservableCollection<UserModel> _users;

    public ObservableCollection<UserModel> Users
    {
        get => _users;
    
        set
        {
            _users = value;
            OnPropertyChanged(nameof(Users));
        }
    }

    public NavigateToFormCommand<UsersFormPageViewModel, UserModel> FormNavigateCommand { get; }
    public RelayCommandWithParameter DeleteRecordCommand { get; }
    public static UserModel NewModel => new();

    public async Task InitializeAsync()
    {
        await LoadRecordsAsync();
        _repository.Changed += OnChanged;
    }

    private async void OnChanged() => 
        await LoadRecordsAsync();

    private async Task LoadRecordsAsync()
    {
        IEnumerable<UserModel> users = await _repository.GetAllAsync();
        Users = new ObservableCollection<UserModel>(users);
    }
    
    private void DeleteRecord(object model)
    {
        if (model is UserModel user)
            _repository.DeleteAsync(user.Id);
        else
            throw new ArgumentException($"Argument {nameof(model)} must be of type {nameof(UserModel)}");
    }

    private bool CanDeleteRecord() => 
        true;


    public void Dispose() => 
        _repository.Changed -= OnChanged;
}