using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using UniversityDBExplorer.Models;
using UniversityDBExplorer.Special;
using UniversityDBExplorer.Special.Mediator;
using UniversityDBExplorer.Views;

namespace UniversityDBExplorer.ViewModels;

public class GroupsViewModel : INotifyPropertyChanged
{
    private ObservableCollection<GroupModel> searchedGroups;
    private CafedraModel cafedra;
    private GroupModel selectedGroup;
    private RelayCommand addNewGroup;
    private StudentModel selectedStarosta;
    private RelayCommand searchGrpCommand;
    private string searchGroups;

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
                };
                Cafedra.Groups?.Add(gr);
                SearchedGroups = cafedra.Groups;
                BaseViewModel.db.Groups.Add(gr);
                BaseViewModel.db.SaveChanges();
                BaseViewModel.Instance.Groups.Add(gr);
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
                SearchedGroups = cafedra.Groups;
                DbOperations.RemoveStudents();
                BaseViewModel.Instance.Groups.Remove(group);

                if (Cafedra.Groups?.Count > 0)
                    SelectedGroup = Cafedra.Groups[^1];

            }, (obj) => (Cafedra?.Groups?.Count > 0 && selectedGroup != null));
        }
    }



    public ObservableCollection<GroupModel> SearchedGroups
    {
        get { return searchedGroups; }
        set
        {
            searchedGroups = value;
            OnPropertyChanged("SearchedGroups");
        }
    }
    public string SearchGroups
    {
        get { return searchGroups; }
        set { searchGroups = value; OnPropertyChanged("SearchGroups"); }
    }
    private void SearchGrp()
    {
        
        bool isNumber = int.TryParse(SearchGroups, out int number);

        if (!isNumber)
        {
            SearchedGroups = new ObservableCollection<GroupModel>(Cafedra.Groups);
        }
        else
        {
            SearchedGroups = new ObservableCollection<GroupModel>(Cafedra.Groups.Where(f => f.GroupNumber.ToString().Contains(SearchGroups)));
        }
    }
    public RelayCommand SearchGrpCommand
    {
        get
        {
            return searchGrpCommand ??= new RelayCommand(obj =>
            {
                SearchGrp();
            });
        }
    }



    private void OnCafedraChange(CafedraModel cafedra)
    {
        Cafedra = cafedra;
        SearchedGroups = cafedra?.Groups;
    }
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