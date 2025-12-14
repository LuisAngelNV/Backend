using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventariosApi.DTOs;
using InventariosApi.Models;

namespace InventariosApi.Services
{
    public interface IClientesService
    {
        public List<Cliente> ObtenerTodos();
        public Cliente? ObtenerPorId(int id);
        public Cliente Crear(ClienteCreateDto dto);
        public Cliente? Actualizar(int id, ClienteUpdateDto dto);
        bool Eliminar(int id);
    }
}