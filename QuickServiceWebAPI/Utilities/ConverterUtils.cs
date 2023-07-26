using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Utilities
{
    public static class ConverterUtils
    {
        public static long TimeSpanToTicks(TimeSpan timeSpan)
        {
            return timeSpan.Ticks;
        }
        
    }
    public class IntToTimeSpanConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            Int64 value = (Int64)reader.Value;
            return TimeSpan.FromSeconds(value);
            
        }

        public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanWrite => false;
    }
    public class CustomResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(member, memberSerialization);
            if (prop.DeclaringType == typeof(Slametric)
                && (prop.UnderlyingName == "ResponseTime" || prop.UnderlyingName == "ResolutionTime"))
            {
                prop.Converter = new IntToTimeSpanConverter();
            }
            return prop;
        }
    }
}
