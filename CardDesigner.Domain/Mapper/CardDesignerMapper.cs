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
                #region Model to Entity

                // Spell
                cfg.CreateMap<SpellCardModel, SpellCardEntity>();
                cfg.CreateMap<SpellDeckModel, SpellDeckEntity>();
                cfg.CreateMap<SpellDeckDesignModel, SpellDeckDesignEntity>();
                cfg.CreateMap<SpellDeckDesignLinkerModel, SpellDeckDesignLinkerEntity>();

                // Item
                cfg.CreateMap<ItemCardModel, ItemCardEntity>();
                cfg.CreateMap<ItemDeckModel, ItemDeckEntity>();
                cfg.CreateMap<ItemDeckDesignModel, ItemDeckDesignEntity>();
                cfg.CreateMap<ItemDeckDesignLinkerModel, ItemDeckDesignLinkerEntity>();

                // Character base
                cfg.CreateMap<CharacterModel, CharacterEntity>();
                cfg.CreateMap<DeckBackgroundDesignModel, DeckBackgroundDesignEntity>();
                cfg.CreateMap<CharacterClassModel, CharacterClassEntity>();
                cfg.CreateMap<CharacterAbilitiesModel, CharacterAbilitiesEntity>();
                cfg.CreateMap<CasterStatsModel, CasterStatsEntity>();

                // Character
                cfg.CreateMap<CharacterCardModel, CharacterCardEntity>();
                cfg.CreateMap<CharacterDeckModel, CharacterDeckEntity>();
                cfg.CreateMap<CharacterDeckDesignModel, CharacterDeckDesignEntity>();
                cfg.CreateMap<CharacterDeckDesignLinkerModel, CharacterDeckDesignLinkerEntity>();

                // Minion base
                cfg.CreateMap<MinionModel, MinionEntity>();

                // Minion
                cfg.CreateMap<MinionCardModel, MinionCardEntity>();
                cfg.CreateMap<MinionDeckModel, MinionDeckEntity>();
                //cfg.CreateMap<MinionDeckModel, MinionDeckEntity>().ForMember(dest => dest.MinionCards, act => act.MapFrom(src => src.MinionCards));
                //cfg.CreateMap<MinionDeckModel, MinionDeckEntity>().ForMember(dest => dest.MinionCards, act => act.MapFrom(src => src.MinionCards.Select(f => f.Minion)));
                cfg.CreateMap<MinionDeckDesignModel, MinionDeckDesignEntity>();
                cfg.CreateMap<MinionDeckDesignLinkerModel, MinionDeckDesignLinkerEntity>();

                #endregion

                #region Entity to Model

                // Spells
                cfg.CreateMap<SpellCardEntity, SpellCardModel>();
                cfg.CreateMap<SpellDeckEntity, SpellDeckModel>();
                cfg.CreateMap<SpellDeckDesignEntity, SpellDeckDesignModel>();
                cfg.CreateMap<SpellDeckDesignLinkerEntity, SpellDeckDesignLinkerModel>();

                // Items
                cfg.CreateMap<ItemCardEntity, ItemCardModel>();
                cfg.CreateMap<ItemDeckEntity, ItemDeckModel>();
                cfg.CreateMap<ItemDeckDesignEntity, ItemDeckDesignModel>();
                cfg.CreateMap<ItemDeckDesignLinkerEntity, ItemDeckDesignLinkerModel>();

                // Character base
                cfg.CreateMap<CharacterEntity, CharacterModel>();
                cfg.CreateMap<DeckBackgroundDesignEntity, DeckBackgroundDesignModel>();
                cfg.CreateMap<CharacterClassEntity, CharacterClassModel>();
                cfg.CreateMap<CharacterAbilitiesEntity, CharacterAbilitiesModel>();
                cfg.CreateMap<CasterStatsEntity, CasterStatsModel>();

                // Character
                cfg.CreateMap<CharacterCardEntity, CharacterCardModel>();
                cfg.CreateMap<CharacterDeckEntity, CharacterDeckModel>();
                cfg.CreateMap<CharacterDeckDesignEntity, CharacterDeckDesignModel>();
                cfg.CreateMap<CharacterDeckDesignLinkerEntity, CharacterDeckDesignLinkerModel>();

                // Minion base
                cfg.CreateMap<MinionEntity, MinionModel>();

                // Minion
                cfg.CreateMap<MinionCardEntity, MinionCardModel>();
                cfg.CreateMap<MinionDeckEntity, MinionDeckModel>();
                //cfg.CreateMap<MinionDeckEntity, MinionDeckModel>().ForMember(dest => dest.MinionCards, act => act.MapFrom(src => src.MinionCards));
                //cfg.CreateMap<MinionDeckEntity, MinionDeckModel>().ForMember(dest => dest.MinionCards, act => act.MapFrom(src => src.MinionCards.Select(f=>f.Minion)));
                cfg.CreateMap<MinionDeckDesignLinkerEntity, MinionDeckDesignLinkerModel>();
                cfg.CreateMap<MinionDeckDesignEntity, MinionDeckDesignModel>();

                #endregion

            });

            return config.CreateMapper();
        }
    }
}