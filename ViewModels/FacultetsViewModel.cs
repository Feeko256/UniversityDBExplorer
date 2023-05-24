using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Prism.Events;
using UniversityDBExplorer.Models;
using UniversityDBExplorer.Special;
using UniversityDBExplorer.Special.Mediator;
using UniversityDBExplorer.Views;

namespace UniversityDBExplorer.ViewModels;

public class FacultetsViewModel : INotifyPropertyChanged
{
    public ObservableCollection<FacultetModel> Facultets { get; set; }
    private ObservableCollection<FacultetModel> searchedFacultets;
    private FacultetModel? selectedFacultet;
    private RelayCommand addNewFacultet;
    private RelayCommand removeFacultet;
    private RelayCommand updateCafedras;
    private RelayCommand searchFacCommand;
    private string searchFacultets;
    private wndVM _wnd;
    private Mediator mediator { get; set; }
    public FacultetModel? SelectedFacultet
    {
        get { return selectedFacultet; }
        set
        {
            selectedFacultet = value;
            OnPropertyChanged();
            if(SelectedFacultet != null)
                if (SelectedFacultet.Cafedra == null)
                    SelectedFacultet.Cafedra = new ObservableCollection<CafedraModel>();
            mediator.OnFacultetChanged(SelectedFacultet);
        }
    }
    public RelayCommand AddNewFacultet
    {
        get
        {            // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
            return addNewFacultet ??= new RelayCommand(obj =>
            {
                var fac = new FacultetModel 
                {
                    Title = $"Новый факультет", DekanName = "Декан", DekanLastName="Деканов", DekanFatherName="Декановч", 
                    Cafedra = new ObservableCollection<CafedraModel>(), 
                    Mail="Dekan@etu.ru", PhoneNumber="+79999999999", RoomNumber=1234
                };
                Facultets.Add(fac);
                SearchedFacultets = Facultets;
                BaseViewModel.db.Facultets.Add(fac);
                BaseViewModel.db.SaveChanges();
                BaseViewModel.Instance.Facultets = BaseViewModel.db.Facultets.Local.ToObservableCollection();
            });
        }
    }  
    public RelayCommand RemoveFacultet
    {
        get
        {            // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
            return removeFacultet ??= new RelayCommand(obj =>
            {
                if (obj is not FacultetModel fac) return;
                Facultets.Remove(fac);
                SearchedFacultets = Facultets;
                BaseViewModel.db.Facultets.Remove(fac);
                BaseViewModel.db.SaveChanges();
                BaseViewModel.Instance.Facultets.Remove(fac);

                DbOperations.RemoveCafedras();
                DbOperations.RemoveGroups();
                DbOperations.RemoveStudents();

                if (Facultets?.Count > 0)
                    SelectedFacultet = Facultets[^1];
            }, (obj) => (Facultets.Count > 0 && selectedFacultet!=null));
        }
    }
    
    public ObservableCollection<FacultetModel> SearchedFacultets
    {
        get { return searchedFacultets; }
        set { searchedFacultets = value;
            OnPropertyChanged("SearchedFacultets");
        }
    }
    public string SearchFacultets
    {
        get { return searchFacultets; }
        set { searchFacultets = value; OnPropertyChanged("SearchFacultets"); }
    }       
    private void SearchFac()
    {
        if (string.IsNullOrWhiteSpace(SearchFacultets))
        {
            SearchedFacultets = new ObservableCollection<FacultetModel>(Facultets);
        }
        else
        {
            SearchedFacultets = new ObservableCollection<FacultetModel>(Facultets.Where(f => f.Title.Contains(SearchFacultets, StringComparison.OrdinalIgnoreCase)));
        }
    }
    public RelayCommand SearchFacCommand
    {
        get
        {            // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
            return searchFacCommand ??= new RelayCommand(obj =>
            {
                SearchFac();
            });
        }
    }
    public FacultetsViewModel(Mediator mediator)
    {
        Facultets = BaseViewModel.Instance.Facultets;
        SearchedFacultets = new ObservableCollection<FacultetModel>(Facultets);
        this.mediator = mediator;
        _wnd = new wndVM();
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}