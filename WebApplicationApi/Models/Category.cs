using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationApi.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        
        public string Name { get; set; }
      
        [JsonIgnore]
        public virtual ICollection<ArticleCategory> ArticleCategory { get; set; }
    }
}
