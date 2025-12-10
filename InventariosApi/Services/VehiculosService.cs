using InventariosApi.Models;
using InventariosApi.Dtos;
using InventariosApi.Repositories;

namespace InventariosApi.Services
{
    public class VehiculosService
    {
        private readonly VehiculosRepository _repo;

        public VehiculosService(VehiculosRepository repo)
        {
            _repo = repo;
        }

        public List<Vehiculo> ObtenerTodos()
            => _repo.GetAll();

        public Vehiculo? ObtenerPorId(int id)
            => _repo.GetById(id);

        public Vehiculo Crear(VehiculoCreateDto dto)
        {
            var vehiculo = new Vehiculo
            {
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                Anio = dto.Anio,
                Color = dto.Color
            };

            return _repo.Add(vehiculo);
        }

        public Vehiculo? Actualizar(int id, VehiculoUpdateDto dto)
        {
            var vehiculo = _repo.GetById(id);
            if (vehiculo == null) return null;

            vehiculo.Marca = dto.Marca;
            vehiculo.Modelo = dto.Modelo;
            vehiculo.Anio = dto.Anio;
            vehiculo.Color = dto.Color;

            _repo.Update(vehiculo);

            return vehiculo;
        }

        public bool Eliminar(int id)
        {
            var vehiculo = _repo.GetById(id);
            if (vehiculo == null) return false;

            _repo.Delete(id);
            return true;
        }
    }
}
