using AutoMapper;
using CardDesigner.DataAccess.DbContexts;
using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
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

        public Task<bool> UpdateSpellDeck(SpellDeckModel spellDeck)
        {
            throw new System.NotImplementedException();
        }
    }
}