﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_CommerceSystem.Models
{
    public class Product
    {
        [Key]
        public int PID { get; set; }

        [Required]
        public string ProductName { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock {  get; set; }

        public decimal OverallRating { get; set; }

        [ForeignKey("supplier")]
        public int SupplierId { get; set; }

        [JsonIgnore]
        public Supplier supplier { get; set; }

        [ForeignKey("category")]
        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category category { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderProducts> OrderProducts { get; set; }

        [JsonIgnore]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
