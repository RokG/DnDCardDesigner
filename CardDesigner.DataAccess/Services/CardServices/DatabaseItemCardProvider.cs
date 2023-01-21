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
    public class DatabaseItemCardProvider : IItemCardProvider
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
        public DatabaseItemCardProvider(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all characters from database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ItemCardModel>> GetAllItemCards()
        {
            using (CardDesignerDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ItemCard> characterEntities = await 
                    context.ItemCards
                    .Include(sc=>sc.ItemDecks)
                    .ToListAsync();

                return characterEntities.Select(c => _mapper.Map<ItemCardModel>(c));
            }
        }
    }
}