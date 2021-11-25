using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationApi.Data;
using WebApplicationApi.Models;

namespace WebApplicationApi.Repository
{
    public class SqlArticleRepository : IArticleRepository
    {
        private readonly ApplicationContext _context;
        public SqlArticleRepository(ApplicationContext context)
        {
            _context = context;
        }
        public Article CreateArticle(Article article)
        {    
            _context.Article.Add(article);
            
            _context.SaveChanges();
        
            return article;
        }

        public void DeleteArticle(Article article)
        {
            _context.Remove(article);
            _context.SaveChanges();
        }

        public Article EditArticle(Article article)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Article> GetAllArticles()
        {
            var article = _context.Article.Include(x => x.ArticleCategory).ThenInclude(y => y.Category).AsEnumerable();
 
            return article;
        }
        public Article GetArticleById(int Id)
        {
            return _context.Article.Include(x => x.ArticleCategory).ThenInclude(y => y.Category).SingleOrDefault(p=>p.ArticleId==Id);
        }
      
    }
}
