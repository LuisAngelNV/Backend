using FluentValidation;
using InventariosApi.Models;

namespace InventariosApi.Validations
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(x => x.Nombre)
             .NotEmpty().WithMessage("El nombre es obligatorio.")
             .MaximumLength(50).WithMessage("El nombre puede ser mayor a 50 caracteres.")
             .Matches("^[a-zA-Z]+$").WithMessage("El nombre solo puede contener letras.")
             .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.");

            RuleFor(x => x.Apellido)
             .NotEmpty().WithMessage("El apellido es obligatorio.")
             .MaximumLength(50).WithMessage("El apellido puede ser mayor a 50 caracteres.")
             .Matches("^[a-zA-Z]+$").WithMessage("El apellido solo puede contener letras.")
             .MinimumLength(3).WithMessage("El apellido debe tener al menos 3 caracteres.");

            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es obligatorio.")
            .EmailAddress().WithMessage("El email debe ser un correo electronico.");


        }
    }
}