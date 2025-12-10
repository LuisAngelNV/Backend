namespace InventariosApi.Dtos
{
    public class FrutaUpdateDto
    {
        public string Nombre { get; set; }
        public string Color { get; set; }
        public decimal PesoGramos { get; set; }
        public DateTime FechaCaducidad { get; set; }
    }
}
