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
        private Mediator mediator = new Mediator();
        private int selectedIndex;
        private RelayCommand updateDatabase;

        public ContentControl Facultets { get; set; }
        public ContentControl Cafedras { get; set; }
        public ContentControl Groups { get; set; }
        public ContentControl Students { get; set; }
        public ContentControl StudentSearch { get; set; }
        public FacultetsViewModel FacultetsViewModel { get; set; }
        public CafedraViewModel CafedraViewModel { get; set; }
        public GroupsViewModel GroupsViewModel { get; set; }
        public StudentsViewModel StudentsViewModel { get; set; }
        public SearchStudentsViewModel SearchStudentsViewModel { get; set; }

        public RelayCommand UpdateDatabase
        {
            get
            {
                return updateDatabase ??= new RelayCommand(obj =>
                {
                    foreach (var a in BaseViewModel.Instance.Facultets)
                    {
                        BaseViewModel.db.Entry(a).State = EntityState.Modified;
                        BaseViewModel.db.SaveChanges();
                    }
                });
            } 
        }
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
               DataContext = SearchStudentsViewModel = new SearchStudentsViewModel(mediator)
           };
       }
       public event PropertyChangedEventHandler? PropertyChanged;
       private void OnPropertyChanged([CallerMemberName] string prop = "")
       {
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
       }
    }
}