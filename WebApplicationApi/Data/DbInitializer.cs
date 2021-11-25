using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationApi.Models;

namespace WebApplicationApi.Data
{
    public class DbInitializer
    {
        public static void InitializeDatabase(ApplicationContext context)
        {
            context.Database.EnsureCreated();

            if (context.Category.Any())
            {
                return;
            }
            var categories = new Category[]
           {
                    new Category {  Name = "Informativan",ArticleCategory=new List<ArticleCategory>() },
                    new Category {  Name = "Interpretativan",ArticleCategory=new List<ArticleCategory>() },
           };
            foreach (var c in categories)
            {
                context.Category.Add(c);
            }
            context.SaveChanges();

            if (context.Article.Any())
            {
                return;
            }

               var articles =new Article[]
               {
                    new Article {  Name = "Prvi clanak", Description="Ovo je prvi clanak koji je napravljen",ArticleCategory=new List<ArticleCategory>() },
                    new Article {  Name = "Drugi clanak", Description="Ovo je drugi clanak koji je napravljen",ArticleCategory=new List<ArticleCategory>()  },
                   

               };
           
            foreach (var p in articles)
            {
                context.Article.Add(p);
            }
            
            context.SaveChanges();

            var articlecategory = new ArticleCategory[]
            {
                    new ArticleCategory{Article=articles[0],Category=categories[1] },
                    new ArticleCategory{Article=articles[1], Category=categories[0]},
               

            };
            foreach (var a in articlecategory)
            {
                context.ArticleCategory.Add(a);
               
            }
            articles[0].ArticleCategory.Add(articlecategory[0]);
            articles[1].ArticleCategory.Add(articlecategory[1]);
           
            context.SaveChanges();
        }

        
         


    }

}

