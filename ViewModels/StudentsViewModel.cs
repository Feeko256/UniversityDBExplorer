using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using UniversityDBExplorer.Models;
using UniversityDBExplorer.Special;
using UniversityDBExplorer.Special.Mediator;
using UniversityDBExplorer.Views;

namespace UniversityDBExplorer.ViewModels;

public class StudentsViewModel : INotifyPropertyChanged
{
    private GroupModel group;
    private ObservableCollection<StudentModel> searchedStudents;
    private StudentModel selectedStudent;
    private RelayCommand addNewStudent;
    private RelayCommand removeStudent;
    private RelayCommand openLast;
    private RelayCommand openNext;
    private RelayCommand searchStdCommand;
    private string searchStudents;
    private Mediator mediator { get; set; }
    public GroupModel Group
    {
        get { return group; }
        set
        {
            group = value;
            OnPropertyChanged();
        }
    }
    public StudentModel? SelectedStudent
    {
        get { return selectedStudent; }
        set
        {
            selectedStudent = value;
            OnPropertyChanged();
            mediator.OnStudentChange(selectedStudent);
        }
    }
    public RelayCommand AddNewStudent
    {
        get
        {
            return addNewStudent ??= new RelayCommand(obj =>
            {
                // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
                var st = new StudentModel()
                {
                    Name = "Олег Олегович Олежко",
                    Flu="нет",
                    Location="СПБ", 
                    Mail="student@etu.ru",
                    PhoneNumber="+79211528982",
                    Status="Нормально", 
                    StudBiletNumber=830537
                };
                Group.Student?.Add(st);
                SearchedStudents = group.Student;
                BaseViewModel.db.Students.Add(st);
                BaseViewModel.db.SaveChanges();
                BaseViewModel.Instance.Students = BaseViewModel.db.Students.Local.ToObservableCollection(); 
            }, obj=>group!=null);
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
                group.Student.Remove(std);
                SearchedStudents = group.Student;
                BaseViewModel.db.Students.Remove(std);
                BaseViewModel.db.SaveChanges();
                BaseViewModel.Instance.Students.Remove(std);

                if (group.Student?.Count > 0)
                    SelectedStudent = group.Student[^1];
            }, (obj) => (group?.Student.Count > 0 && SelectedStudent != null));
        }
    }


    public ObservableCollection<StudentModel> SearchedStudents
    {
        get { return searchedStudents; }
        set
        {
            searchedStudents = value;
            OnPropertyChanged("SearchedStudents");
        }
    }
    public string SearchStudents
    {
        get { return searchStudents; }
        set { searchStudents = value; OnPropertyChanged("SearchStudents"); }
    }
    private void SearchStd()
    {
        if (string.IsNullOrWhiteSpace(SearchStudents))
        {
            SearchedStudents = new ObservableCollection<StudentModel>(Group.Student);
        }
        else
        {
            SearchedStudents = new ObservableCollection<StudentModel>(Group.Student.Where(f => f.Name.Contains(SearchStudents, StringComparison.OrdinalIgnoreCase)));
        }
    }
    public RelayCommand SearchStdCommand
    {
        get
        {
            // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
            return searchStdCommand ??= new RelayCommand(obj =>
            {
                SearchStd();
            });
        }
    }




    
    public RelayCommand OpenPrev
    {
        get
        {
            return openLast ??= new RelayCommand(obj =>
            {
                mediator.OnIndexChange(2);
            });
        }
    }

    public RelayCommand OpenNext
    {
        get
        {
            return openNext ??= new RelayCommand(obj =>
            {
                mediator.OnIndexChange(0);

            });
        }
    }

    private void OnGroupChange(GroupModel group)
    {
        Group = group;
        SearchedStudents = Group?.Student;
    }
    public StudentsViewModel(Mediator mediator)
    {
        this.mediator = mediator;
        this.mediator.GroupChange += OnGroupChange;
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}