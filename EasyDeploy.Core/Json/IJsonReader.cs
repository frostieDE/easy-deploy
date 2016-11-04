using EasyDeploy.Core.Model;

namespace EasyDeploy.Core.Json
{
    public interface IJsonReader
    {
        T ReadJson<T>(string json);
    }
}
