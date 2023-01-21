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
    public class DatabaseSpellCardDeleter : ISpellCardDeleter
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
        public DatabaseSpellCardDeleter(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all characters from database
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DeleteSpellCard(SpellCardModel spellCard)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                try
                {
                    SpellCard spellCardEntity = _mapper.Map<SpellCard>(spellCard);
                    if (dbContext.SpellCards.Contains(spellCardEntity))
                    {
                        dbContext.SpellCards.Remove(spellCardEntity);
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