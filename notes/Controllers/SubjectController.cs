using notes.Model.Repository;
using notes.Models;
using notes.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace notes.Controllers
{
    public class SubjectController : BaseNotesController
    {
        private readonly ISubjectRepository subjectRepository;

        public SubjectController()
        {
            this.subjectRepository = new SubjectRepository();
        }

        public IEnumerable<Subject> GetAllSubjects()
        {
            return subjectRepository.GetSubjects();
        }

        [HttpPost]
        public Subject SaveNew(Subject subject)
        {
            return subjectRepository.SaveSubject(subject);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            return execute(() => { subjectRepository.Delete(id); });
        }
    }
}
