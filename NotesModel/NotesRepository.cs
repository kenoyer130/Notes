using notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace NotesModel
{
    public class NotesRepository : BaseRepository
    {
        public Note Create(Note note)
        {
            return Execute<Note>(() =>
                {
                    note.NoteID =
                        conn.Query<int>(@"INSERT note (CategoryID, Title, Description, BucketID) 
                                      VALUES (@CategoryID, @Title, @Description, @BucketID); SELECT CAST(SCOPE_IDENTITY() as int);",
                                       new { CategoryID = note.CategoryID, Title = note.Title, Description = note.Description, BucketID = note.Bucket }).Single();
                    return note;
                });
        }

        public Note Read(int noteID)
        {
            return Execute<Note>(() =>
            {
                return conn.Query<Note>
                     ("select NoteID, CategoryID, Title, Description, BucketID from notes where noteID=@noteID;",
                     new { noteID }).Single<Note>();
            });
        }

        public void Update(Note note)
        {
            if (note.NoteID == 0)
                throw new Exception("NoteID is not set.");

            Execute(() =>
            {
                conn.Execute(@"UPDATE note  set Title=@Title, CategoryID=@CategoryID, Description=@Description,  BucketID = @BucketID
                                   WHERE noteID=@NoteID",
                                    new { NoteID = note.NoteID, CategoryID = note.CategoryID, Title = note.Title, Description = note.Description, BucketID = note.Bucket });
            });
        }

        public void Delete(int noteID)
        {
            Execute(() =>
            {
                conn.Execute("delete from note where noteID=@noteID;", new { noteID });
            });
        }

        public IEnumerable<Note> GetCategoryNotes(int accountID)
        {
            return Execute<IEnumerable<Note>>(() =>
            {
                return conn.Query<Note>
                     (@"select no.NoteID, no.CategoryID, no.Title, no.Description, no.BucketID from note no
                        inner join category c on no.categoryID = c.categoryID
                        where accountID=@accountID ORDER BY c.CategoryID;",
                     new { accountID });
            });
        }
    }
}
