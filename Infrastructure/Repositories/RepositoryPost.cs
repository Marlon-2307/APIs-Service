using Amazon.Runtime.Internal.Util;
using Athenas.EjemploTemplateApi.Domain.Entities;
using Athenas.EjemploTemplateApi.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Athenas.EjemploTemplateApi.Domain.Exceptions;

namespace Athenas.EjemploTemplateApi.Infrastructure.Repositories
{
    public class RepositoryPost(ApplicationDbContext context, ILogger<RepositoryPost> logger) : IRepositoryPost
    {
        public async Task<int> SavePost(Post post)
        {
            try
            {
                var rest = context.Set<Post>().Add(post);
                await context.SaveChangesAsync();
                return post.Id;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al insertar en la base de datos {Message}", ex.Message);
                throw new DataBaseException(ex.Message);
            }
        }

        public async Task<int> Update(int id, string content, string title)
        {
            try
            {
                var post = await context.Posts.Where(x => x.Id == id).FirstAsync();
                post.Title = title;
                post.Content = content;
                var result = context.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar en la base de datos {Message}", ex.Message);
                throw new DataBaseException(ex.Message);
            }
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                var post = await context.Posts.Where(x => x.Id == id).FirstAsync();
                context.Remove(post);
                var result = context.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al eliminar en la base de datos {Message}", ex.Message);
                throw new DataBaseException(ex.Message);
            }
        }

        public async Task<List<Post>> GetList(int page, int pageSize)
        {
            try
            {
                var listPost = await context.Posts.
                                                   OrderBy(x => x.Id)
                                                  .Include(p => p.Categories)
                                                  .Include(p => p.Comments) 
                                                  .Skip((page - 1) * pageSize)
                                                  .Take(pageSize)
                                                  .ToListAsync();
                return listPost;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al consultar en la base de datos {Message}", ex.Message);
                throw new DataBaseException(ex.Message);
            }
        }
    }
}
