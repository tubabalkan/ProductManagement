using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; } // Unique kimlik
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        // Domain kuralları gerekiyorsa burada metotlar olabilir
    }
}

