using AutoMapper;
using CardDesigner.DataAccess.DbContexts;
using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardDesigner.DataAccess.Services
{
    public class DatabaseSpellDeckUpdater : ISpellDeckUpdater
    {
        #region Private fields

        private readonly CardDesignerDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="dbContextFactory">Database context factory</param>
        /// <param name="mapper">Mapper object</param>
        public DatabaseSpellDeckUpdater(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<SpellDeckModel> UpdateSpellDeck(SpellDeckModel spellDeck)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                try
                {
                    // Get spell deck from database
                    SpellDeck spellDeckEntity = dbContext.SpellDecks
                        .Include(sd=>sd.SpellCards)
                        .Single(sc => sc.ID == spellDeck.ID);

                    // Loop over cards in source deck
                    foreach (SpellCardModel spellCard in spellDeck.SpellCards)
                    {
                        // If any card is new, add it to the list
                        if (!spellDeckEntity.SpellCards.Where(sd => sd.ID == spellCard.ID).Any())
                        {
                            SpellCard spellCardEntity = _mapper.Map<SpellCard>(spellCard);
                            spellDeckEntity.SpellCards.Add(spellCardEntity);
                        }
                    }

                    await dbContext.SaveChangesAsync();

                    return _mapper.Map<SpellDeckModel>(spellDeckEntity); ;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}