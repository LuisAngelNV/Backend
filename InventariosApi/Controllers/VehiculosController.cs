using InventariosApi.Dtos;
using InventariosApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventariosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiculosController : ControllerBase
    {
        private readonly VehiculosService _service;

        public VehiculosController(VehiculosService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            return Ok(_service.ObtenerTodos());
        }

        [HttpGet("{id:int}")]
        public IActionResult ObtenerPorId(int id)
        {
            var vehiculo = _service.ObtenerPorId(id);
            if (vehiculo == null)
                return NotFound("Vehículo no encontrado");

            return Ok(vehiculo);
        }

        [HttpPost]
        public IActionResult Crear(VehiculoCreateDto dto)
        {
            var nuevo = _service.Crear(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevo.Id }, nuevo);
        }

        [HttpPut("{id:int}")]
        public IActionResult Actualizar(int id, VehiculoUpdateDto dto)
        {
            var actualizado = _service.Actualizar(id, dto);
            if (actualizado == null)
                return NotFound("Vehículo no encontrado");

            return Ok(actualizado);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Eliminar(int id)
        {
            var borrado = _service.Eliminar(id);
            if (!borrado)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Cliente no encontrado",
                    Detail = $"No existe un cliente con id {id}",
                    Status = StatusCodes.Status404NotFound
                });
            }
            return NoContent();
        }
    }
}
