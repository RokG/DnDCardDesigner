using AutoMapper;
using CardDesigner.DataAccess.DbContexts;
using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardDesigner.DataAccess.Services
{
    public class DatabaseMinionService : IMinionService
    {
        private readonly CardDesignerDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public DatabaseMinionService(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<MinionModel> CreateMinion(MinionModel minion)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                MinionEntity minionEntity = _mapper.Map<MinionEntity>(minion);

                MinionEntity createdMinionEntity = dbContext.Minions.Add(minionEntity).Entity;
                await dbContext.SaveChangesAsync();

                return _mapper.Map<MinionModel>(createdMinionEntity);
            }
        }

        public async Task<MinionModel> UpdateMinion(MinionModel minionModel)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                MinionEntity minionEntity = _mapper.Map<MinionEntity>(minionModel);
                MinionEntity createdMinionEntity = dbContext.Minions.Update(minionEntity).Entity;

                await dbContext.SaveChangesAsync();

                return _mapper.Map<MinionModel>(createdMinionEntity);
            }
        }

        public async Task<bool> DeleteMinion(MinionModel minion)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                try
                {
                    MinionEntity minionEntity = _mapper.Map<MinionEntity>(minion);
                    if (dbContext.Minions.Contains(minionEntity))
                    {
                        dbContext.Minions.Remove(minionEntity);

                        // Delete minion from minion cards where it exists
                        foreach (var minionCard in dbContext.MinionCards)
                        {
                            if (minionCard.Minion == minionEntity)
                            {
                                minionCard.Minion = null;
                            }
                        }

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

        public async Task<IEnumerable<MinionModel>> GetAllMinions()
        {
            using (CardDesignerDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<MinionEntity> minionEntities = await
                    context.Minions
                    .ToListAsync();

                return minionEntities.Select(c => _mapper.Map<MinionModel>(c));
            }
        }
    }
}
