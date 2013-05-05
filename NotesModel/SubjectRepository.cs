using notes.Models;
using notes.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;

namespace notes.Model.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        public IEnumerable<Subject> GetSubjects()
        {
            using (var conn = new SqlConnection(Connection.Value))
            {
                conn.Open();
                return conn.Query<Subject>("select SubjectID, Title from subject");
            }
        }
        
        public Subject SaveSubject(Subject subject)
        {
            using (var conn = new SqlConnection(Connection.Value))
            {
                conn.Open();
                subject.SubjectID = conn.Execute("INSERT subject (Title) VALUES (@title); SELECT SCOPE_IDENTITY();", new {title = subject.Title });
                return subject;
            }
        }

        public void Delete(int subjectID)
        {
            using (var conn = new SqlConnection(Connection.Value))
            {
                conn.Open();
                conn.Execute("delete from subject where subjectID=@subjectID;", new { subjectID });              
            }
        }
    }
}