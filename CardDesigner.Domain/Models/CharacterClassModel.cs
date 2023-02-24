using CardDesigner.Domain.HelperModels;

namespace CardDesigner.Domain.Models
{
    public class CharacterClassModel
    {
        public int ID { get; set; }
        public CharacterModel Character { get; set; }

        // Properties
        public int Level { get; set; }
        public string ClassID { get; set; }
        public string ClassSpecialization { get; set; }
        public ClassModel Class { get; set; }
    }
}
