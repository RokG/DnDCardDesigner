using AutoMapper;
using CardDesigner.DataAccess.DbContexts;
using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardDesigner.DataAccess.Services
{
    public class DatabaseSpellDeckProvider : ISpellDeckProvider
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
        public DatabaseSpellDeckProvider(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SpellDeckModel>> GetAllSpellDecks()
        {
            using (CardDesignerDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<SpellDeck> spellDeckEntities = await
                    context.SpellDecks
                    .Include(sd=>sd.Characters)
                    .Include(sd=>sd.SpellCards)
                    .ToListAsync();

                return spellDeckEntities.Select(c => _mapper.Map<SpellDeckModel>(c));
            }
        }
    }
}