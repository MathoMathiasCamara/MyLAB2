using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLAB2.Model
{
    public class IsIsoscele:Triangle
    {
        //private bool _IsIsoscele;
       

        public bool CheckIsIsoscele
        {
            get
            {

                return (((base.LineAB == base.LineBC) && base.LineAB != base.LineCA) || ((base.LineBC == base.LineAB) && base.LineBC != base.LineCA) || ((base.LineCA == base.LineAB) && base.LineCA != base.LineBC));
            }
        }
    }
}
