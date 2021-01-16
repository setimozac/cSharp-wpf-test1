using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class InvalidDataException : Exception
    {
        public InvalidDataException(string msg)
            :base(msg)
        {

        }
    }
}
