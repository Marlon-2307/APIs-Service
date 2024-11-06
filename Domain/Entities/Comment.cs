using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athenas.EjemploTemplateApi.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }  
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Post Post { get; set; }
    }
}
