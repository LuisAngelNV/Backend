using InventariosApi.DTOs;
using InventariosApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventariosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ClientesService _clientes;

        public ClientesController(ClientesService clientes)
        {
            _clientes = clientes;
        }

        // GET /api/clientes
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var clientes = _clientes.ObtenerTodos();
            return Ok(clientes);
        }

        // GET /api/clientes/{id}
        [HttpGet("{id:int}")]
        public IActionResult ObtenerPorId(int id)
        {
            var cliente = _clientes.ObtenerPorId(id);

            if (cliente == null)
            {
                return NotFound(Problem(
                    title: "Cliente no encontrado",
                    detail: $"No existe un cliente con el id {id}",
                    statusCode: StatusCodes.Status404NotFound
                ));
            }

            return Ok(cliente);
        }

        // POST /api/clientes
        [HttpPost]
        public IActionResult Crear(ClienteCreateDto dto)
        {
            var nuevo = _clientes.Crear(dto);

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { id = nuevo.Id },
                nuevo
            );
        }

        // PUT /api/clientes/{id}
        [HttpPut("{id:int}")]
        public IActionResult Actualizar(int id, ClienteUpdateDto dto)
        {
            var actualizado = _clientes.Actualizar(id, dto);

            if (actualizado == null)
            {
                return NotFound(Problem(
                    title: "No se pudo actualizar",
                    detail: $"El cliente con id {id} no existe",
                    statusCode: StatusCodes.Status404NotFound
                ));
            }

            return Ok(actualizado);
        }

        // DELETE /api/clientes/{id}
        [HttpDelete("{id:int}")]
        public IActionResult Eliminar(int id)
        {
            var eliminado = _clientes.Eliminar(id);

            if (!eliminado)
            {
                return NotFound(Problem(
                    title: "No se pudo eliminar",
                    detail: $"El cliente con id {id} no existe",
                    statusCode: StatusCodes.Status404NotFound
                ));
            }

            return NoContent(); // 204
        }
    }
}
