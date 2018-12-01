using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public ApplicationUser Yazar { get; set; }
        public string Konu { get; set; }
        public string Resim { get; set; }
        public string Yazi { get; set; }
    }
}
