using Athenas.EjemploTemplateApi.Application.DataTransferObjects;
using Athenas.EjemploTemplateApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athenas.EjemploTemplateApi.Application.Contracts
{
    public interface IPostsUseCase
    {
        Task<List<PostResponseDto>> GetPosts(int page, int pageSize);

        Task<bool> CreatePost(PostDto post);

        Task<bool> UpdatePost(PostEditDto postEditDto);

        Task<bool> DeletePost(int id);
    }
}