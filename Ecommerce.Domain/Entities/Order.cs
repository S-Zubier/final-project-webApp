using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        [Required] public string CustomerName { get; set; }
        [Required] public string Address { get; set; }
        [Required] public string Phone { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}
