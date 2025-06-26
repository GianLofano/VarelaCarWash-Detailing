namespace VarelaCarWash.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; } // Descripci�n del servicio
        public decimal Precio { get; set; }
        public string? ImagenPath { get; set; } // Ruta de la imagen
    }
}
