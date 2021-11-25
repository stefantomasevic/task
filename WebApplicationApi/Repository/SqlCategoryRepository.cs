using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationApi.Data;
using WebApplicationApi.Models;

namespace WebApplicationApi.Repository
{
    public class SqlCategoryRepository : ICategoryRepository
    {
        private readonly ApplicationContext _context;
        public SqlCategoryRepository(ApplicationContext context)
        {
            _context = context;
        }
        public Category CreateCategory(Category category)
        {
            _context.Category.Add(category);

            _context.SaveChanges();
            return category;
        }
        public void DeleteCategory(Category category)
        {
            _context.Remove(category);
            _context.SaveChanges();
        }
        public Category EditCategory(Category category)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Category> GetAllCategories()
        {
            var category = _context.Category.Include(x => x.ArticleCategory).ThenInclude(y => y.Article).AsEnumerable();
            
            return category;
        }
        public Category GetCategoryById(int Id)
        {
            return _context.Category.Include(x => x.ArticleCategory).ThenInclude(y => y.Article).SingleOrDefault(p => p.CategoryId == Id);
        }
    }
}
