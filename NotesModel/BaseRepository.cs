using notes.Model.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesModel
{
    public abstract class BaseRepository
    {
        protected SqlConnection conn;

        public R Execute<R>(Func<R> action)
        {
            using (conn = new SqlConnection(Connection.Value))
            {
                conn.Open();
                return action.Invoke();
            }
        }

        public void Execute(Action action)
        {
            using (conn = new SqlConnection(Connection.Value))
            {
                conn.Open();
                action.Invoke();
            }
        }
    }
}
