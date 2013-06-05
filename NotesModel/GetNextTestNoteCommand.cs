using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using notes.Models;

namespace NotesModel
{
    public class GetNextTestNoteCommand : BaseRepository
    {
        private int accountID;
        private int next;
        public GetNextTestNoteCommand(int accountID, int next)
        {
            this.accountID = accountID;
            this.next = next;
        }

        public Note Execute()
        {
            return Execute<Note>(() =>
            {
                return conn.Query<Note>(@"SELECT * FROM (
                                            SELECT
                                                ROW_NUMBER() OVER (ORDER BY testrunid ASC) AS rownumber,
                                                noteid
                                              FROM testrun
                                            WHERE accountid=@accountID
                                        ) as tmp WHERE rownumber = @next", new { accountID = accountID, next = next }).Single();
            });
        }
    }
}
