using EasyDeploy.Core.Logger;

namespace EasyDeploy.Core.Json
{
    public class DebugJsonReader : IJsonReader
    {
        private readonly IJsonReader jsonReader;
        private readonly ILogger logger;

        public DebugJsonReader(IJsonReader jsonReader, ILogger logger)
        {
            this.jsonReader = jsonReader;
            this.logger = logger;
        }

        public T ReadJson<T>(string json)
        {
            logger.Log($"Parse JSON: \n{json}");
            return jsonReader.ReadJson<T>(json);
        }
    }
}
