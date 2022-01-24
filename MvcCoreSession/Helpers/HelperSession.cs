using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace MvcCoreSession.Helpers
{
    public class HelperSession
    {
        //TENDREMOS DOS METODOS STATIC
        public static byte[]  ObjectToByte(Object obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static Object ByteToObject(byte[] datos)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(datos, 0, datos.Length);
                ms.Seek(0, SeekOrigin.Begin);
                Object obj = (Object)formatter.Deserialize(ms);
                return obj;
            }
        }

        //METODO PARA RECIBIR UN OBJETO Y DEVOLVER EL STRING
        //SERIALIZADO
        public static String SerializeObject(object objeto)
        {
            string data =
                JsonConvert.SerializeObject(objeto);
            return data;
        }

        //METODO QUE RECIBIRA EL JSON Y DEVOLVEMOS EL OBJETO
        //QUE LO REPRESENTA DESERIALIZADO
        public static T DeserializeObject<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
