using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.Text;

namespace BBS.Utils
{
    public static class CommonUtils
    {
        public static string JSONSerializeFromList<T>(T obj)
        {
            string retVal = String.Empty;
            using (MemoryStream ms = new())
            {
                DataContractJsonSerializer serializer = new(obj!.GetType());
                serializer.WriteObject(ms, obj);
                var byteArray = ms.ToArray();
                retVal = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            }
            return retVal;
        }

        public static string JSONSerialize(object obj)
        {
            string repVal = JsonConvert.SerializeObject(obj);
            Console.WriteLine(repVal);
            return repVal; 
        }
    }
}
