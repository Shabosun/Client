using Client.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Client.Repository
{
    interface INoteRepository
    {

        Task<ObservableCollection<Note>> GetAllNotes();
        Task<Note> Get(Guid NoteId);
        Task<Note> Create(Note note);
        Task<Note> Update(Note note);
        Task<Note> Delete(Guid NoteId);
    }
}
