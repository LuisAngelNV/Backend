using InventariosApi.Models;

namespace InventariosApi.Repositories
{
    public class VehiculosRepository
    {
        private readonly List<Vehiculo> _vehiculos = new()
        {
            new Vehiculo { Id = 1, Marca = "Toyota", Modelo = "Corolla", Anio = 2020, Color = "Rojo" },
            new Vehiculo { Id = 2, Marca = "Honda", Modelo = "Civic", Anio = 2019, Color = "Negro" }
        };

        public List<Vehiculo> GetAll() => _vehiculos;

        public Vehiculo? GetById(int id) =>
            _vehiculos.FirstOrDefault(v => v.Id == id);

        public Vehiculo Add(Vehiculo vehiculo)
        {
            vehiculo.Id = _vehiculos.Max(v => v.Id) + 1;
            _vehiculos.Add(vehiculo);
            return vehiculo;
        }

        public void Update(Vehiculo vehiculo)
        {
            var index = _vehiculos.FindIndex(v => v.Id == vehiculo.Id);
            _vehiculos[index] = vehiculo;
        }

        public void Delete(int id)
        {
            var vehiculo = _vehiculos.FirstOrDefault(v => v.Id == id);
            if (vehiculo != null)
                _vehiculos.Remove(vehiculo);
        }
    }
}
