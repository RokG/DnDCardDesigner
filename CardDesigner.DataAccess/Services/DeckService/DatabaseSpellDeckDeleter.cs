using AutoMapper;
using CardDesigner.DataAccess.DbContexts;
using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CardDesigner.DataAccess.Services
{
    public class DatabaseSpellDeckDeleter : ISpellDeckDeleter
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
        public DatabaseSpellDeckDeleter(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<bool> DeleteSpellDeck(SpellDeckModel spellDeck)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                try
                {
                    SpellDeck spellDeckEntity = _mapper.Map<SpellDeck>(spellDeck);
                    if (dbContext.SpellDecks.Contains(spellDeckEntity))
                    {
                        dbContext.SpellDecks.Remove(spellDeckEntity);
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