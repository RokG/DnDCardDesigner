﻿using AutoMapper;
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
                cfg.CreateMap<ItemCardModel, ItemCard>();
            });

            return config.CreateMapper();
        }
    }
}