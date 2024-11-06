using Athenas.EjemploTemplateApi.Application.Contracts;
using Athenas.EjemploTemplateApi.Application.DataTransferObjects;
using Athenas.EjemploTemplateApi.Application.UseCases;
using Athenas.EjemploTemplateApi.WebApi.Handler;
using Athenas.EjemploTemplateApi.WebApi.ModelsResponse;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Athenas.EjemploTemplateApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController(IPostsUseCase postsUseCase) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(CreatePostResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePost([FromBody] PostDto post)
        {
            PostValidator validationRules = new PostValidator();
            var validResult = validationRules.Validate(post);
            if(!validResult.IsValid) InvalidModel.Response(validResult);

            var response = await postsUseCase.CreatePost(post).ConfigureAwait(false);

            CreatePostResponse clientCreateResponse = new CreatePostResponse
            {
                Data = response,
                StatusCode = StatusCodes.Status200OK,
                Message = "Proceso exitoso."
            };

            return Ok(clientCreateResponse);
        }

        [HttpPut("UpdatePost")]
        [ProducesResponseType(typeof(CreatePostResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePost([FromBody] PostEditDto post)
        {
            PostEditDtoValidator validationRules = new PostEditDtoValidator();
            var validResult = validationRules.Validate(post);
            if (!validResult.IsValid) InvalidModel.Response(validResult);

            var response = await postsUseCase.UpdatePost(post).ConfigureAwait(false);

            CreatePostResponse clientCreateResponse = new CreatePostResponse
            {
                Data = response,
                StatusCode = StatusCodes.Status200OK,
                Message = "Proceso exitoso."
            };

            return Ok(clientCreateResponse);
        }

        [HttpDelete("DeletePost")]
        [ProducesResponseType(typeof(CreatePostResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePost([FromBody] int id)
        {
            var response = await postsUseCase.DeletePost(id).ConfigureAwait(false);

            CreatePostResponse clientCreateResponse = new CreatePostResponse
            {
                Data = response,
                StatusCode = StatusCodes.Status200OK,
                Message = "Proceso exitoso."
            };

            return Ok(clientCreateResponse);
        }

        [HttpPost("GetPosts")]
        [ProducesResponseType(typeof(GetPostsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPosts([FromBody] GetPostPaginationDto getPostPaginationDto)
        {
            GetPostPaginationDtoValidator validationRules = new GetPostPaginationDtoValidator();
            var validResult = validationRules.Validate(getPostPaginationDto);
            if (!validResult.IsValid) InvalidModel.Response(validResult);

            var response = await postsUseCase.GetPosts(getPostPaginationDto.page, getPostPaginationDto.pageSize).ConfigureAwait(false);

            GetPostsResponse clientCreateResponse = new GetPostsResponse
            {
                Data = response,
                StatusCode = StatusCodes.Status200OK,
                Message = "Proceso exitoso."
            };

            return Ok(clientCreateResponse);
        }
    }
}
