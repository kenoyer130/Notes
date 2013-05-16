using notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace notes.Models
{
    public class CategoryNode
    {
        public CategoryNode()
        {
            Children = new List<CategoryNode>();
        }

        public Category Value { get; set; }
        public List<CategoryNode> Children { get; set; }
        public List<Note> Notes { get; set; }
    }
}
