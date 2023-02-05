using CardDesigner.Domain.Models;
using System.Collections.Generic;

namespace CardDesigner.Domain.Services
{
    public interface IJsonFileItemService
    {
        public IEnumerable<ArmourModel> LoadArmours(string filePath);
        public IEnumerable<WeaponModel> LoadWeapons(string filePath);
        public IEnumerable<ClassModel> LoadClasses(string filePath);
    }
}
