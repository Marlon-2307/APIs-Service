using AutoMapper;
using Athenas.EjemploTemplateApi.Application.DataTransferObjects;
using Athenas.EjemploTemplateApi.Domain.Entities;
using System.Linq;

namespace Athenas.EjemploTemplateApi.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<PostDto, Post>()
                       .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.Select(c => new Comment { Content = c }).ToList()))
                       .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories.Select(c => new Category { Name = c }).ToList()));

            CreateMap<Post, PostResponseDto>()
                     .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                     .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories.Select(c => c.Name).ToList()))
                     .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.Select(c => c.Content).ToList()));
        }
    }
}
