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
    public class CafedraModel : INotifyPropertyChanged
    {
        private string? title;
        private string? zavName;
        private string? zavSurname;
        private string? zavFathername;
        private string? mail;
        private string? phoneNumber;
        private int? roomNumber;
        public int Id { get; set; }
        private ObservableCollection<GroupModel>? groups;

        public string? Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }
        public ObservableCollection<GroupModel>? Groups
        {
            get { return groups; }
            set
            {
                groups = value;
                OnPropertyChanged("Groups");
            }
        }
        public string? ZavName
        {
            get { return zavName; }
            set
            {
                zavName = value;
                OnPropertyChanged("ZavName");
            }
        }
        public string? ZavSurname
        {
            get { return zavSurname; }
            set
            {
                zavSurname = value;
                OnPropertyChanged("ZavSurname");
            }
        }
        public string? ZavFathername
        {
            get { return zavFathername; }
            set
            {
                zavFathername = value;
                OnPropertyChanged("ZavFathername");
            }
        }
        public string? Mail
        {
            get { return mail; }
            set
            {
                mail = value;
                OnPropertyChanged("Mail");
            }
        }
        public string? PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }
        public int? RoomNumber
        {
            get { return roomNumber; }
            set
            {
                roomNumber = value;
                OnPropertyChanged("RoomNumber");
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
