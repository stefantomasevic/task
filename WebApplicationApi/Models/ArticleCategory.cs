using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationApi.Models
{
    public class ArticleCategory
    {
        public int ArticleId { get; set; }
        //navigation property
        public virtual Article Article { get; set; } = new Article();

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = new Category();
    }
}
