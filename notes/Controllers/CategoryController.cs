using notes.Model.Repository;
using notes.Models;
using NotesInterfaces.Repository;
using NotesModel;
using NotesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace notes.Controllers
{
    public class CategoryController : BaseNotesController
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly CategoryService categoryService;

        public CategoryController()
        {
            this.categoryRepository = new CategoryRepository();
            this.categoryService = new CategoryService();
        }

        [HttpGet]
        [ActionName("GetAllCategories")]
        public IEnumerable<CategoryNode> GetAllCategories()
        {
            return categoryService.GetCategoryTree(getAccountID());
        }

        [HttpPost]
        [ActionName("SaveNew")]
        public Category SaveNew(Category Category)
        {
            Category.AccountID = getAccountID();

            return categoryRepository.UpsertCategory(Category);
        }

        [HttpPost]
        public Category Update(Category Category)
        {
            return categoryRepository.UpsertCategory(Category);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            return execute(() => { categoryRepository.Delete(id); });
        }
    }
}
