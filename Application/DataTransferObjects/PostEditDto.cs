using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athenas.EjemploTemplateApi.Application.DataTransferObjects
{
    public class PostEditDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
    public class PostEditDtoValidator : AbstractValidator<PostEditDto>
    {
        public PostEditDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0).WithMessage("Id es requerido");
            RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("Titulo es requerido");
            RuleFor(x => x.Content).NotNull().NotEmpty().WithMessage("Content es requerido");
        }
    }
}
