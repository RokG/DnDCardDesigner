namespace CardDesigner.Domain.Entities
{
    public class ItemDeckItemCard
    {
        public int ItemDeckID { get; set; }
        public ItemDeckEntity ItemDeck { get; set; }
        public int ItemCardID { get; set; }
        public ItemCardEntity ItemCard { get; set; }
    }
}
