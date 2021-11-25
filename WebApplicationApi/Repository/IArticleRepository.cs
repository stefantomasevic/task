using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationApi.Models;

namespace WebApplicationApi.Repository
{
    public interface IArticleRepository
    {
        IEnumerable<Article> GetAllArticles();
        Article GetArticleById(int Id);
        Article CreateArticle(Article article);
        Article EditArticle(Article article);
        void DeleteArticle(Article article);
        
           

    }
}
