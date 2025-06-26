using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using VarelaCarWash.Models;

namespace VarelaCarWash.Services
{
    public static class JsonServicioService
    {
        private static readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "servicios.json");

        public static List<Service> Leer()
        {
            if (!File.Exists(filePath)) return new List<Service>();
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Service>>(json) ?? new List<Service>();
        }

        public static void Guardar(List<Service> servicios)
        {
            var json = JsonSerializer.Serialize(servicios, new JsonSerializerOptions { WriteIndented = true });
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            File.WriteAllText(filePath, json);
        }

        public static void Agregar(Service servicio)
        {
            var servicios = Leer();
            servicio.Id = servicios.Any() ? servicios.Max(s => s.Id) + 1 : 1;
            servicios.Add(servicio);
            Guardar(servicios);
        }

        public static void Editar(Service servicio)
        {
            var servicios = Leer();
            var existente = servicios.FirstOrDefault(s => s.Id == servicio.Id);
            if (existente != null)
            {
                existente.Nombre = servicio.Nombre;
                existente.Descripcion = servicio.Descripcion;
                existente.Precio = servicio.Precio;
                existente.ImagenPath = servicio.ImagenPath; // Actualiza la imagen si corresponde
                Guardar(servicios);
            }
        }

        public static void Eliminar(int id)
        {
            var servicios = Leer().Where(s => s.Id != id).ToList();
            Guardar(servicios);
        }

        public static Service? ObtenerPorId(int id)
        {
            return Leer().FirstOrDefault(s => s.Id == id);
        }
    }
}
