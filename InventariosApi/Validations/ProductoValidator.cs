using FluentValidation;
using InventariosApi.Models;

namespace InventariosApi.Validations;

public class ProductoValidator : AbstractValidator<Producto>
{
    public ProductoValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.");

        RuleFor(x => x.Precio)

            .GreaterThan(0).WithMessage("El precio debe ser mayor a 0.")
            .LessThan(10000).WithMessage("El precio debe ser menor a 10000.");
    }
}
