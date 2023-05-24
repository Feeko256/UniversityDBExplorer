using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UniversityDBExplorer.Models;

namespace UniversityDBExplorer.Special.Mediator;

public class Mediator
{
    public event Action<FacultetModel> FacultetChange;
    public event Action<CafedraModel> CafedraChange;
    public event Action<GroupModel> GroupChange;
    public event Action<StudentModel> StudentChange;

    public event Action<int> IndexChange; 

    public void OnFacultetChanged(FacultetModel facultet)
    {
        FacultetChange?.Invoke(facultet);
    }
    public void OnCafedraChange(CafedraModel cafedra)
    {
        CafedraChange?.Invoke(cafedra);
    }
    public void OnGroupChange(GroupModel group)
    {
        GroupChange?.Invoke(group);
    }
    public void OnStudentChange(StudentModel student)
    {
        StudentChange?.Invoke(student);
    }
    public void OnIndexChange(int index)
    {
        IndexChange?.Invoke(index);
    }
}