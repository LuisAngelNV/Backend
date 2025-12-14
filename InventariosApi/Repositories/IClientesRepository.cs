using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventariosApi.Models;

namespace InventariosApi.Repositories
{
    public interface IClientesRepository
    {
        public List<Cliente> GetAll();
        public Cliente? GetById(int id);
        public Cliente Add(Cliente cliente);
        public void Update(Cliente cliente);
        public void Delete(int id);
    }
}