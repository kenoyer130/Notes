using notes.Models;
using NotesInterfaces.Repository;
using NotesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesServices
{
    public class CategoryService
    {
        public IEnumerable<CategoryNode> GetCategoryTree(int accountID)
        {
            ICategoryRepository categoryRepository = new CategoryRepository();
            NotesRepository notesRepository = new NotesRepository();

            IEnumerable<Category> categories = categoryRepository.GetCategories(accountID);
            IEnumerable<Note> notes = notesRepository.GetCategoryNotes(accountID);

            List<CategoryNode> rootNodes = new List<CategoryNode>();
           
            IEnumerable<Category> roots = categories.Where(p => p.ParentCategoryID == 0);

            foreach (Category child in roots)
            {
                CategoryNode node = new CategoryNode();
                node.Value = child;

                node.Notes = notes.Where(p => p.CategoryID == child.CategoryID).ToList();

                rootNodes.Add(node);

                attachChildren(node, categories, notes);
            }          

            return rootNodes;
        }

        private void attachChildren(CategoryNode root, IEnumerable<Category> categories, IEnumerable<Note> notes)
        {
            IEnumerable<Category> children = categories.Where(p => p.ParentCategoryID == root.Value.CategoryID);
                
            foreach(Category child in children) {
                    CategoryNode node = new CategoryNode();
                    node.Value = child;

                    node.Notes = notes.Where(p => p.CategoryID == child.CategoryID).ToList();

                    root.Children.Add(node);

                    attachChildren(node, categories, notes);
            }                        
        }
    }
}
