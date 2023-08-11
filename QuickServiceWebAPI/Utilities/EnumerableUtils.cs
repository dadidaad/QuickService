using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;

namespace QuickServiceWebAPI.Utilities
{
    public static class EnumerableUtils
    {
        public static bool IsAny<T>(this IEnumerable<T> data)
        {
            return data != null && data.Any();
        }
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        public static T DeepCopy<T>(this T self)
        {
            var serialized = JsonConvert.SerializeObject(self);
            return JsonConvert.DeserializeObject<T>(serialized);
        }

        public static T DeepCopyForReferenceType<T>(T item)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, item);
            stream.Seek(0, SeekOrigin.Begin);
            T result = (T)formatter.Deserialize(stream);
            stream.Close();
            return result;
        }
    }
}
