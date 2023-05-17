using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UniversityDBExplorer.Models
{
    public class StudentModel : INotifyPropertyChanged
    {
        private string? name;
        private int? studBiletNumber;
        private string? location;
        private string? status;
        private string? flu;
        private string? mail;
        private string? phoneNumber;
        public int Id { get; set; }
        public string? Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
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
        public string? Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }
        public string? Flu
        {
            get { return flu; }
            set
            {
                flu = value;
                OnPropertyChanged("Flu");
            }
        }
        public string? Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged("Location");
            }
        }
        public int? StudBiletNumber
        {
            get { return studBiletNumber; }
            set
            {
                studBiletNumber = value;
                OnPropertyChanged("StudBiletNumber");
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
