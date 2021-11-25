using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplicationApi.Data;
using WebApplicationApi.Models;
using WebApplicationApi.Repository;

namespace WebApplicationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {

        private readonly IArticleRepository _repository;
        private readonly ApplicationContext _context;
        public ArticleController(IArticleRepository repository, ApplicationContext context)
        {
            _repository = repository;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetArticles()
        {
            var articles = _repository.GetAllArticles().ToArray();

            var articleviewmodel = articles.Select(p => new ArticleViewModel()
            {

                Id = p.ArticleId,
                Name = p.Name,
                Description = p.Description,
                Categories = p.ArticleCategory.Select(c => c.Category).ToList()
            });

            return Ok(articleviewmodel);
        }
        [HttpGet("{id}", Name = "GetArticleById")]
        public IActionResult GetArticleById(int id)
        {
            var article = _repository.GetArticleById(id);
            if(article is null)
            {
                return NotFound("Ne postoji");
            }
            var articleviewModel = new ArticleViewModel
            {
                Id = article.ArticleId,
                Name = article.Name,
                Description = article.Description,
                Categories = article.ArticleCategory.Select(c => c.Category).ToList()
            };
            return Ok(articleviewModel);
        }
        public Article CreateArticle(ArticleViewModel articleViewModel)
        {
            var article = new Article
            {
                ArticleId = articleViewModel.Id,
                Name = articleViewModel.Name,
                Description = articleViewModel.Description,
             };
            //dodavanje clanka u bazu
            _repository.CreateArticle(article);
            
            //dodavanje kategorije, prolazak kroz sve kategorije koje je prosledio korisnik
            //putem id dodajemo zeljeni broj kategorija
            foreach (var item in articleViewModel.Categories)
            {
                var articleCategory1 = new ArticleCategory[]
                {
                    new ArticleCategory{Article = article,Category = _context.Category.Where(c=>c.CategoryId==item.CategoryId).FirstOrDefault()}
                };
                foreach (var ac in articleCategory1)
                {
                    _context.ArticleCategory.Add(ac);
                }                
            }
             _context.SaveChanges();

            return article;
        }
        [HttpPut("{id}")]
        public IActionResult UpdateArticle(int id,ArticleViewModel articleViewModel)
        {
           var article= _repository.GetArticleById(id);
            if(article is null)
            {
                return NotFound("ne postoji");
            }
            article.Name = articleViewModel.Name;
            article.Description = articleViewModel.Description;
            _context.Update(article);

            //brisanje postojecih kategorija
           _context.RemoveRange(article.ArticleCategory);
            ///dodavanje izmenjenih kategorija
            foreach (var item in articleViewModel.Categories)
            {
                var articleCategory1 = new ArticleCategory[]
                {
                    new ArticleCategory{Article = article,Category = _context.Category.Where(c=>c.CategoryId==item.CategoryId).FirstOrDefault()}
                };
                foreach (var ac in articleCategory1)
                {
                    _context.ArticleCategory.Add(ac);
                }
            }
            _context.SaveChanges();
           
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteArticleById(int id)
        {
            var article = _repository.GetArticleById(id);
            _repository.DeleteArticle(article);
            return NoContent();

        }
       
    }

}