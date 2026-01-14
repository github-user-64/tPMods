using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace BedWars.BedWarsData
{
    public class RectangleJsonConverter : JsonConverter<Rectangle>
    {
        public override void WriteJson(JsonWriter writer, Rectangle value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("X");
            writer.WriteValue(value.X);
            writer.WritePropertyName("Y");
            writer.WriteValue(value.Y);
            writer.WritePropertyName("Width");
            writer.WriteValue(value.Width);
            writer.WritePropertyName("Height");
            writer.WriteValue(value.Height);
            writer.WriteEndObject();
        }

        public override Rectangle ReadJson(JsonReader reader, Type objectType, Rectangle existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            int x = (int)obj["X"];
            int y = (int)obj["Y"];
            int width = (int)obj["Width"];
            int height = (int)obj["Height"];
            return new Rectangle(x, y, width, height);
        }
    }
}
