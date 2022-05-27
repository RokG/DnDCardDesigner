using AutoMapper;
using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Models;

namespace CardDesigner.Domain.Mapper
{
    public static class CardDesignerMapper
    {
        /// <summary>
        /// Create map of Entity - Model
        /// </summary>
        /// <returns></returns>
        public static IMapper CreateMapper()
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<CharacterModel, Character>();
                cfg.CreateMap<SpellCardModel, SpellCard>();
                cfg.CreateMap<SpellDeckModel, SpellDeck>();
                cfg.CreateMap<ItemCardModel, ItemCard>();

                cfg.CreateMap<Character, CharacterModel>();
                cfg.CreateMap<SpellCard, SpellCardModel>();
                cfg.CreateMap<SpellDeck, SpellDeckModel>();
                cfg.CreateMap<ItemCard, ItemCardModel>();
            });

            return config.CreateMapper();
        }
    }
}