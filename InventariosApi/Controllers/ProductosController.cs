using InventariosApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventariosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private static List<Producto> productos = new()
        {
            new Producto { Id = 1, Nombre = "Laptop", Precio = 9500 },
            new Producto { Id = 2, Nombre = "Mouse", Precio = 250 },
            new Producto { Id = 3, Nombre = "Teclado", Precio = 600 }
        };

        // GET api/productos
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            return Ok(productos);
        }

        // GET api/productos/3
        [HttpGet("{id:int}")]
        public IActionResult ObtenerPorId(int id)
        {
            var prod = productos.FirstOrDefault(x => x.Id == id);
            if (prod == null)
                return NotFound("Producto no encontrado");

            return Ok(prod);
        }

        // POST api/productos
        [HttpPost]
        public IActionResult Crear(Producto producto)
        {
            // Si FluentValidation falla → automáticamente regresa BadRequest
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            producto.Id = productos.Max(x => x.Id) + 1;
            productos.Add(producto);

            return CreatedAtAction(nameof(ObtenerPorId), new { id = producto.Id }, producto);
        }

        // PUT api/productos/2
        [HttpPut("{id:int}")]
        public IActionResult Actualizar(int id, Producto datos)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var prod = productos.FirstOrDefault(x => x.Id == id);
            if (prod == null)
                return NotFound("Producto no encontrado");

            prod.Nombre = datos.Nombre;
            prod.Precio = datos.Precio;

            return Ok(prod);
        }

        // DELETE api/productos/1
        [HttpDelete("{id:int}")]
        public IActionResult Eliminar(int id)
        {
            var prod = productos.FirstOrDefault(x => x.Id == id);
            if (prod == null)
                return NotFound("Producto no encontrado");

            productos.Remove(prod);

            return NoContent();
        }
    }
}
