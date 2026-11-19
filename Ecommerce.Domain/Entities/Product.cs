using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "الرجاء ادخال اسم المنتج")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = " الرجاء ادخال السعر ")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
