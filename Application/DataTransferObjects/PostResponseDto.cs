using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athenas.EjemploTemplateApi.Application.DataTransferObjects
{
    public class PostResponseDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Categories { get; set; }
        public List<string> Comments { get; set; }
    }
}
