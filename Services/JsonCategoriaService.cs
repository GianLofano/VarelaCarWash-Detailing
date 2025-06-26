using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace VarelaCarWash.Services
{
    public static class JsonCategoriaService
    {
        private static readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "categorias.json");

        public static List<string> Leer()
        {
            if (!File.Exists(filePath)) return new List<string>();
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
        }

        public static void Agregar(string categoria)
        {
            var categorias = Leer();
            if (!categorias.Contains(categoria))
            {
                categorias.Add(categoria);
                var json = JsonSerializer.Serialize(categorias, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
        }
    }
}
