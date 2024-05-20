using FluentValidation;
using PharmaApi.Models;
namespace PharmaApi.Dto.Validator
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductValidator()
        {
            RuleFor(dto => dto.nombre_producto).NotEmpty().MinimumLength(8);
            RuleFor(dto => dto.descripcion_producto).NotEmpty().MinimumLength(15);
            RuleFor(dto => dto.presentacion).NotEmpty();
            RuleFor(dto => dto.laboratorio).NotEmpty();
            RuleFor(dto => dto.categoria).NotEmpty();
            RuleFor(dto => dto.stock).NotEmpty();
            RuleFor(dto => dto.precio).NotEmpty();
        }
    }
}
