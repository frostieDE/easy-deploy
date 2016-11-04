using EasyDeploy.Core.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace EasyDeploy.Core.Json.Converter
{
    public class DeviceTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DeployAction) || objectType.IsSubclassOf(typeof(DeployAction));
        }

        public override bool CanRead { get { return true; } }

        public override bool CanWrite { get { return true; } }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var type = jsonObject["Type"].Value<string>();

            var assembly = Assembly.GetAssembly(typeof(DeployAction));
            var targetAssembly = assembly.GetName().Name;
            var targetType = typeof(DeployAction).Namespace + "." + type;
            var targetObject = Activator.CreateInstance(targetAssembly, targetType).Unwrap();

            serializer.Populate(jsonObject.CreateReader(), targetObject);
            return targetObject;
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, JsonSerializer serializer)
        {
            // Remove this converter to prevent loops
            var wasRemoved = serializer.Converters.Remove(this);

            var jsonObject = JObject.FromObject(value, serializer);
            jsonObject.AddFirst(new JProperty("Type", value.GetType().Name));

            writer.WriteToken(jsonObject.CreateReader());

            if (wasRemoved)
            {
                // re-add converter if it was removed in the first place
                serializer.Converters.Add(this);
            }
        }
    }
}
