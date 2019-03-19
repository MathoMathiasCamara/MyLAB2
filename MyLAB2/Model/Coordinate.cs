using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLAB2.Model
{
    public class Coordinate
    {
        public double _CoordinateX { get; set; }
        public double _CoordinateY { get; set; }

        public Coordinate()
        {
            _CoordinateX = 0.0;
            _CoordinateY = 0.0;
        }
    }
}
