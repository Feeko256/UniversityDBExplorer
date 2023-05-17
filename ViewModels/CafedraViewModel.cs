using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using UniversityDBExplorer.Models;
using UniversityDBExplorer.Special;
using UniversityDBExplorer.Special.Mediator;

namespace UniversityDBExplorer.ViewModels;

public class CafedraViewModel : INotifyPropertyChanged
{
    private FacultetModel facultet;

    public FacultetModel Facultet
    {
        get { return facultet; }
        set
        {
            facultet = value;
            OnPropertyChanged("Facultet");
        }
    }

    public ObservableCollection<CafedraModel> Cafedras { get; set; }
  
    private CafedraModel? selectedCafedra;
    private RelayCommand addNewCafedra;
    private RelayCommand removeCafedra;
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
    private void OnFacultetChanged(FacultetModel facultet) => Facultet = facultet;
    public RelayCommand AddNewCafedra
    {
        get
        {
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
                BaseViewModel.db.Cafedras.Add(caf);
                BaseViewModel.db.SaveChanges();
            }, obj => Facultet != null);
        }
    }

     public RelayCommand RemoveCafedra
     {
         get
         {
             return removeCafedra ??= new RelayCommand(obj =>
             {
                 if (obj is not CafedraModel caf) return;
                 Facultet.Cafedra?.Remove(caf);
                 BaseViewModel.db.Cafedras.Remove(caf);
                 BaseViewModel.db.SaveChanges();
                     
                 DbOperations.RemoveGroups();
                 DbOperations.RemoveStudents();
 
                 if (Facultet.Cafedra?.Count > 0)
                     SelectedCafedra = Facultet.Cafedra[^1];
             }, obj => Facultet is { Cafedra.Count: > 0 });
         }
     }
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}