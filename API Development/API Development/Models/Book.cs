using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Development.Models
{
    public class Book
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public byte[] CoverImage { get; set; }
        public decimal Price { get; set; }
    }
}
