using CardDesigner.Domain.HelperModels;

namespace CardDesigner.Domain.Models
{
    public class CharacterClassModel
    {
        // Entity links
        public int ID { get; set; }

        // Properties
        public int Level { get; set; }
        public string ClassID { get; set; }
        public string ClassSpecialization { get; set; }
        public ClassModel Class { get; set; }
    }
}
