using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athenas.EjemploTemplateApi.Application.DataTransferObjects
{
    public class GetPostPaginationDto
    {
        public int page { get; set; }
        public int pageSize { get; set; }
    }
    public class GetPostPaginationDtoValidator : AbstractValidator<GetPostPaginationDto>
    {
        public GetPostPaginationDtoValidator()
        {
            RuleFor(x => x.page).GreaterThanOrEqualTo(0).WithMessage("page es requerido");
            RuleFor(x => x.pageSize).GreaterThanOrEqualTo(0).WithMessage("pageSize es requerido");
        }
    }
}
