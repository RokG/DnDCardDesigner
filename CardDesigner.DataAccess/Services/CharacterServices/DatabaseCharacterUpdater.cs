using AutoMapper;
using CardDesigner.DataAccess.DbContexts;
using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CardDesigner.DataAccess.Services
{
    public class DatabaseCharacterUpdater : ICharacterUpdater
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
        public DatabaseCharacterUpdater(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all characters from database
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateCharacter(CharacterModel character)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                try
                {
                    // Get character from database
                    Character characterEntity = dbContext.Characters
                        .Include(d => d.SpellDeck)
                        .Single(d => d.ID == character.ID);

                    // Get spell deck from database
                    SpellDeck charEntity = dbContext.SpellDecks.Single(c => c.Name == character.SpellDeck.Name);
                    characterEntity.SpellDeck = charEntity;

                    // Update database
                    if (dbContext.Characters.Contains(characterEntity))
                    {
                        dbContext.Characters.Update(characterEntity);
                        await dbContext.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
