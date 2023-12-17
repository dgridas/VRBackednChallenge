using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HdrBoxReader.BO.Entities
{
    public class HdrBoxContent : BaseEntity
    {
        public string PoNumber { get; set; }
        public string Isbn { get; set; }
        public int Quantity { get; set; }

    }
}
