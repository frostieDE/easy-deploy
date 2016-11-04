using EasyDeploy.Core.Json.Converter;
using Newtonsoft.Json;

namespace EasyDeploy.Core.Json
{
    public  class JsonWriter : IJsonWriter
    {
        public string WriteJson(object data)
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented, new DeviceTypeConverter());
        }
    }
}
