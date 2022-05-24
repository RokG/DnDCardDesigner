using AutoMapper;
using CardDesigner.DataAccess.DbContexts;
using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using System.Threading.Tasks;

namespace CardDesigner.DataAccess.Services
{
    public class DatabaseSpellCardCreator : ISpellCardCreator
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
        public DatabaseSpellCardCreator(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all characters from database
        /// </summary>
        /// <returns></returns>
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