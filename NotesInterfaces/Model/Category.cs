using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace notes.Models
{
    public class Category
    {        
        public int CategoryID { get; set; }
        public string Title { get; set; }
        public int ParentCategoryID { get; set; }
    }
}
