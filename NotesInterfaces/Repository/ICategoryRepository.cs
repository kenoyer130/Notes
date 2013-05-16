using notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesInterfaces.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories(int accountID);

        Category UpsertCategory(Category category);

        void Delete(int categoryID);
    }
}
