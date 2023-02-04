using CardDesigner.Domain.Models;
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
    }
}
