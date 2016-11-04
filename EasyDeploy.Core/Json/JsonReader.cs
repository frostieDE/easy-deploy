using EasyDeploy.Core.Json.Converter;
using Newtonsoft.Json;

namespace EasyDeploy.Core.Json
{
    public class JsonReader : IJsonReader
    {
        public T ReadJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, new DeviceTypeConverter());
        }
    }
}
