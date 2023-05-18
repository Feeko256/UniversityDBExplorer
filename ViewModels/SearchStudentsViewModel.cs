using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UniversityDBExplorer.Models;

namespace UniversityDBExplorer.ViewModels;

public class SearchStudentsViewModel: INotifyPropertyChanged
{
    public ObservableCollection<FacultetModel> Facultets { get; set; }
    public ObservableCollection<StudentModel> St { get; set; }
    public SearchStudentsViewModel()
    {
        Facultets = BaseViewModel.Instance.Facultets;
        St = BaseViewModel.Instance.S;
    }
    
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}