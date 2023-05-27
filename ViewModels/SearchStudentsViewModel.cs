using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.Media3D;
using UniversityDBExplorer.Models;
using UniversityDBExplorer.Special;
using UniversityDBExplorer.Special.Mediator;
using UniversityDBExplorer.Views;

namespace UniversityDBExplorer.ViewModels;

public class SearchStudentsViewModel : INotifyPropertyChanged
{
    private ObservableCollection<StudentModel> searchedStudents;
    private string searchSearchStudents;
    private RelayCommand searchStudCommand;
    private RelayCommand removeStudent;
    private RelayCommand resetFilter;

    private FacultetModel selectedFac;
    private CafedraModel selectedCaf;
    private GroupModel selectedGrp;
    private StudentModel selectedStd;


    public ObservableCollection<FacultetModel> Facultets { get; set; }
    public ObservableCollection<StudentModel> Students { get; set; }
    public ObservableCollection<GroupModel> Groups { get; set; }
    public ObservableCollection<CafedraModel> Cafedras { get; set; }

    public SearchStudentsViewModel()
    {
        Facultets = new ObservableCollection<FacultetModel>();
        Cafedras = new ObservableCollection<CafedraModel>();
        Groups = new ObservableCollection<GroupModel>();
        Students = new ObservableCollection<StudentModel>();
        NewMethod();
    }

    public FacultetModel SelectedFacultet
    {
        get => selectedFac;
        set
        {
            selectedFac = value;
            OnPropertyChanged();
            if (SelectedFacultet != null)
            {
                Cafedras.Clear();
                if (SelectedFacultet.Cafedra == null)
                    SelectedFacultet.Cafedra = new ObservableCollection<CafedraModel>();
                foreach (var a in SelectedFacultet.Cafedra)
                {
                    Cafedras.Add(a);
                }

                Groups.Clear();
                foreach (var a in Cafedras)
                {
                    if (a.Groups == null)
                        a.Groups = new ObservableCollection<GroupModel>();
                    foreach (var b in a.Groups)
                    {
                        Groups.Add(b);
                    }
                }

                Students.Clear();
                foreach (var a in Groups)
                {
                    if (a.Student == null)
                        a.Student = new ObservableCollection<StudentModel>();
                    foreach (var b in a.Student)
                    {
                        Students.Add(b);
                    }
                }

                SearchedStudents = new ObservableCollection<StudentModel>(Students);
            }
        }
    }

    public CafedraModel SelectedCafedra
    {
        get => selectedCaf;
        set
        {
            selectedCaf = value;
            OnPropertyChanged();
            Groups.Clear();
            if (SelectedCafedra != null)
            {
                Groups.Clear();
                if (SelectedCafedra.Groups == null)
                    SelectedCafedra.Groups = new ObservableCollection<GroupModel>();
                foreach (var a in SelectedCafedra.Groups)
                {
                    Groups.Add(a);
                }

                Students.Clear();
                foreach (var a in Groups)
                {
                    if (a.Student == null)
                        a.Student = new ObservableCollection<StudentModel>();
                    foreach (var b in a.Student)
                    {
                        Students.Add(b);
                    }
                }

                SearchedStudents = new ObservableCollection<StudentModel>(Students);
            }
        }
    }

    public GroupModel SelectedGroup
    {
        get => selectedGrp;
        set
        {
            selectedGrp = value;
            OnPropertyChanged();

            if (SelectedGroup != null)
            {
                Students.Clear();
                if (SelectedGroup.Student == null)
                    SelectedGroup.Student = new ObservableCollection<StudentModel>();
                foreach (var a in SelectedGroup.Student)
                {
                    Students.Add(a);
                }

                SearchedStudents = new ObservableCollection<StudentModel>(Students);
            }
        }
    }

    public StudentModel SelectedStudent
    {
        get => selectedStd;
        set
        {
            selectedStd = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand ResetFilter
    {
        get
        {
            NewMethod();
            // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
            return resetFilter ??= new RelayCommand(obj => { NewMethod(); });
        }
    }

    private void NewMethod()
    {
        Facultets.Clear();
        Cafedras.Clear();
        Groups.Clear();
        Students.Clear();

        SelectedFacultet = null;
        SelectedCafedra = null;
        SelectedGroup = null;
        SelectedStudent = null;

        foreach (var a in BaseViewModel.Instance.Facultets)
        {
            Facultets.Add(a);
        }

        foreach (var a in Facultets)
        {
            if (a.Cafedra != null)
                foreach (var b in a.Cafedra)
                {
                    Cafedras.Add(b);
                }
        }

        foreach (var a in Cafedras)
        {
            if (a.Groups != null)
                foreach (var b in a.Groups)
                {
                    Groups.Add(b);
                }
        }

        foreach (var a in Groups)
        {
            if (a.Student != null)
                foreach (var b in a.Student)
                {
                    Students.Add(b);
                }
        }

        SearchedStudents = new ObservableCollection<StudentModel>(Students);
    }

    public ObservableCollection<StudentModel> SearchedStudents
    {
        get => searchedStudents;
        set
        {
            searchedStudents = value;
            OnPropertyChanged();
        }
    }

    public string SearchSearchStudents
    {
        get => searchSearchStudents;
        set
        {
            searchSearchStudents = value;
            OnPropertyChanged();
        }
    }

    private void SearchStud()
    {
        if (string.IsNullOrWhiteSpace(SearchSearchStudents))
        {
            SearchedStudents = new ObservableCollection<StudentModel>(Students);
        }
        else
        {
            SearchedStudents = new ObservableCollection<StudentModel>(Students.Where(f =>
                f.Name.Contains(SearchSearchStudents, StringComparison.OrdinalIgnoreCase)));
        }
    }

    public RelayCommand SearchStudCommand
    {
        get
        {
            // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
            return searchStudCommand ??= new RelayCommand(obj => { SearchStud(); });
        }
    }

    public RelayCommand RemoveStudent
    {
        get
        {
            // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
            return removeStudent ??= new RelayCommand(obj =>
            {
                if (obj is not StudentModel std) return;
                Students.Remove(std);
                SearchedStudents = Students;
                BaseViewModel.db.Students.Remove(std);
                BaseViewModel.db.SaveChanges();
                BaseViewModel.Instance.Students.Remove(std);

                if (Students?.Count > 0)
                    SelectedStudent = Students[^1];
            }, (obj) => (Students.Count > 0 && SelectedStudent != null));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}