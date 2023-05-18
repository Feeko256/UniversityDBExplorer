using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using UniversityDBExplorer.Models;
using UniversityDBExplorer.Special;
using UniversityDBExplorer.Views;

namespace UniversityDBExplorer.ViewModels;

public class BaseViewModel
{
    private static readonly BaseViewModel instance = new BaseViewModel();

    public static ApplicationContext db = new ApplicationContext();
    public ObservableCollection<FacultetModel> Facultets { get; set; }
    public ObservableCollection<CafedraModel> C { get; set; }
    public ObservableCollection<GroupModel> G { get; set; }
    public ObservableCollection<StudentModel> S { get; set; }


    
    public static ObservableCollection<GroupModel> Groups;
    public static ObservableCollection<StudentModel> Students;
    public static BaseViewModel Instance
    {
        get
        {
            return instance;
        }
    }
    static BaseViewModel()
    {

        db.Database.EnsureCreated();
        db.Facultets.Load();
        db.Cafedras.Load();
        db.Groups.Load();
        db.Students.Load();
        Instance.Facultets = db.Facultets.Local.ToObservableCollection(); 
        Instance.S = db.Students.Local.ToObservableCollection(); 
        //Instance.Cafedras = db.Cafedras.Local.ToObservableCollection();
       // var c1 = new CafedraModel { Title = "ass" };
      ///  var c2 = new CafedraModel { Title = "ass2" };
    //    Cafedras.Add(c1);
        //Cafedras.Add(c2);
    //   db.Cafedras.Add(c1);
      //  db.Cafedras.Add(c2);
        //db.SaveChanges();
    }
}