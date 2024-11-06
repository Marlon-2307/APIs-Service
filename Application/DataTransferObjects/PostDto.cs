using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athenas.EjemploTemplateApi.Application.DataTransferObjects;

public class PostDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public List<string> Categories { get; set; }
    public List<string> Comments { get; set; }
}

public class PostValidator : AbstractValidator<PostDto>
{
    public PostValidator()
    {
        RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("Titulo es requerido");
        RuleFor(x => x.Content).NotNull().NotEmpty().WithMessage("Content es requerido");
        RuleFor(post => post.Categories)
               .NotNull().WithMessage("Debe enviar al menos una categoria.")
               .NotEmpty().WithMessage("Debe enviar al menos una categoria.")
               .ForEach(category =>
                   category
                       .NotEmpty().WithMessage("Debe enviar al menos una categoria.")
               );
        RuleFor(post => post.Comments)
             .NotNull().WithMessage("Debe enviar al menos un comentario.")
             .NotEmpty().WithMessage("Debe enviar al menos un comentario.")
             .ForEach(comment =>
                 comment
                     .NotEmpty().WithMessage("Debe enviar al menos un comentario.")
                     .MaximumLength(300).WithMessage("El maximo de caracteres para los comentarios es de 300.")
             );
    }
}
