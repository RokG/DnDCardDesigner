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
    public class DatabaseItemDeckProvider : IItemDeckProvider
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
        public DatabaseItemDeckProvider(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemDeckModel>> GetAllItemDecks()
        {
            using (CardDesignerDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ItemDeck> itemDeckEntities = await
                    context.ItemDecks
                    .Include(sd=>sd.Characters)
                    .Include(sd=>sd.ItemCards)
                    .ToListAsync();

                return itemDeckEntities.Select(c => _mapper.Map<ItemDeckModel>(c));
            }
        }
    }
}