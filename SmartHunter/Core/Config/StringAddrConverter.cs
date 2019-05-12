using Newtonsoft.Json;
using System;

namespace SmartHunter.Core.Config
{
    public class StringAddrConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ulong);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            ulong ulongValue = (ulong) value;
            writer.WriteRawValue($"\"0x{ulongValue:X}\"");
        }

        public override bool CanRead => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            string hex = reader.ReadAsString();
            if (hex == null) {
                return existingValue;
            }

            if (hex.StartsWith("0x")) {
                // Strip hex prefix
                hex = hex.Substring(2);
            }

            return Convert.ToUInt64(hex, 16);
        }
    }
}
