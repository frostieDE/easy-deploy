using EasyDeploy.Core.IO;
using EasyDeploy.Core.Json;
using System;
using System.IO;

namespace EasyDeploy.GUI.Settings
{
    public class JsonSettingsStorage : ISettingStorage
    {
        private const string Filename = "settings.json";

        #region Services

        private readonly IFileReader fileReader;
        private readonly IFileWriter fileWriter;

        private readonly IJsonReader jsonReader;
        private readonly IJsonWriter jsonWriter;

        #endregion

        public JsonSettingsStorage(IFileReader fileReader, IFileWriter fileWriter, IJsonReader jsonReader, IJsonWriter jsonWriter)
        {
            this.fileReader = fileReader;
            this.fileWriter = fileWriter;
            this.jsonReader = jsonReader;
            this.jsonWriter = jsonWriter;
        }

        protected string GetFolder()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EasyDeploy");
        }

        protected string GetFile()
        {
            return Path.Combine(GetFolder(), Filename);
        }

        public Settings LoadSettings()
        {
            var file = GetFile();

            if (File.Exists(file))
            {
                var task = this.fileReader.ReadFileAsync(file);
                task.Wait();
                var json = task.Result;

                return jsonReader.ReadJson<Settings>(json);
            }
            else
            {
                return new Settings();
            }
        }

        public void SaveSettings(Settings settings)
        {
            if(!Directory.Exists(GetFolder()))
            {
                Directory.CreateDirectory(GetFolder());
            }

            var file = GetFile();
            var json = jsonWriter.WriteJson(settings);

            fileWriter.WriteFileAsync(file, json).Wait();
        }
    }
}
