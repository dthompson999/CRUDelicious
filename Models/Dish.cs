using System;
using System.ComponentModel.DataAnnotations;

namespace CRUDelicious.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Chef { get; set; }

        [Range(1,5)]
        public int Tastiness { get; set; }

        [Range(1,10000)]
        public int Calories { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public virtual string DataImageUrlField { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}