using AutoMapper;
using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Models;
using System.Collections.Generic;

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
                cfg.CreateMap<SpellDeckDesignModel, SpellDeckDesignEntity>();
                cfg.CreateMap<ItemDeckDesignModel, ItemDeckDesignEntity>();
                cfg.CreateMap<DeckBackgroundDesignModel, DeckBackgroundDesignEntity>();
                cfg.CreateMap<SpellDeckDesignLinkerModel, SpellDeckDesignLinkerEntity>();
                cfg.CreateMap<ItemDeckDesignLinkerModel, ItemDeckDesignLinkerEntity>();
                cfg.CreateMap<CharacterModel, CharacterEntity>();
                cfg.CreateMap<CharacterClassModel, CharacterClassEntity>();
                cfg.CreateMap<CharacterAbilitiesModel, CharacterAbilitiesEntity>();
                cfg.CreateMap<CasterStatsModel, CasterStatsEntity>();
                cfg.CreateMap<SpellCardModel, SpellCardEntity>();
                cfg.CreateMap<ItemCardModel, ItemCardEntity>();
                cfg.CreateMap<CharacterCardModel, CharacterCardEntity>();
                cfg.CreateMap<SpellDeckModel, SpellDeckEntity>();
                cfg.CreateMap<ItemDeckModel, ItemDeckEntity>();
                cfg.CreateMap<CharacterDeckModel, CharacterDeckEntity>();

                cfg.CreateMap<SpellDeckDesignEntity, SpellDeckDesignModel>();
                cfg.CreateMap<ItemDeckDesignEntity, ItemDeckDesignModel>();
                cfg.CreateMap<DeckBackgroundDesignEntity, DeckBackgroundDesignModel>();
                cfg.CreateMap<SpellDeckDesignLinkerEntity, SpellDeckDesignLinkerModel>();
                cfg.CreateMap<ItemDeckDesignLinkerEntity, ItemDeckDesignLinkerModel>();
                cfg.CreateMap<CharacterEntity, CharacterModel>();
                cfg.CreateMap<CharacterClassEntity, CharacterClassModel>();
                cfg.CreateMap<CharacterAbilitiesEntity, CharacterAbilitiesModel>();
                cfg.CreateMap<CasterStatsEntity, CasterStatsModel>();
                cfg.CreateMap<SpellCardEntity, SpellCardModel>();
                cfg.CreateMap<ItemCardEntity, ItemCardModel>();
                cfg.CreateMap<CharacterCardEntity, CharacterCardModel>();
                cfg.CreateMap<SpellDeckEntity, SpellDeckModel>();
                cfg.CreateMap<ItemDeckEntity, ItemDeckModel>();
                cfg.CreateMap<CharacterDeckEntity, CharacterDeckModel>();
            });

            return config.CreateMapper();
        }
    }
}