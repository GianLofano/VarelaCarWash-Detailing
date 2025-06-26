using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VarelaCarWash.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public ICollection<OrderItem> Items { get; set; }

        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "Pendiente"; // o Enviado, Cancelado, etc.
    }
}
