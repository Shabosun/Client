using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    public class Note : INotifyPropertyChanged
    {
        private Guid id;
        private string title;
        private string description;
        public DateTime CreationDate;
        public DateTime EditDate ; 
        
        public Note()
        {
            this.id = new Guid();
            this.title = "";
            this.description = "";
        }

        public Note(Guid id, string title, string description)
        {
            this.id = id;
            this.title = title;
            this.description = description;
        }

        public Guid Id {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }

            }


        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }

        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }



}
