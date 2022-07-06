using AutoMapper;
using CardDesigner.DataAccess.DbContexts;
using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using System.Threading.Tasks;

namespace CardDesigner.DataAccess.Services
{
    public class DatabaseSpellCardUpdater : ISpellCardUpdater
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
        public DatabaseSpellCardUpdater(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all characters from database
        /// </summary>
        /// <returns></returns>
        public async Task<SpellCardModel> UpdateSpellCard(SpellCardModel spellCard)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                SpellCard spellCardEntity = _mapper.Map<SpellCard>(spellCard);

                SpellCard createdSpellCardEntity = dbContext.SpellCards.Update(spellCardEntity).Entity;
                await dbContext.SaveChangesAsync();

                return _mapper.Map<SpellCardModel>(spellCardEntity);
            }
        }
    }
}