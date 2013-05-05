using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace notes.Model.Repository
{
    public static class Connection
    {
        public static string Value
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["NotesConnection"].ConnectionString;
            }
        }
    }
}
