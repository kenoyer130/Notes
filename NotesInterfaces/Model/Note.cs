using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace notes.Models
{  
    public class Note
    {
        public int NoteID { get; set; }
        public string Title { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public Bucket Bucket { get; set; }
    }
}