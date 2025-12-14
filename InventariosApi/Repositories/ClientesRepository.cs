using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventariosApi.Models;

namespace InventariosApi.Repositories
{
    public class ClientesRepository
    {
        private readonly List<Cliente> _clientes = new()
        {
            new Cliente { Id = 1, Nombre = "Cliente 1" , Apellido = "Apellido 1", Email = "o5H4o@example.com", EsActivo = true , FechaRegistro = DateTime.Now },
            new Cliente { Id = 2, Nombre = "Cliente 2" , Apellido = "Apellido 2", Email = "o5H4o@example.com", EsActivo = true , FechaRegistro = DateTime.Now },
            new Cliente { Id = 3, Nombre = "Cliente 3" , Apellido = "Apellido 3", Email = "o5H4o@example.com", EsActivo = true , FechaRegistro = DateTime.Now },
            new Cliente { Id = 4, Nombre = "Cliente 4" , Apellido = "Apellido 4", Email = "o5H4o@example.com", EsActivo = true , FechaRegistro = DateTime.Now },
        };

        public List<Cliente> GetAll() => _clientes;
        public Cliente? GetById(int id) =>
            _clientes.FirstOrDefault(x => x.Id == id);

        public Cliente Add(Cliente cliente)
        {
            cliente.Id = _clientes.Max(x => x.Id) + 1;
            _clientes.Add(cliente);
            return cliente;
        }

        public void Update(Cliente cliente)
        {
            var index = _clientes.FindIndex(x => x.Id == cliente.Id);
            _clientes[index] = cliente;
        }

        public void Delete(int id)
        {
            var index = _clientes.FindIndex(x => x.Id == id);
            _clientes.RemoveAt(index);
        }
    }
}