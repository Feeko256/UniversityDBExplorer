using System.Linq;
using Microsoft.EntityFrameworkCore;
using UniversityDBExplorer.Models;
using UniversityDBExplorer.ViewModels;

namespace UniversityDBExplorer.Special;

public static class DbOperations
{
  public static void RemoveCafedras()
  {
      var cafedraList = BaseViewModel.db.Cafedras
  .FromSql($"SELECT * FROM Cafedras WHERE FacultetModelId IS NULL")
  .ToList<CafedraModel>();
      BaseViewModel.db.Cafedras.RemoveRange(cafedraList);
      BaseViewModel.db.SaveChanges();
  }
  public static void RemoveGroups()
  {
      var groupsList = BaseViewModel.db.Groups
  .FromSql($"SELECT * FROM Groups WHERE CafedraModelId IS NULL")
  .ToList<GroupModel  >();
      BaseViewModel.db.Groups.RemoveRange(groupsList);
      BaseViewModel.db.SaveChanges();
  }
  public static void RemoveStudents()
  {
      var studentsList = BaseViewModel.db.Students
  .FromSql($"SELECT * FROM Students WHERE GroupModelId IS NULL")
  .ToList<StudentModel>();
      BaseViewModel.db.Students.RemoveRange(studentsList);
      BaseViewModel.db.SaveChanges();
  }
}