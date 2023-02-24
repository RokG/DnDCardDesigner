using CardDesigner.Domain.HelperModels;
using CardDesigner.Domain.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CardDesigner.DataAccess.Services
{
    public class JsonFileItemsService : IJsonFileItemService
    {
        public IEnumerable<ArmourModel> LoadArmours(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<IEnumerable<ArmourModel>>(json);
        }

        public IEnumerable<WeaponModel> LoadWeapons(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<IEnumerable<WeaponModel>>(json);
        }

        public IEnumerable<ClassModel> LoadClasses(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<IEnumerable<ClassModel>>(json);
        }

        public IEnumerable<ConsumableModel> LoadConsumables(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<IEnumerable<ConsumableModel>>(json);
        }
        public IEnumerable<UsableModel> LoadUsables(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<IEnumerable<UsableModel>>(json);
        }
        public IEnumerable<ClothingModel> LoadClothings(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<IEnumerable<ClothingModel>>(json);
        }
    }
}
