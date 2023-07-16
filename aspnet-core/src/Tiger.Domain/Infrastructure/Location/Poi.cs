using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Infrastructure.Location
{
    public class Poi
    {
        public string Tag { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
        public int? Distance { get; set; }
    }
}
