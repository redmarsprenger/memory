using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Serialization;

namespace Memory.Classes
{
    class ObjectSterializer
    {
        BinaryFormatter formatter = new BinaryFormatter();
        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializableObject"></param>
        /// <param name="fileName"></param>
        public void SerializeObject<T>(string serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }

            try
            {
                // Create a file to write to.
                File.WriteAllText(fileName, serializableObject);
            }
            catch (Exception ex)
            {
                //Log exception here
            }
        }


        /// <summary>
        /// Deserializes an xml file into an object list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string DeSerializeObject<T>(string fileName)
        {
            string objectOut = "null";
            if (string.IsNullOrEmpty(fileName)) { return null; }



            try
            {
                // Open the file to read from.
                objectOut = File.ReadAllText(fileName);
            }
            catch (Exception ex)
            {
                //Log exception here
            }

            return objectOut;
        }
        //        BinaryFormatter formatter = new BinaryFormatter();
        //        /// <summary>
        //        /// Serializes an object.
        //        /// </summary>
        //        /// <typeparam name="T"></typeparam>
        //        /// <param name="serializableObject"></param>
        //        /// <param name="fileName"></param>
        //        public void SerializeObject<T>(T serializableObject, string fileName)
        //        {
        //            if (serializableObject == null) { return; }
        //
        //            try
        //            {
        //
        ////                // Save the Button to a string.
        ////                string serializabledObject = XamlWriter.Save(serializableObject);
        ////
        ////                using (FileStream fs = File.Create(fileName))
        ////                {
        ////                    AddText(fs, "This is some text");
        ////                    AddText(fs, "This is some more text,");
        ////                    AddText(fs, "\r\nand this is on a new line");
        ////                    AddText(fs, "\r\n\r\nThe following is a subset of characters:\r\n");
        ////
        ////                    for (int i = 1; i < 120; i++)
        ////                    {
        ////                        AddText(fs, Convert.ToChar(i).ToString());
        ////
        ////                    }
        ////                }
        //
        //                //                string memString = serializabledObject;
        //                //                // convert string to stream
        //                //                byte[] buffer = Encoding.ASCII.GetBytes(memString);
        //                //                MemoryStream ms = new MemoryStream(buffer);
        //                //                //write to file
        //                //                FileStream file = new FileStream(fileName, FileMode.Create, FileAccess.Write);
        //                //                ms.WriteTo(file);
        //                //                file.Close();
        //                //                ms.Close();
        //                //
        //                //                FileStream writerFileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write);
        //                //                
        //                //                formatter.Serialize(writerFileStream, serializabledObject);
        //                //                
        //                //                writerFileStream.Close();
        //
        //                //                XmlDocument xmlDocument = new XmlDocument();
        //                //                XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
        //                //                using (MemoryStream stream = new MemoryStream())
        //                //                {
        //                //                    serializer.Serialize(stream, serializableObject);
        //                //                    stream.Position = 0;
        //                //                    xmlDocument.Load(stream);
        //                //                    xmlDocument.Save(fileName);
        //                //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                //Log exception here
        //            }
        //        }
        //
        //
        //        /// <summary>
        //        /// Deserializes an xml file into an object list
        //        /// </summary>
        //        /// <typeparam name="T"></typeparam>
        //        /// <param name="fileName"></param>
        //        /// <returns></returns>
        //        public T DeSerializeObject<T>(string fileName)
        //        {
        //            if (string.IsNullOrEmpty(fileName)) { return default(T); }
        //
        //            T objectOut = default(T);
        //
        //            try
        //            {
        //                //                XmlDocument xmlDocument = new XmlDocument();
        //                //                xmlDocument.Load(fileName);
        //                //                string xmlString = xmlDocument.OuterXml;
        //                //
        //                //                using (StringReader read = new StringReader(xmlString))
        //                //                {
        //                //                    Type outType = typeof(T);
        //                //
        //                //                    XmlSerializer serializer = new XmlSerializer(outType);
        //                //                    using (XmlReader reader = new XmlTextReader(read))
        //                //                    {
        //                //                        objectOut = (T)serializer.Deserialize(reader);
        //                //                    }
        //                //                }
        //
        //
        //
        //                FileStream readerFileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        //
        //                string serializabledObject = (string)formatter.Deserialize(readerFileStream);
        //
        //                readerFileStream.Close();
        //
        //
        //                // Load the button
        //                StringReader stringReader = new StringReader(serializabledObject);
        //                XmlReader xmlReader = XmlReader.Create(stringReader);
        //                objectOut = (T)XamlReader.Load(xmlReader);
        //            }
        //            catch (Exception ex)
        //            {
        //                //Log exception here
        //            }
        //
        //            return objectOut;
        //        }







    }
}
