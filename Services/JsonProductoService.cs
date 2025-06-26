using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using VarelaCarWash.Models;

namespace VarelaCarWash.Services
{
    public static class JsonProductoService
    {
        private static readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "productos.json");

        public static List<Product> Leer()
        {
            if (!File.Exists(filePath)) return new List<Product>();
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        }

        public static void Guardar(List<Product> productos)
        {
            var json = JsonSerializer.Serialize(productos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public static void Agregar(Product producto)
        {
            var productos = Leer();
            producto.Id = productos.Any() ? productos.Max(p => p.Id) + 1 : 1;
            productos.Add(producto);
            Guardar(productos);
        }

        public static void Editar(Product producto)
        {
            var productos = Leer();
            var existente = productos.FirstOrDefault(p => p.Id == producto.Id);
            if (existente != null)
            {
                existente.Nombre = producto.Nombre;
                existente.Categoria = producto.Categoria;
                existente.Precio = producto.Precio;
                existente.Stock = producto.Stock;
                existente.ImagenPath = producto.ImagenPath; // Actualiza la imagen si corresponde
                existente.Descripcion = producto.Descripcion; // Actualiza la descripción
                Guardar(productos);
            }
        }

        public static void Eliminar(int id)
        {
            var productos = Leer().Where(p => p.Id != id).ToList();
            Guardar(productos);
        }

        public static Product? ObtenerPorId(int id)
        {
            return Leer().FirstOrDefault(p => p.Id == id);
        }
    }
}
