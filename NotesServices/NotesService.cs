using notes.Models;
using NotesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesServices
{
    public class NotesService
    {
        private NotesRepository notesRepository;
        Validator<Note> validator = new Validator<Note>();

        public Note Create(Note note)
        {
            validator.Validate(note);

            note.Bucket = Bucket.OneDay;
            return notesRepository.Create(note);
        }

        public void Update(Note note)
        {
            validator.Validate(note);

            notesRepository.Update(note);
        }

        public Note Read(int noteID)
        {
            return notesRepository.Read(noteID);
        }

        public void Delete(int noteID)
        {
            notesRepository.Delete(noteID);
        }
    }
}
