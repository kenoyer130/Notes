using notes.Models;
using NotesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace notes.Controllers
{
    public class NotesController : BaseNotesController
    {
        private NotesRepository notesRepository;

        public NotesController()
        {
            notesRepository = new NotesRepository();
        }

        [HttpPost]
        [ActionName("Create")]
        public int Create(Note note)
        {
            return notesRepository.Create(note).NoteID;
        }

        [HttpPost]
        [ActionName("Read")]
        public void Read(int noteID)
        {
            notesRepository.Read(noteID);
        }

        [HttpPost]
        [ActionName("Update")]
        public void Update(Note note)
        {
            notesRepository.Update(note);
        }

        [HttpDelete]      
        public HttpResponseMessage Delete(int id)
        {
            return execute(() => { notesRepository.Delete(id); });
        }
    }
}
