using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Dal.Entities
{
    public class Categoria
    {
        public int? idCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public bool Activa { get; set; }
    }
}
