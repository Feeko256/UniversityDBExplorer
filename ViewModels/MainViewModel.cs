using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using UniversityDBExplorer.Models;
using UniversityDBExplorer.Special;
using UniversityDBExplorer.Special.Mediator;
using UniversityDBExplorer.Views;

namespace UniversityDBExplorer.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        //public SelectViewModel SelectVM{ get; set; } 

        // public ObservableCollection<FacultetModel> Facultets { get; set; }
        //public ObservableCollection<CafedraModel> Cafedras { get; set; }
        //public ObservableCollection<GroupModel> Groups { get; set; }

       // private ObservableCollection<FacultetModel> searchedFacultets;

        
       // ApplicationContext db = new ApplicationContext();

    /*    private RelayCommand addNewFacultet;
        private RelayCommand addNewCafedra;
        private RelayCommand addNewGroup;
        private RelayCommand addNewStudent;

        private RelayCommand removeGroup;
        private RelayCommand removeFacultet;
        private RelayCommand removeCafedra;
        private RelayCommand removeStudent;*/

        private RelayCommand updateFacultets;


      /*  private RelayCommand openCafedra;
        private RelayCommand openGroup;
        private RelayCommand openStudent;

        private RelayCommand returnFacultet;
        private RelayCommand returnCafedra;
        private RelayCommand returnGroup;

        private RelayCommand searchFacCommand;

        
        private int selectedTabIndex;
        
        private string searchFacultets;*/

     /*   #region find_facultet 
        public ObservableCollection<FacultetModel> SearchedFacultets
        {
            get { return searchedFacultets; }
            set { searchedFacultets = value;
                OnPropertyChanged("SearchedFacultets"); }
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
        

        #endregion*/


    /* public UserControl Cafedras
     {
         get { return cafedras; }
         set
         {
             cafedras = value;
             OnPropertyChanged("Cafedras");
         }
     }
        public int SelectedTabIndex
        {
            get { return selectedTabIndex; }
            set
            {
                selectedTabIndex = value;
                OnPropertyChanged(nameof(SelectedTabIndex));
            }
        }
    /*    #region Adding
        public RelayCommand AddNewFacultet
        {
            get
            {
                return addNewFacultet ??= new RelayCommand(obj =>
                {
                    FacultetModel fac = new FacultetModel {Title = $"Новый факультет", DekanName = "Декан", DekanLastName="Деканов", DekanFatherName="Декановч", Cafedra = new ObservableCollection<CafedraModel>(), Mail="Почта", PhoneNumber="+79999999999", RoomNumber=1234};
                    Facultets.Add(fac);
                    SearchedFacultets = Facultets;
                    SelectVM.SelectedFacultet = fac;
                    db.Facultets.Add(fac);
                    db.SaveChanges();
                });
            }
        }       */
    /*
        public RelayCommand AddNewCafedra
        {
            get
            {
                return addNewCafedra ??
                  (addNewFacultet = new RelayCommand(obj =>
                  {
                      if (SelectVM?.SelectedFacultet?.Cafedra == null) 
                          SelectVM.SelectedFacultet.Cafedra = new ObservableCollection<CafedraModel>();
                      CafedraModel caf = new CafedraModel
                      {
                          Title = $"Новая кафедра {SelectVM.SelectedFacultet.Cafedra.Count + 1}",
                          Groups = new ObservableCollection<GroupModel>(),
                          ZavName = "Зав",
                          ZavSurname = "Завов",
                          ZavFathername = "Завович",
                          Mail = "leti@etu.ru",
                          PhoneNumber = "+78123332255",
                          RoomNumber = 1234
                      };
                      SelectVM.SelectedFacultet.Cafedra.Add(caf);
                      SelectVM.SelectedCafedra = caf;
                      db.Cafedras.Add(caf);
                      db.SaveChanges();

                  }, (obj) => SelectVM.SelectedFacultet != null));
            }
        }     
        public RelayCommand AddNewGroup
        {
            get
            {
                return addNewGroup ??
                  (addNewGroup = new RelayCommand(obj =>
                  {
                      if (SelectVM.SelectedCafedra.Groups == null) 
                          SelectVM.SelectedCafedra.Groups = new ObservableCollection<GroupModel>();
                      GroupModel group = new GroupModel
                      {
                          GroupNumber = 1234,
                          Student = new ObservableCollection<StudentModel>(),
                          Mail = "starosta@etu.ru",
                          PhoneNumber = "+73213332215",
                          StarostaName = "ФИО",
                          Starosta = new StudentModel { Name = "Олег Олегович Олежко", Flu = "нет", Location = "СПБ", Mail = "student@etu.ru", PhoneNumber = "+79211528982", Status = "Нормально", StudBiletNumber = 830537 }
                      };
                      SelectVM.SelectedCafedra.Groups.Add(group);
                      SelectVM.SelectedGroup = group;
                      db.Groups.Add(group);
                      db.SaveChanges();
                  }, (obj) => SelectVM.SelectedCafedra != null));
            }
        }
        public RelayCommand AddNewStudent
        {
            get
            {
                return addNewStudent ??
                  (addNewStudent = new RelayCommand(obj =>
                  {
                      if (SelectVM?.SelectedGroup.Student == null) SelectVM.SelectedGroup.Student = new ObservableCollection<StudentModel>();
                      StudentModel student = new StudentModel 
                      {
                       Name = "Олег Олегович Олежко", Flu="нет", Location="СПБ", Mail="student@etu.ru", PhoneNumber="+79211528982", Status="Нормально", StudBiletNumber=830537 
                       };
                      SelectVM.SelectedGroup.Student.Add(student);
                      SelectVM.SelectedStudent = student;
                      db.Students.Add(student);
                      db.SaveChanges();
                  }, (obj) => SelectVM.SelectedGroup != null));
            }
        }*/
    /*
        #endregion
        #region Removing
        public RelayCommand RemoveFacultet
        {
            get
            {
                return removeFacultet ??= new RelayCommand(obj =>
                {
                    FacultetModel? fac = obj as FacultetModel;
                    if (fac != null)
                    {
                        if (fac == null) return;

                        Facultets.Remove(fac);
                        SearchedFacultets = Facultets;
                        db.Facultets.Remove(fac);
                        db.SaveChanges();

                        RemoveCafedras();
                        RemoveGroups();
                        RemoveStudents();

                        if (Facultets?.Count > 0)
                            SelectVM.SelectedFacultet = Facultets[Facultets.Count - 1];
                    }
                }, obj => Facultets?.Count > 0);
            }
        }

        public RelayCommand RemoveCafedra
        {
            get
            {
                return removeCafedra ??
                       (removeCafedra = new RelayCommand(obj =>
                       {
                           if (obj is CafedraModel caf)
                           {
                               SelectVM.SelectedFacultet?.Cafedra.Remove(caf);
                               db.Cafedras.Remove(caf);
                               db.SaveChanges();

                               RemoveGroups();
                               RemoveStudents();

                               if (SelectVM.SelectedFacultet?.Cafedra.Count > 0)
                                   SelectVM.SelectedCafedra = SelectVM.SelectedFacultet.Cafedra[SelectVM.SelectedFacultet.Cafedra.Count - 1];

                           }
                       }, (obj) => SelectVM.SelectedFacultet?.Cafedra?.Count > 0));
            }
        }
        public RelayCommand RemoveGroup
        {
            get
            {
                return removeGroup ??= new RelayCommand(obj =>
                {
                    GroupModel? group = obj as GroupModel;
                    if (group != null)
                    {
                        SelectVM?.SelectedCafedra?.Groups.Remove(group);

                        db.Groups.Remove(group);
                        db.SaveChanges();

                        RemoveStudents();

                        if (SelectVM?.SelectedCafedra?.Groups?.Count > 0)
                            SelectVM.SelectedGroup = SelectVM.SelectedCafedra.Groups[SelectVM.SelectedCafedra.Groups.Count - 1];
                    }

                }, (obj) => SelectVM.SelectedCafedra?.Groups?.Count > 0);
            }
        }
        public RelayCommand RemoveStudent
        {
            get
            {
                return removeStudent ??= new RelayCommand(obj =>
                {
                    StudentModel? student = obj as StudentModel;
                    if (student != null)
                    {
                        SelectVM.SelectedGroup?.Student?.Remove(student);

                        if(student == SelectVM?.SelectedGroup?.Starosta)
                        {
                            SelectVM.SelectedGroup.Starosta = new StudentModel { Name = "Староста", Flu = "", Location = "", Mail = "", PhoneNumber = "", Status = "", StudBiletNumber = 0 };
                            db.SaveChanges();
                        }
                        else
                            db.Students.Remove(student);
                        db.SaveChanges();

                        if (SelectVM?.SelectedGroup?.Student?.Count > 0)
                            SelectVM.SelectedStudent = SelectVM.SelectedGroup.Student[SelectVM.SelectedGroup.Student.Count - 1];
                    }

                }, (obj) => SelectVM.SelectedGroup?.Student?.Count > 0);
            }
        }
        #endregion*/
        public RelayCommand UpdateFacultets
        {
            get
            {
                return updateFacultets ??= new RelayCommand(obj =>
                {
                    foreach (var a in BaseViewModel.Instance.Facultets)
                    {
                        BaseViewModel.db.Entry(a).State = EntityState.Modified;
                        BaseViewModel.db.SaveChanges();
                    }
                });
            } 
        }
        /*
        public RelayCommand SearchFacCommand
        {
            get
            {
                return searchFacCommand ??= new RelayCommand(obj =>
                {
                    SearchFac();
                });
            }
        }

        #region TabScrollingUp
        public RelayCommand OpenCafedra
        {
            get
            {
                return openCafedra ??= new RelayCommand(obj =>
                {
                    SelectedTabIndex = 1;
                }, (obj) => SelectVM.SelectedFacultet != null);
            }
        }
        public RelayCommand OpenGroup
        {
            get
            {
                return openGroup ??= new RelayCommand(obj =>
                {
                    SelectedTabIndex = 2;
                }, (obj) => SelectVM.SelectedCafedra != null);
            }
        }
        public RelayCommand OpenStudent
        {
            get
            {
                return openStudent ??= new RelayCommand(obj =>
                {
                    SelectedTabIndex = 3;
                }, (obj) => SelectVM.SelectedGroup != null);
            }
        }
        #endregion*/
        /*#region TabScrollDown
        public RelayCommand ReturnFacultet
        {
            get
            {
                return returnFacultet ??= new RelayCommand(obj =>
                {
                    SelectedTabIndex = 0;

                });
            }
        }

        public RelayCommand ReturnCafedra
        {
            get
            {
                return returnCafedra ??= new RelayCommand(obj =>
                {
                    SelectedTabIndex = 1;

                });
            }
        }
        public RelayCommand ReturnGroup
        {
            get
            {
                return returnGroup ??= new RelayCommand(obj =>
                {
                    SelectedTabIndex = 2;
                });
            }
        }
        #endregion*/
    
       private Mediator mediator = new Mediator();
       public ContentControl  Facultets { get; set; }
       public ContentControl  Cafedras { get; set; }
       public ContentControl  Groups { get; set; }
       public ContentControl  Students { get; set; }
       public ContentControl  StudentSearch { get; set; }
       public FacultetsViewModel FacultetsViewModel { get; set; }
        public CafedraViewModel CafedraViewModel { get; set; }
        public GroupsViewModel GroupsViewModel { get; set; }
        public StudentsViewModel StudentsViewModel { get; set; }
        public SearchStudentsViewModel SearchStudentsViewModel { get; set; }
        private int selectedIndex;

        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                OnPropertyChanged();
            }
        }
        private void OnIndexChange(int index)
        {
            SelectedIndex = index;
        }


       public MainViewModel()
       {
           mediator.IndexChange += OnIndexChange;
           Facultets = new Facultets()
           {
               DataContext = FacultetsViewModel = new FacultetsViewModel(mediator)
           };
           Cafedras = new Cafedras()
           {
               DataContext = CafedraViewModel = new CafedraViewModel(mediator)
           };
           Groups = new Groups()
           {
               DataContext = GroupsViewModel = new GroupsViewModel(mediator)
           };
           Students = new Students()
           {
               DataContext = StudentsViewModel = new StudentsViewModel(mediator)
           };
           StudentSearch = new StudentsSearch()
           {
               DataContext = SearchStudentsViewModel = new SearchStudentsViewModel()
           };
           
           // Cafedras = new Cafedras();
           // SelectVM = new SelectViewModel();
           //  db.Database.EnsureCreated();
           //  db.Facultets.Load();
           //  db.Cafedras.Load();
           //  db.Students.Load();
           //  db.Groups.Load();
           // Facultets = db.Facultets.Local.ToObservableCollection();
           // Cafedras = db.Cafedras.Local.ToObservableCollection();
           //  Groups = db.Groups.Local.ToObservableCollection();
           /*if(db.Students != null)
           {
               ObservableCollection<StudentModel> students = new ObservableCollection<StudentModel>(db.Students.Where(f => f.Name != ""));
               SearchAllStudents = students;
           }*/
           /*     if (Facultets != null)
                    SearchedFacultets = new ObservableCollection<FacultetModel>(Facultets);
    
    
                if(Facultets != null)
                    SelectVM.SelectedFacultet = Facultets.FirstOrDefault<FacultetModel>();*/
       }

       public event PropertyChangedEventHandler? PropertyChanged;
       private void OnPropertyChanged([CallerMemberName] string prop = "")
       {
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
       }
    }
}