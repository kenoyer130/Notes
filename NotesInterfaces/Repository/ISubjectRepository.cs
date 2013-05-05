using notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace notes.Repository
{
    public interface ISubjectRepository
    {
        IEnumerable<Subject> GetSubjects();

        Subject SaveSubject(Subject subject);

        void Delete(int subjectID);
    }
}
