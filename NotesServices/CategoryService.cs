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
            IEnumerable<Category> categories = categoryRepository.GetCategories(accountID);

            List<CategoryNode> rootNodes = new List<CategoryNode>();
           
            IEnumerable<Category> roots = categories.Where(p => p.ParentCategoryID == 0);

            foreach (Category child in roots)
            {
                CategoryNode node = new CategoryNode();
                node.Value = child;

                rootNodes.Add(node);

                attachChildren(node, categories);
            }          

            return rootNodes;
        }

        private void attachChildren(CategoryNode root, IEnumerable<Category> categories)
        {
            IEnumerable<Category> children = categories.Where(p => p.ParentCategoryID == root.Value.CategoryID);
                
            foreach(Category child in children) {
                    CategoryNode node = new CategoryNode();
                    node.Value = child;

                    root.Children.Add(node);

                    attachChildren(node, categories);
            }                        
        }
    }
}
