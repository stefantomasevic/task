using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationApi.Models
{
    public class Article
    {
        public int ArticleId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [JsonIgnore]
        public virtual ICollection<ArticleCategory> ArticleCategory { get; set; }
    }
}
