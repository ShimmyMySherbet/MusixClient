using System;
using System.IO;
using Musix.Windows.API.Interfaces;
using Newtonsoft.Json;

namespace Musix.Models.Modules
{
    public class JsonConfigurationProvider : IConfigurationProvider
    {
        private JsonProviderModel JsonModel = new JsonProviderModel();
        public string FileName = "Config.json";

        public void DeleteEntity(string EntityName)
        {
            lock (JsonModel)
            {
                if (JsonModel != null)
                {
                    JsonModel.Entities.RemoveAll(x => string.Equals(x.EntityName, EntityName, StringComparison.InvariantCultureIgnoreCase));
                }
            }
        }

        public string[] GetEntities()
        {
            lock (JsonModel)
            {
                if (JsonModel != null)
                {
                    return JsonModel.Entities.ConvertAll(x => x.EntityName).ToArray();
                }
                else
                {
                    return new string[] { };
                }
            }
        }

        public void Load()
        {
            lock (JsonModel)
            {
                if (File.Exists(FileName))
                {
                    JsonModel = JsonConvert.DeserializeObject<JsonProviderModel>(File.ReadAllText(FileName));
                }
                else
                {
                    JsonModel = new JsonProviderModel();
                }
            }
        }

        public T LoadEntity<T>(string EntityName)
        {
            lock (JsonModel)
            {
                foreach (JsonProviderEntity entity in JsonModel.Entities)
                {
                    if (string.Equals(entity.EntityName, EntityName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (entity.Instance != null)
                        {
                            return (T)entity.Instance;
                        }
                        else
                        {
                            entity.Instance = JsonConvert.DeserializeObject<T>(entity.EntityData);
                            return (T)entity.Instance;
                        }
                    }
                }
                T Instance = Activator.CreateInstance<T>();
                JsonModel.Entities.Add(JsonProviderEntity.FromInstance(Instance, EntityName));
                return Instance;
            }
        }

        public void Save()
        {
            lock (JsonModel)
            {
                if (JsonModel == null)
                {
                    JsonModel = new JsonProviderModel();
                }
                JsonModel.Entities.ForEach(x => x.Save());
                File.WriteAllText(FileName, JsonConvert.SerializeObject(JsonModel));
            }
        }

        public void WriteEntity<T>(T Entity, string EntityName)
        {
            lock (JsonModel)
            {
                JsonModel.Entities.RemoveAll(x => string.Equals(EntityName, x.EntityName, StringComparison.InvariantCultureIgnoreCase));
                JsonModel.Entities.Add(JsonProviderEntity.FromInstance(Entity, EntityName));
            }
        }
    }
}