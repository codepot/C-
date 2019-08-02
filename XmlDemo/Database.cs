using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace XmlDemo
{
    public class Database
    {
        private const string file = "database.xml";

        public void SaveShapes(List<Shape> shapes)
        {
            TextWriter textWriter = new StreamWriter(file);
            XmlSerializer serializer = new XmlSerializer(shapes.GetType());
            try
            {
                serializer.Serialize(textWriter, shapes);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            textWriter.Close();
        }

        public List<Shape> LoadShapes()
        {
            List<Shape> list = new List<Shape>();
            StreamReader reader = new StreamReader(file);

            XmlSerializer serializer = new XmlSerializer(typeof(List<Shape>));

            list = (List<Shape>)serializer.Deserialize(reader);
            reader.Close();
            return list;
        }
    }

}
