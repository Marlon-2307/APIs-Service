using Athenas.EjemploTemplateApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athenas.EjemploTemplateApi.Domain.Interfaces
{
    public interface IRepositoryPost
    {
        Task<int> SavePost(Post post);
        Task<int> Update(int id, string content, string title);
        Task<int> Delete(int id);
        Task<List<Post>> GetList(int page, int pageSize);
    }
}
