using Client.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Repository
{
    class NotesRepository : INoteRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseURL;

        public NotesRepository(string baseURL)
        {
            _httpClient = new HttpClient();
            _baseURL  = baseURL;
        }

        public async Task<Note> Create(Note note)
        {

            string noteJson = JsonConvert.SerializeObject(note);

            var noteContent = new StringContent(noteJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(_baseURL, noteContent);

            if (response.IsSuccessStatusCode)
            {
                string createdNoteJson = await response.Content.ReadAsStringAsync();

                Note createdNote = JsonConvert.DeserializeObject<Note>(createdNoteJson);

                return createdNote;
            }
            else return null;
        }

        public async Task<Note> Delete(Guid NoteId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{_baseURL}/{NoteId}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Note>(responseBody);
        }

        public async Task<Note> Get(Guid NoteId)
        {
            

            HttpResponseMessage response = await _httpClient.GetAsync($"{_baseURL}/{NoteId}");

            response.EnsureSuccessStatusCode();

           string responseBody =  await response.Content.ReadAsStringAsync();

           
            
            return JsonConvert.DeserializeObject<Note>(responseBody);
            
        }

        public async Task<ObservableCollection<Note>> GetAllNotes()
        {
            //List<Note> notes = new List<Note>();

            HttpResponseMessage response = await _httpClient.GetAsync(_baseURL);
            response.EnsureSuccessStatusCode();

            Debug.Print( "Code: "+ $"{response.StatusCode}" + " Message: " + $"{response.RequestMessage}");
            
           

            string responseBody = await response.Content.ReadAsStringAsync();

            Debug.Print("Reponse Body " + $"{responseBody}");

            
            return JsonConvert.DeserializeObject<ObservableCollection<Note>>(responseBody);
        }

        public async Task<Note> Update(Note note)
        {

            string noteJson = JsonConvert.SerializeObject(note);

            var noteContent = new StringContent(noteJson, Encoding.UTF8, "application/json");
            //HttpResponseMessage response = await _httpClient.PutAsync("$\"{_baseURL}/{note.Id}")
            HttpResponseMessage response = await _httpClient.PutAsync($"{_baseURL}", noteContent);

            if (response.IsSuccessStatusCode)
            {
                string updatedNoteJson = await response.Content.ReadAsStringAsync();

                Note updatedNote = JsonConvert.DeserializeObject<Note>(updatedNoteJson);

                return updatedNote;
            }
            else
            {
                return null;
            }
        }
    }
}
