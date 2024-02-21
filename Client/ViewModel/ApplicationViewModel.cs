using Client.Command;
using Client.Model;
using Client.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MoreLinq;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Windows;

namespace Client.ViewModel
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private const string BaseURL = "https://localhost:7148/api/Notes";
        private Note selectedNote;
        private NotesRepository _repository = new NotesRepository(BaseURL);


        
        public ObservableCollection<Note> _notes = new ObservableCollection<Note>();
        public ObservableCollection<Note> Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }
      
        

        public Note SelectedNote
        {
            get { return selectedNote; }
            set
            {
                selectedNote = value;
                OnPropertyChanged("SelectedNote");
            }
        }

        //Команды
        
        //Команда добавления
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(async obj =>  
                    {
                        EditNoteWindow editNoteWindow = new EditNoteWindow(new Note());
                        if(editNoteWindow.ShowDialog() == true)
                        {
                            Note note = editNoteWindow.Note;
                            await _repository.Create(note);
                            LoadNotes();

                        }
                  
                        
                    }));
            }
        }

        //Команда удаления
        private RelayCommand removeCommand;
        public  RelayCommand RemoveCommand {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(async obj =>
                    {
                        Note note = obj as Note;
                        if (note != null)
                        {
                            await _repository.Delete(note.Id);
                           
                            LoadNotes();


                        }
                    },
                    (obj) => Notes.Count > 0
                    ));
            }    
        }

        //Команда изменения 
        private RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                    (editCommand = new RelayCommand(async obj =>
                    {
                       
                        Note note = obj as Note;
                        if (note == null) return;


                        Note vm = new Note(selectedNote.Id, selectedNote.Title, selectedNote.Description);
                        EditNoteWindow editWindow = new EditNoteWindow(vm);


                        if(editWindow.ShowDialog() == true)
                        {
                            note.Id = editWindow.Note.Id;
                            note.Title = editWindow.Note.Title;
                            note.Description = editWindow.Note.Description;

                            await _repository.Update(note);
                            LoadNotes(); //возможно надо убрать


                        }

                        
                    }));

            }
        }



        //Команда очистки
        private RelayCommand clearCommand;
        public RelayCommand ClearCommand
        {
            get
            {
                return clearCommand ??
                   (clearCommand = new RelayCommand(obj =>
                   {

                       SelectedNote = null;
                   }));
            }
        }

        private async void LoadNotes()
        {
            Notes = new ObservableCollection<Note>(await _repository.GetAllNotes());
            //Notes = await _repository.GetAllNotes();
            
            Debug.Print(Notes.Count.ToString());

            
            
           
        }


        public ApplicationViewModel() {


             LoadNotes();

        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop ="")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
