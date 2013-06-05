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
    public class TestRunController : BaseNotesController
    {
        [HttpPost]
        [ActionName("Create")]
        public void Create()
        {
            int accountID = getAccountID();

            var createTestRunCommand = new CreateTestRunCommand(accountID);
            createTestRunCommand.Execute();
        }

        [HttpPost]
        [ActionName("Read")]
        public Note Read(int next)
        {
            GetNextTestNoteCommand getNextTestNoteCommand = new GetNextTestNoteCommand(getAccountID(), next);
            return getNextTestNoteCommand.Execute();
        }
    }
}
