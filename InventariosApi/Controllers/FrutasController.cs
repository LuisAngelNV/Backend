using InventariosApi.Dtos;
using InventariosApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InventariosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FrutasController : ControllerBase
    {
        private static List<Fruta> frutas = new()
        {
            new Fruta { Id = 1, Nombre = "Manzana", Color = "Rojo", PesoGramos = 150, FechaCaducidad = DateTime.Today.AddDays(10) },
            new Fruta { Id = 2, Nombre = "Mandarina", Color = "Naranja", PesoGramos = 180, FechaCaducidad = DateTime.Today.AddDays(2) },
        };

        // GET /api/frutas → listar todas
        [HttpGet]
        public IActionResult GetFruta()
        {
            return Ok(frutas);
        }

        // GET /api/frutas/{id
        [HttpGet("{id:int}")]
        public IActionResult GetFrutaPorID(int id)
        {
            var fruta = frutas.FirstOrDefault(c => c.Id == id);
            if (fruta == null)
                return NotFound("No existe el valor solicitado");
            return Ok(fruta);
        }
        //POST /api/frutas → crear(201 Created; valida con FluentValidation)
        [HttpPost]
        public IActionResult CrearFruta(FrutaCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var fruta = new Fruta
            {
                Id = frutas.Max(x => x.Id) + 1,
                Nombre = dto.Nombre,
                Color = dto.Color,
                PesoGramos = dto.PesoGramos,
                FechaCaducidad = dto.FechaCaducidad
            };

            frutas.Add(fruta);

            return CreatedAtAction(nameof(GetFrutaPorID), new { id = fruta.Id }, fruta);
        }

        //        PUT /api/frutas/{id
        //} → actualizar(404 si no existe; validar; 409 si duplicado por Nombre+Fecha)
        [HttpPut("{id:int}")]
        public IActionResult ActualizarFruta(int id, FrutaUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var fruta = frutas.FirstOrDefault(x => x.Id == id);
            if (fruta == null)
                return NotFound("Fruta no encontrada");

            fruta.Nombre = dto.Nombre;
            fruta.Color = dto.Color;
            fruta.PesoGramos = dto.PesoGramos;
            fruta.FechaCaducidad = dto.FechaCaducidad;

            return Ok(fruta);
        }

        //        DELETE /api/frutas/{id} → eliminar(204 NoContent; 404 si no existe)
        [HttpDelete("{id:int}")]
        public IActionResult EliminarFruta(int id)
        {
            var fr = frutas.FirstOrDefault(x => x.Id == id);
            if (fr == null)
                return NotFound("Fruta no encontrada");

            frutas.Remove(fr);
            return NoContent();
        }
    }
}
