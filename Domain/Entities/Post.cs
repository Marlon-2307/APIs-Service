using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athenas.EjemploTemplateApi.Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
