using notes.Model.Repository;
using notes.Models;
using NotesInterfaces.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace NotesModel
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public IEnumerable<Category> GetCategories(int accountID)
        {
           return Execute<IEnumerable<Category>>(()=>{
               return conn.Query<Category>
                    ("select CategoryID, Title, AccountID, ParentCategoryID from Category where accountID=@accountID ORDER BY CategoryID;",
                    new { accountID });
           });
        }

        public Category UpsertCategory(Category category)
        {           
           if(category.CategoryID == 0){

               return Execute<Category>(() =>
               {
                   category.CategoryID = 
                       conn.Execute(@"INSERT category (Title, AccountID, ParentCategoryID) 
                                      VALUES (@title, @accountID, @parentCategoryID); SELECT SCOPE_IDENTITY();",
                                      new { title = category.Title, parentCategoryID = category.ParentCategoryID, accountID = category.AccountID });
                   return category;
               });
           }else{
               conn.Execute(@"UPDATE category  set Title=@title, AccountID=@accountID, ParentCategoryID=@parentCategoryID 
                               WHERE categoryID=@categoryID",
                                new { category.CategoryID, category.Title, category.ParentCategoryID, category.AccountID });
               return category;
           }
        }

        public void Delete(int categoryID)
        {
            Execute(() =>
            {
                conn.Execute("delete from category where categoryID=@categoryID;", new { categoryID });
            });
        }
    }
}
