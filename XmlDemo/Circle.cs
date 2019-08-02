using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlDemo
{
    public class Circle : Shape
    {
        public int radius;
        public Circle() { }

        public Circle(int r)
        {
            radius = r;

            area = 3.14f * radius * radius;
        }

        public override string ToString()
        {
            return "Circle, radius=" + radius;
        }
    }
}
