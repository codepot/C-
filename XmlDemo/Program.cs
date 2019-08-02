using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace XmlDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            /*      
             // write method, good!
            List<Shape> a = new List<Shape>();
            a.Add(new Square(1));
            a.Add(new Circle(5));
            a.Add(new Square(4));
            
            a.Add(new Square(8));
            a.Add(new Circle(5));
            a.Add(new Circle(7));
            a.Add(new Circle(1));
            a.Add(new Square(9));
            a.Add(new Circle(3));
            TextWriter fS = new StreamWriter("database.xml");           
            XmlSerializer xS = new XmlSerializer(a.GetType());
            Console.WriteLine("writing");
            try
            {
                xS.Serialize(fS, a);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.ToString());
                Console.ReadKey();
            }
            fS.Close();
            Console.WriteLine("Fin"); 



            //------deserialization here  ---  READ/LOAD method, good!

            List<Shape> list = new List<Shape>();
            StreamReader reader = new StreamReader("database.xml");

            XmlSerializer serializer = new XmlSerializer(typeof(List<Shape>));
               // typeof(list.GetType());

            
            list = (List<Shape>)serializer.Deserialize(reader);

            foreach(Shape s in list){
                Console.WriteLine("this is a "+s.ToString());
            }
            reader.Close();
             * 
             * */

            Database database = new Database();
            List<Shape> shapes = database.LoadShapes();
            foreach (Shape shape in shapes)
                Console.WriteLine(shape.ToString());

        }
    }
}
