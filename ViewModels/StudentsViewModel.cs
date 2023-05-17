using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UniversityDBExplorer.Models;
using UniversityDBExplorer.Special;
using UniversityDBExplorer.Special.Mediator;
using UniversityDBExplorer.Views;

namespace UniversityDBExplorer.ViewModels;

public class StudentsViewModel : INotifyPropertyChanged
{
    private GroupModel group;
    private StudentModel selectedStudent;
    private RelayCommand addNewStudent;
    private RelayCommand removeStudent;
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
                BaseViewModel.db.Students.Add(st);
                BaseViewModel.db.SaveChanges();
            }, obj=>group!=null);
        }
    }
   
    public RelayCommand RemoveStudent
    {
        get
        {
            return removeStudent ??= new RelayCommand(obj =>
            {
                if (obj is not StudentModel std) return;
                group.Student.Remove(std);
                //SearchedStudents = group.Student;
                BaseViewModel.db.Students.Remove(std);
                BaseViewModel.db.SaveChanges();


                if (group.Student?.Count > 0)
                    SelectedStudent = group.Student[^1];
            }, (obj) => (group?.Student.Count > 0 && selectedStudent != null));
        }
    } 
    private void OnGroupChange(GroupModel group) => Group = group;
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