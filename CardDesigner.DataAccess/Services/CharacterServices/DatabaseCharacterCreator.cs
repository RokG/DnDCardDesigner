using AutoMapper;
using CardDesigner.DataAccess.DbContexts;
using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using System.Threading.Tasks;

namespace CardDesigner.DataAccess.Services
{
    public class DatabaseCharacterCreator : ICharacterCreator
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
        public DatabaseCharacterCreator(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all characters from database
        /// </summary>
        /// <returns></returns>
        public async Task<CharacterModel> CreateCharacter(CharacterModel character)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                Character characterEntity = _mapper.Map<Character>(character);

                Character createdClient = dbContext.Characters.Add(characterEntity).Entity;
                await dbContext.SaveChangesAsync();

                return _mapper.Map<CharacterModel>(createdClient);
            }
        }
    }
}