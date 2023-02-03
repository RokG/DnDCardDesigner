using CardDesigner.Domain.Enums;

namespace CardDesigner.Domain.Models
{
    public class CharacterClassModel
    {
        public int ID { get; set; }
        public int Level { get; set; }
        public CharacterClassType Class { get; set; }
        public CharacterModel Character { get; set; }
    }
}
