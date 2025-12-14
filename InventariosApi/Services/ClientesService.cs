using InventariosApi.DTOs;
using InventariosApi.Models;
using InventariosApi.Repositories;
using Microsoft.AspNetCore.Routing.Constraints;

namespace InventariosApi.Services
{
    public class ClientesService : IClientesService
    {
        private readonly IClientesRepository _clientes;

        public ClientesService(IClientesRepository cliente)
        {
            _clientes = cliente;
        }

        public List<Cliente> ObtenerTodos() => _clientes.GetAll();

        public Cliente? ObtenerPorId(int id) => _clientes.GetById(id);
        public Cliente Crear(ClienteCreateDto dto)
        {
            var cliente = new Cliente
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Email = dto.Email
            };
            return _clientes.Add(cliente);
        }

        public Cliente? Actualizar(int id, ClienteUpdateDto dto)
        {
            var cliente = _clientes.GetById(id);
            if (cliente == null) return null;

            cliente.Nombre = dto.Nombre;
            cliente.Apellido = dto.Apellido;
            cliente.Email = dto.Email;

            _clientes.Update(cliente);

            return cliente;
        }


        public bool Eliminar(int id)
        {
            var cliente = _clientes.GetById(id);
            if (cliente == null) return false;

            _clientes.Delete(id);
            return true;
        }
    }
}