using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Musix.Models
{
    public class JsonProviderEntity
    {
        [JsonIgnore]
        public object Instance;

        public string EntityName;
        public string EntityData;

        public void Save()
        {
            if (Instance != null)
            {
                EntityData = JsonConvert.SerializeObject(Instance);
            }
        }
        public static JsonProviderEntity FromInstance(object Instance, string InstanceName)
        {
            JsonProviderEntity entity = new JsonProviderEntity();
            entity.Instance = Instance;
            entity.EntityName = InstanceName;
            entity.Save();
            return entity;
        }
    }
}