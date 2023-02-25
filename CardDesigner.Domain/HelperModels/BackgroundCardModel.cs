using CardDesigner.Domain.Models;

namespace CardDesigner.Domain.HelperModels
{
    public class BackgroundCardModel : ICard
    {
        public int ID { get; set; } = -1;
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
