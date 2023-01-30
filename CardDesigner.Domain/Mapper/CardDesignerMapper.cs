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
                cfg.CreateMap<SpellDeckDesignModel, SpellDeckDesignEntity>();
                cfg.CreateMap<ItemDeckDesignModel, ItemDeckDesignEntity>();
                cfg.CreateMap<CharacterDeckDesignModel, CharacterDeckDesignEntity>();
                cfg.CreateMap<SpellDeckDesignLinkerModel, SpellDeckDesignLinkerEntity>();
                cfg.CreateMap<ItemDeckDesignLinkerModel, ItemDeckDesignLinkerEntity>();
                cfg.CreateMap<CharacterModel, CharacterEntity>();
                cfg.CreateMap<SpellCardModel, SpellCardEntity>();
                cfg.CreateMap<SpellDeckModel, SpellDeckEntity>();
                cfg.CreateMap<ItemCardModel, ItemCardEntity>();
                cfg.CreateMap<ItemDeckModel, ItemDeckEntity>();

                cfg.CreateMap<SpellDeckDesignEntity, SpellDeckDesignModel>();
                cfg.CreateMap<ItemDeckDesignEntity, ItemDeckDesignModel>();
                cfg.CreateMap<CharacterDeckDesignEntity, CharacterDeckDesignModel>();
                cfg.CreateMap<SpellDeckDesignLinkerEntity, SpellDeckDesignLinkerModel>();
                cfg.CreateMap<ItemDeckDesignLinkerEntity, ItemDeckDesignLinkerModel>();
                cfg.CreateMap<CharacterEntity, CharacterModel>();
                cfg.CreateMap<SpellCardEntity, SpellCardModel>();
                cfg.CreateMap<SpellDeckEntity, SpellDeckModel>();
                cfg.CreateMap<ItemCardEntity, ItemCardModel>();
                cfg.CreateMap<ItemDeckEntity, ItemDeckModel>();
            });

            return config.CreateMapper();
        }
    }
}