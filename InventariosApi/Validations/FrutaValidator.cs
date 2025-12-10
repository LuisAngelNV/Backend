using FluentValidation;
using InventariosApi.Models;

namespace InventariosApi.Validations
{
    public class FrutaValidator : AbstractValidator<Fruta>
    {
        // Lista de colores como propiedad estática en el validador
        private static readonly string[] _coloresValidos = { "rojo", "amarillo", "verde", "morado", "naranja" };

        public FrutaValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MinimumLength(2).WithMessage("El nombre deberá ser mayor a 2 caracteres")
                .MaximumLength(50).WithMessage("El nombre puede ser mayor a 50 caracteres")
                .Matches("^[a-zA-Z]+$").WithMessage("El nombre solo puede contener letras");

            RuleFor(x => x.Color)
                .NotEmpty().WithMessage("El color es obligatorio.")
                .Must(c => _coloresValidos.Contains(c.ToLower()))
                .WithMessage($"Color inválido. Debe ser: {string.Join(", ", _coloresValidos)}.");

            RuleFor(x => x.PesoGramos)
                .InclusiveBetween(0, 5000).WithMessage("Se deberá de tener un peso entre los 0 y 5000 gramos");

            RuleFor(x => x.FechaCaducidad)
                .GreaterThan(DateTime.Now).WithMessage("La fecha deberá ser mayor a la fecha y hora actual");

        }
    }
}
