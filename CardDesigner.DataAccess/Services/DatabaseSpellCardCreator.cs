using AutoMapper;
using CardDesigner.DataAccess.DbContexts;
using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesigner.DataAccess.Services
{
    public class DatabaseSpellCardCreator : ISpellCardCreator
    {
        private readonly CardDesignerDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public DatabaseSpellCardCreator(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<SpellCardModel> CreateSpellCard(SpellCardModel character)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                SpellCard characterEntity = _mapper.Map<SpellCard>(character);

                SpellCard createdClient = dbContext.SpellCards.Add(characterEntity).Entity;
                await dbContext.SaveChangesAsync();

                return _mapper.Map<SpellCardModel>(createdClient);
            }
        }
    }
}