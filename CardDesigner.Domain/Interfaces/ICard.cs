namespace CardDesigner.Domain.Models
{
    public interface ICard
    {
        public string Name { get; set; }

        public int ID { get; set; }

        public string Title { get; set; }

        public bool Conflicts(ICard card)
        {
            return false;
        }
    }
}