using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UniversityDBExplorer.Models
{
    public class GroupModel : INotifyPropertyChanged
    {
        private int groupNumber;
        private string? starostaName;
        private string? mail;
        private string? phoneNumber;
        private ObservableCollection<StudentModel>? student;
        private StudentModel? starosta;
        public int Id { get; set; }
        public StudentModel? Starosta
        {
            get { return starosta; }
            set
            {
                starosta = value;
                OnPropertyChanged("Starosta");
            }
        }
        public ObservableCollection<StudentModel>? Student
        {
            get { return student; }
            set
            {
                student = value;
                OnPropertyChanged("Student");
            }
        }

        public int GroupNumber 
        {
            get { return groupNumber;}
            set 
            {
                groupNumber = value;
                OnPropertyChanged("GroupNumber");
            } 
        }
        public string StarostaName
        {
            get { return starostaName; }
            set
            {
                starostaName = value;
                OnPropertyChanged("StarostaName");
            }
        }
        public string Mail
        {
            get { return mail; }
            set
            {
                mail = value;
                OnPropertyChanged("Mail");
            }
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
