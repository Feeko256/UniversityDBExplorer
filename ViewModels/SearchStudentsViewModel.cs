using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Media3D;
using UniversityDBExplorer.Models;
using UniversityDBExplorer.Special;
using UniversityDBExplorer.Special.Mediator;
using UniversityDBExplorer.Views;

namespace UniversityDBExplorer.ViewModels;

public class SearchStudentsViewModel : INotifyPropertyChanged
{
    public ObservableCollection<FacultetModel> Facultets { get; set; }
    public ObservableCollection<StudentModel> Students { get; set; }
    public ObservableCollection<GroupModel> Groups { get; set; }
    public ObservableCollection<CafedraModel> Cafedras { get; set; }
    private RelayCommand resetFilter;
    private int indexResetFac = -1;
    private int indexResetCaf = -1;
    private int indexResetGrp = -1;
    private FacultetModel selectedFac;
    private CafedraModel selectedCaf;
    private GroupModel selectedGrp;
    private StudentModel selectedStd;
    public SearchStudentsViewModel()
    {
        Facultets = BaseViewModel.Instance.Facultets;
        Cafedras = new ObservableCollection<CafedraModel>();
        Groups = new ObservableCollection<GroupModel>();
        Students = new ObservableCollection<StudentModel>();
        
        foreach (var a in Facultets)
        {
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
        //Students = BaseViewModel.Instance.Students;
        //Groups=BaseViewModel.Instance.Groups;
        // Cafedras=BaseViewModel.Instance.Cafedras;
    }

    public FacultetModel SelectedFacultet
    {
        get
        {
            return selectedFac;
        }
        set
        {
            selectedFac = value;
            OnPropertyChanged();
            FilterUpdate();
        }
    }
    public CafedraModel SelectedCafedra
    {
        get
        {
            return selectedCaf;
        }
        set
        {
            selectedCaf = value;
            OnPropertyChanged();
            FilterUpdate();
        }
    }

    public GroupModel SelectedGroup
    {
        get { return selectedGrp; }
        set
        {
            selectedGrp = value;
            OnPropertyChanged();
            FilterUpdate();
        }
    }

    public StudentModel SelectedStudent
    {
        get { return selectedStd; }
        set
        {
            selectedStd = value;
            OnPropertyChanged();

        }
    }

    private void FilterUpdate()
    {
        
        if (indexResetFac != -1 && indexResetCaf == -1 && indexResetGrp == -1)
        {
            if (SelectedFacultet != null)
            {
                Cafedras.Clear();
                foreach (var a in SelectedFacultet.Cafedra)
                {
                    Cafedras.Add(a);
                }
                Groups.Clear();
                foreach (var a in Cafedras)
                {
                    foreach (var b in a.Groups)
                    {
                        Groups.Add(b);
                    }
                }
                Students.Clear();
                foreach (var a in Groups)
                {
                    foreach (var b in a.Student)
                    {
                      Students.Add(b);  
                    }
                }
            }
        }
        
    }

    public int IndexResetFac
    {
        get { return indexResetFac; }
        set
        {
            indexResetFac = value;
            OnPropertyChanged("indexResetFac");
        }
    }
    public int IndexResetCaf
    {
        get { return indexResetCaf; }
        set
        {
            indexResetCaf = value;
            OnPropertyChanged("indexResetCaf");
        }
    }
    public int IndexResetGrp
    {
        get { return indexResetGrp; }
        set
        {
            indexResetGrp = value;
            OnPropertyChanged("indexResetGrp");
        }
    }
    public RelayCommand ResetFilter
    {
        get
        {
            // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
            return resetFilter ??= new RelayCommand(obj =>
            {
                IndexResetFac = -1;
                IndexResetCaf = -1;
                IndexResetGrp = -1;
                Facultets = BaseViewModel.Instance.Facultets;
                Cafedras.Clear();
                Groups.Clear();
                Students.Clear();
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
               }//, (obj) => (IndexResetFac != -1 || IndexResetCaf != -1 || IndexResetGrp != -1)
            });
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}