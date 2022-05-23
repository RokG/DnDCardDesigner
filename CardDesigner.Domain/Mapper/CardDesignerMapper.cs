using AutoMapper;
using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Mapper
{
    public static class CardDesignerMapper
    {
        public static IMapper CreateMapper()
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<CharacterModel, Character>();
                cfg.CreateMap<CardDeckModel, CardDeck>();
                cfg.CreateMap<SpellCardModel, SpellCard>();
            });

            return config.CreateMapper();
        }
    }
}