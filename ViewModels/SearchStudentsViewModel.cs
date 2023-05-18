using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Media3D;
using UniversityDBExplorer.Models;
using UniversityDBExplorer.Special;
using UniversityDBExplorer.Views;

namespace UniversityDBExplorer.ViewModels;

public class SearchStudentsViewModel: INotifyPropertyChanged
{
    public ObservableCollection<FacultetModel> Facultets { get; set; }
    public ObservableCollection<StudentModel> Students { get; set; }
    public ObservableCollection<GroupModel> Groups { get; set; }
    public ObservableCollection<CafedraModel> Cafedras { get; set; }
    public SearchStudentsViewModel()
    {
        Facultets = BaseViewModel.Instance.Facultets;
        Students = BaseViewModel.Instance.Students;
        Groups=BaseViewModel.Instance.Groups;
        Cafedras=BaseViewModel.Instance.Cafedras;
    }
    
    private RelayCommand resetFilter;
    private int indexResetFac = -1;
    private int indexResetCaf = -1;
    private int indexResetGrp = -1;
    private FacultetModel selectedFac;
    private CafedraModel selectedCaf;
    private GroupModel selectedGrp;

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
            return resetFilter ??= new RelayCommand(obj =>
            {
                IndexResetFac = -1;
                IndexResetCaf = -1;
                IndexResetGrp = -1;
                Facultets = BaseViewModel.Instance.Facultets;
                Students = BaseViewModel.Instance.Students;
                Groups = BaseViewModel.Instance.Groups;
                Cafedras = BaseViewModel.Instance.Cafedras;
            }, (obj) => (IndexResetFac != -1 || IndexResetCaf != -1 || IndexResetGrp != -1));
        }
    }

    public FacultetModel SelectedFac
    {

        get { return selectedFac; }
        set
        {
            selectedFac = value;
            OnPropertyChanged("selectedFac");
            if (SelectedFac != null)
            {
                Cafedras = SelectedFac.Cafedra;
            }
        }

    }
    

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}