using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XmlDemo
{
    [XmlInclude(typeof(Square))]
    [XmlInclude(typeof(Circle))]
    public abstract class Shape
    {
        public Shape() { }
        public double area;
        public int edges;
    }
}
