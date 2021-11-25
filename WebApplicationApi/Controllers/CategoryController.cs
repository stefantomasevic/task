using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationApi.Data;
using WebApplicationApi.Models;
using WebApplicationApi.Repository;

namespace WebApplicationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repository;
        private readonly ApplicationContext _context;
        public CategoryController(ICategoryRepository repository, ApplicationContext context)
        {
            _repository = repository;
            _context = context;
        }
        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _repository.GetAllCategories().ToArray();

            var categoryviewmodel = categories.Select(p => new CategoryViewModel()
            {

                Id = p.CategoryId,
                Name = p.Name,
                Articles = p.ArticleCategory.Select(c => c.Article).ToList()
            });
            return Ok(categoryviewmodel);
        }
        [HttpGet("{id}", Name = "GetCategoryById")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _repository.GetCategoryById(id);
            if (category is null)
            {
                return NotFound("Ne postoji");
            }
            var categoryviewmodel = new CategoryViewModel
            {
                Id = category.CategoryId,
                Name = category.Name,
                Articles = category.ArticleCategory.Select(c => c.Article).ToList()
            };

            return Ok(categoryviewmodel);
        }
        public Category CreateArticle(CategoryViewModel categoryViewModel)
        {
            var category = new Category
            {
                CategoryId = categoryViewModel.Id,
                Name = categoryViewModel.Name,
            };
           
            _repository.CreateCategory(category);

           //dodavanje zeljenog broja artikala
            foreach (var item in categoryViewModel.Articles)
            {
                var articleCategory = new ArticleCategory[]
                {
                    new ArticleCategory{Article = _context.Article.Where(c=>c.ArticleId==item.ArticleId).FirstOrDefault(),Category =category}
                };
                foreach (var ac in articleCategory)
                {
                    _context.ArticleCategory.Add(ac);
                }
            }
           
            _context.SaveChanges();

            return category;
        }
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, CategoryViewModel categoryViewModel)
        {
            var category = _repository.GetCategoryById(id);
            if (category is null)
            {
                return NotFound("ne postoji");
            }
            category.Name = categoryViewModel.Name;
      
            _context.Update(category);

            //brisanje postojecih clanaka iz kategorije
            _context.RemoveRange(category.ArticleCategory);
            ///dodavanje clanaka
            foreach (var item in categoryViewModel.Articles)
            {
                var articleCategory = new ArticleCategory[]
                {
                    new ArticleCategory{Article =_context.Article.Where(c=>c.ArticleId==item.ArticleId).FirstOrDefault(),Category = category}
                };
                foreach (var ac in articleCategory)
                {
                    _context.ArticleCategory.Add(ac);
                }
            }
            _context.SaveChanges();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategoryById(int id)
        {
            var category = _repository.GetCategoryById(id);
            _repository.DeleteCategory(category);
            return NoContent();
        }
    }
}