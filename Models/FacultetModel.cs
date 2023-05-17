using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UniversityDBExplorer.Special;

namespace UniversityDBExplorer.Models
{
    public class FacultetModel : INotifyPropertyChanged
    {
        private string? title;
        private ObservableCollection<CafedraModel>? cafedra;
        private string? dekanName;
        private string? dekanLastName;
        private string? dekanFatherName;
        private string? mail;
        private string? phoneNumber;
        private int? roomNumber;
        
        public int Id { get; set; }
        public string? Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }
        public ObservableCollection<CafedraModel>? Cafedra
        {
            get { return cafedra; }
            set
            {
                cafedra = value;
                OnPropertyChanged("Cafedra");
            }
        }
        public string? DekanName
        {
            get { return dekanName; }
            set
            {
                dekanName = value;
                OnPropertyChanged("DekanName");
            }
        }
        public string? DekanLastName
        {
            get { return dekanLastName; }
            set
            {
                dekanLastName = value;
                OnPropertyChanged("DekanLastName");
            }
        }
        public string? DekanFatherName
        {
            get { return dekanFatherName; }
            set
            {
                dekanFatherName = value;
                OnPropertyChanged("DekanFatherName");
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

        [NotMapped]
        public int CafedraCount
        {
            get
            {
                if(cafedra!=null)
                    return Cafedra.Count;
                return 0;
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
