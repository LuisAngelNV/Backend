using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventariosApi.DTOs;
using InventariosApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventariosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClientesService _clientes;

        public ClientesController(IClientesService clientes)
        {
            _clientes = clientes;
        }

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            return Ok(_clientes.ObtenerTodos());
        }

        [HttpGet("{id:int}")]
        public IActionResult ObtenerPorId(int id)
        {
            var cliente = _clientes.ObtenerPorId(id);
            if (cliente == null)
                return NotFound("Cliente no localizado");

            return Ok(cliente);
        }
        [HttpPost]
        public IActionResult Crear(ClienteCreateDto dto)
        {
            var nuevo = _clientes.Crear(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevo.Id }, nuevo);
        }

        [HttpPut("{id:int}")]
        public IActionResult Actualizar(int id, ClienteUpdateDto dto)
        {
            var actualizado = _clientes.Actualizar(id, dto);
            if (actualizado == null)
                return NotFound("Cliente no encontrado");

            return Ok(actualizado);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Eliminar(int id)
        {
            var borrado = _clientes.Eliminar(id);
            if (!borrado)
                return NotFound("Cliente no encontrado");

            return NoContent();
        }
    }
}