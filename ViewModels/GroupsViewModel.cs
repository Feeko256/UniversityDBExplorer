using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UniversityDBExplorer.Models;
using UniversityDBExplorer.Special;
using UniversityDBExplorer.Special.Mediator;

namespace UniversityDBExplorer.ViewModels;

public class GroupsViewModel : INotifyPropertyChanged
{
    private CafedraModel cafedra;
    private GroupModel selectedGroup;
    private RelayCommand addNewGroup;
    private StudentModel selectedStarosta;

    private RelayCommand removeGroup;
    private Mediator mediator { get; set; }
    public CafedraModel Cafedra
    {
        get { return cafedra; }
        set
        {
            cafedra = value;
            OnPropertyChanged();
        }
    }

    public StudentModel? SelectedStarosta
    {
        get { return selectedStarosta; }
        set
        {
            selectedStarosta = value;
            OnPropertyChanged();
        }
    }
    public GroupModel? SelectedGroup
    {
        get { return selectedGroup; }
        set
        {
            selectedGroup = value;
            OnPropertyChanged();
            if (selectedGroup != null)
                if (SelectedGroup.Student == null)
                    SelectedGroup.Student = new ObservableCollection<StudentModel>();
            mediator.OnGroupChange(SelectedGroup);
        }
    }
    public RelayCommand AddNewGroup
    {
        get
        {
            return addNewGroup ??= new RelayCommand(obj =>
            {
                var gr = new GroupModel
                {
                    GroupNumber = 1234,
                    Student = new ObservableCollection<StudentModel>(),
                    Mail = "starosta@etu.ru",
                    PhoneNumber = "+73213332215",
                    StarostaName = "ФИО",
                    Starosta = new StudentModel
                    {
                        Name = "Олег Олегович Олежко",
                        Flu = "нет",
                        Location = "СПБ",
                        Mail = "student@etu.ru",
                        PhoneNumber = "+79211528982",
                        Status = "Нормально",
                        StudBiletNumber = 830537
                    }
                };
                Cafedra.Groups?.Add(gr);
                BaseViewModel.db.Groups.Add(gr);
                BaseViewModel.db.SaveChanges();
            }, obj => Cafedra != null);
        }
    }
    public RelayCommand RemoveGroup
    {
        get
        {
            return removeGroup ??= new RelayCommand(obj =>
            {
                if (obj is not GroupModel group) return;
                Cafedra.Groups?.Remove(group);
                BaseViewModel.db.Groups.Remove(group);
                BaseViewModel.db.SaveChanges();
                    
                DbOperations.RemoveStudents();
 
                if (Cafedra.Groups?.Count > 0)
                    SelectedGroup = Cafedra.Groups[^1];

            }, obj => Cafedra is { Groups.Count: > 0 });
        }
    }
    private void OnCafedraChange(CafedraModel cafedra) => Cafedra = cafedra;
    public GroupsViewModel(Mediator mediator)
    {
        this.mediator = mediator;
        this.mediator.CafedraChange += OnCafedraChange;
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}