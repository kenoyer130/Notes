using notes.Model.Repository;
using NotesInterfaces.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Text.RegularExpressions;

namespace NotesModel
{
    public class AccountGoogleRepository : IAccountRepository
    {
        Regex googleID = new Regex("id=(.*?)$");

        public int Login(string id)
        {
            string openID = googleID.Match(id).Groups[1].Value;

            int? accountID = 0;
            
            using (var conn = new SqlConnection(Connection.Value))
            {
                conn.Open();
                accountID = conn.Query<int>("select accountID from dbo.Account WHERE openID='@openID'", new { openID }).FirstOrDefault();

                if (accountID == null || accountID == 0)
                {
                    accountID = conn.Execute("insert into dbo.Account(OpenID) VALUES ('@openID'); SELECT SCOPE_IDENTITY();", new { openID });
                }
            }

            return accountID.Value;
        }
    }
}
