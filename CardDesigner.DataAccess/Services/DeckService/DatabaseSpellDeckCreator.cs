using AutoMapper;
using CardDesigner.DataAccess.DbContexts;
using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using System.Threading.Tasks;

namespace CardDesigner.DataAccess.Services
{
    public class DatabaseSpellDeckCreator : ISpellDeckCreator
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
        public DatabaseSpellDeckCreator(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<SpellDeckModel> CreateSpellDeck(SpellDeckModel spellDeck)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                SpellDeck spellDeckEntity = _mapper.Map<SpellDeck>(spellDeck);

                SpellDeck createdSpellDeckEntity = dbContext.SpellDecks.Add(spellDeckEntity).Entity;
                await dbContext.SaveChangesAsync();

                return _mapper.Map<SpellDeckModel>(createdSpellDeckEntity);
            }
        }

    }
}