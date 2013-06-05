using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using notes.Models;

namespace NotesModel
{
    public static class IListExtensions
    {
        public static void Shuffle<T>(this List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

    public class CreateTestRunCommand : BaseRepository
    {
        private int accountID;

        public CreateTestRunCommand(int accountID)
        {
            this.accountID = accountID;
        }     

        public void Execute()
        {
            // empty out any existing test run
            clearTestRun();

            // pull out all the notes
            List<int> notes = getNotes();

            // randomize the order
            notes.Shuffle();

            // save the randomized notes. todo this is horrible.
            foreach (int noteID in notes)
            {
                insertTestRunNote(noteID);
            }           
        }

        private void clearTestRun()
        {
            Execute(() =>
            {
                conn.Execute("delete from TestRun where accountID=@accountID;", new { accountID });
            });
        }

        private List<int> getNotes()
        {
            return Execute<List<int>>(() =>
            {
                return conn.Query<int>(@"Select NoteID FROM dbo.Note n 
                                            inner join dbo.category c on c.categoryID = n.categoryID
                                            Left join dbo.NoteTestRunId nt on nt.noteid=n.noteid
                                            where c.accountID=@AccountID
                                            and (n.BucketID=0 
                                                OR (n.BucketID=2 nt.lastRun > DATEADD(day, -7, GetDate())
                                                OR (n.BucketID=3 nt.lastRun > DATEADD(day, -30, GetDate())
                                            )
                                        ",
                                   new { AccountID = accountID }).ToList();
               
            });
        }

        private void insertTestRunNote(int noteID)
        {
            Execute(() =>
            {
                conn.Query(@"INSERT testrun (accountID, noteID) VALUES (@accountID, @noteID)",
                                       new { accountID = accountID, noteID = noteID });
            });
        }
    }
}
