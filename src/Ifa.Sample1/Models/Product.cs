using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ifa.Sample1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}