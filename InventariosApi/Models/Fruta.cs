namespace InventariosApi.Models
{
    public class Fruta
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public decimal PesoGramos { get; set; }  // peso en gramos
        public DateTime FechaCaducidad { get; set; }
    }
}


