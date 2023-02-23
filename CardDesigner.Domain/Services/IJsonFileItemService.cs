using CardDesigner.Domain.HelperModels;
using System.Collections.Generic;

namespace CardDesigner.Domain.Services
{
    public interface IJsonFileItemService
    {
        public IEnumerable<ArmourModel> LoadArmours(string filePath);
        public IEnumerable<WeaponModel> LoadWeapons(string filePath);
        public IEnumerable<ClassModel> LoadClasses(string filePath);
        public IEnumerable<ConsumableModel> LoadConsumables(string filePath);
        public IEnumerable<UsableModel> LoadUsables(string filePath);
        public IEnumerable<ClothingModel> LoadClothings(string filePath);
    }
}
