using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlDemo
{
    public class Square : Shape
    {
        public int iSize;
        public Square() { }

        public Square(int size)
        {
            iSize = size;
            edges = 4;
            area = size * size;
        }

        public override string ToString()
        {
            return "SQuare, iSize=" + iSize;
        }
    }
}
