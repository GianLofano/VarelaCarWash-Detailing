namespace VarelaCarWash.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string? ImagenPath { get; set; } // Ruta de la imagen
        public string? Descripcion { get; set; } // Descripción del producto
    }
}