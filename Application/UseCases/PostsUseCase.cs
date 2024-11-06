using Athenas.EjemploTemplateApi.Application.Contracts;
using Athenas.EjemploTemplateApi.Application.DataTransferObjects;
using Athenas.EjemploTemplateApi.Domain.Entities;
using Athenas.EjemploTemplateApi.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athenas.EjemploTemplateApi.Application.UseCases
{
    public class PostsUseCase(IRepositoryPost repositoryPost, IMapper mapper) : IPostsUseCase
    {
        public async Task<bool> CreatePost(PostDto post)
        {
            if (post == null) throw new ArgumentNullException(nameof(post));
            var postEntity = mapper.Map<Post>(post);
            await repositoryPost.SavePost(postEntity).ConfigureAwait(false);
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            var result = await repositoryPost.Delete(id).ConfigureAwait(false);
            return result != 1 ? false : true;
        }

        public async Task<List<PostResponseDto>> GetPosts(int page, int pageSize)
        {
            var posts = await repositoryPost.GetList(page, pageSize).ConfigureAwait(false);
            var result = mapper.Map<List<PostResponseDto>>(posts);
            return result;
        }

        public async Task<bool> UpdatePost(PostEditDto postEditDto)
        {
            if(postEditDto == null) throw new ArgumentNullException(nameof(postEditDto));
            var result = await repositoryPost.Update(postEditDto.Id, postEditDto.Content, postEditDto.Title).ConfigureAwait(false);
            return result != 1 ? false : true;
        }
    }
}
