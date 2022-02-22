using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RFDDesktop.Infrastructer.Extentions
{
    public static class XmlConverter
    {



        public static string Serialize<T>(this T value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                var stringWriter = new StringWriter();
                using (var writer = XmlWriter.Create(stringWriter,new XmlWriterSettings() { OmitXmlDeclaration=true}))
                {
                    xmlserializer.Serialize(writer, value);
                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred", ex);
            }
        }

        public static T DeserializeObject<T>(string xml)
        where T : new()
        {
            if (string.IsNullOrEmpty(xml))
            {
                return new T();
            }
            try
            {
                using (var stringReader = new StringReader(xml))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(stringReader);
                }
            }
            catch (Exception)
            {
                return new T();
            }
        }



        public static T JsonToXml<T>(string jsonString) where T : new()
        {

            try
            {




                XmlDocument xmlDocument = new XmlDocument();
                XElement element;
                using (var reader = JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(jsonString), XmlDictionaryReaderQuotas.Max))
                {
                    element = XElement.Load(reader);
                    //xmlDocument.LoadXml(element.ToString());

                    //var xxX = element.Elements("data").FirstOrDefault().Value;
                }


                var xmlString = XDocument.Load(JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(jsonString), XmlDictionaryReaderQuotas.Max));
                var serializer = new XmlSerializer(typeof(T));
                var readerr = xmlString.Root.CreateReader();
                return (T)serializer.Deserialize(readerr);
                //using (var stringReader = new StringReader(element.CreateReader()))
                //{

                //}


            }
            catch (Exception)
            {

                return new T();
            }

        }


    }
    
}
