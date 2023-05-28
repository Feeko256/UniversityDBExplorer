using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using UniversityDBExplorer.Models;
using UniversityDBExplorer.Special;
using UniversityDBExplorer.Special.Mediator;
using UniversityDBExplorer.Views;

namespace UniversityDBExplorer.ViewModels;

public class CafedraViewModel : INotifyPropertyChanged
{
    private FacultetModel facultet;
    private ObservableCollection<CafedraModel> searchedCafedras;
    private CafedraModel? selectedCafedra;
    private RelayCommand addNewCafedra;
    private RelayCommand removeCafedra;
    private RelayCommand searchCafCommand;
    private string searchCafedras;
    private RelayCommand openLast;
    private RelayCommand openNext;
    private Mediator mediator { get; set; }
    public CafedraModel? SelectedCafedra
    {
        get { return selectedCafedra; }
        set
        {
            selectedCafedra = value;
            OnPropertyChanged();
            if(selectedCafedra != null)
                if (SelectedCafedra?.Groups == null)
                    SelectedCafedra.Groups = new ObservableCollection<GroupModel>();
            mediator.OnCafedraChange(SelectedCafedra);
        }
    }
    public CafedraViewModel(Mediator mediator)
    {
        this.mediator = mediator;
        this.mediator.FacultetChange += OnFacultetChanged;
        
    }
    private void OnFacultetChanged(FacultetModel facultet)
    {
        Facultet = facultet;
        SearchedCafedras = facultet?.Cafedra;
    }
    public RelayCommand AddNewCafedra
    {
        get
        {            // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
            return addNewCafedra ??= new RelayCommand(obj =>
            {
                var caf = new CafedraModel
                {
                    Title = $"Новый кафедра",
                    ZavName = "Зав",
                    ZavSurname = "Завов",
                    ZavFathername = "Завович",
                    Groups = new ObservableCollection<GroupModel>(),
                    Mail = "Zac@etu.ru",
                    PhoneNumber = "+79999999999",
                    RoomNumber = 1234
                };
                Facultet.Cafedra?.Add(caf);
                SearchedCafedras = facultet.Cafedra;
                BaseViewModel.db.Cafedras.Add(caf);
                BaseViewModel.db.SaveChanges();
                mediator.OnCafedraListChanged(Facultet.Cafedra);
            }, obj => Facultet != null);
        }
    }
     public RelayCommand RemoveCafedra
     {
         get
         {            // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
             return removeCafedra ??= new RelayCommand(obj =>
             {
                 if (obj is not CafedraModel caf) return;
                 Facultet.Cafedra?.Remove(caf);
                 SearchedCafedras = facultet.Cafedra;
                 BaseViewModel.db.Cafedras.Remove(caf);
                 BaseViewModel.db.SaveChanges();
                 BaseViewModel.Instance.Cafedras?.Remove(caf);
                     
                 DbOperations.RemoveGroups();
                 DbOperations.RemoveStudents();
 
                 if (Facultet.Cafedra?.Count > 0)
                     SelectedCafedra = Facultet.Cafedra[^1];
             }, (obj) => Facultet?.Cafedra?.Count > 0 && SelectedCafedra != null);
         }
     }
    public ObservableCollection<CafedraModel> SearchedCafedras
    {
        get { return searchedCafedras; }
        set
        {
            searchedCafedras = value;
            OnPropertyChanged("SearchedCafedras");
        }
    }
    public string SearchCafedras
    {
        get { return searchCafedras; }
        set { searchCafedras = value; OnPropertyChanged("SearchCafedras"); }
    }
    private void SearchCaf()
    {
        if (string.IsNullOrWhiteSpace(SearchCafedras))
        {
            SearchedCafedras = new ObservableCollection<CafedraModel>(Facultet.Cafedra);
        }
        else
        {
            SearchedCafedras = new ObservableCollection<CafedraModel>(Facultet.Cafedra.Where(f => f.Title.Contains(SearchCafedras, StringComparison.OrdinalIgnoreCase)));
        }
    }
    public RelayCommand SearchCafCommand
    {
        get
        {            // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
            return searchCafCommand ??= new RelayCommand(obj =>
            {
                SearchCaf();
            });
        }
    }
    public FacultetModel Facultet
    {
        get { return facultet; }
        set
        {
            facultet = value;
            OnPropertyChanged("Facultet");
        }
    }
    public RelayCommand OpenPrev
    {
        get
        {
            return openLast ??= new RelayCommand(obj =>
            {
                mediator.OnIndexChange(0);
            });
        }
    }
    public RelayCommand OpenNext
    {
        get
        {
            return openNext ??= new RelayCommand(obj =>
            {
                mediator.OnIndexChange(2);

            }, (obj) => (SelectedCafedra != null));
        }
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}