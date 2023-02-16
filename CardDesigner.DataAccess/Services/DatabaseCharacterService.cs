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
    public class DatabaseCharacterService : ICharacterService
    {
        private readonly CardDesignerDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public DatabaseCharacterService(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<CharacterModel> CreateCharacter(CharacterModel character)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                CharacterEntity characterEntity = _mapper.Map<CharacterEntity>(character);

                CharacterEntity createdCharacterEntity = dbContext.Characters.Add(characterEntity).Entity;
                await dbContext.SaveChangesAsync();

                return _mapper.Map<CharacterModel>(createdCharacterEntity);
            }
        }

        public async Task<CharacterModel> UpdateCharacter(CharacterModel characterModel)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                CharacterEntity itemCardEntity = _mapper.Map<CharacterEntity>(characterModel);
                CharacterEntity createdItemCardEntity = dbContext.Characters.Update(itemCardEntity).Entity;

                await dbContext.SaveChangesAsync();

                return _mapper.Map<CharacterModel>(createdItemCardEntity);

                // A lot of code here was delted on 15.2. at 23.52
            }
        }

        public async Task<bool> DeleteCharacter(CharacterModel character)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                try
                {
                    CharacterEntity characterEntity = _mapper.Map<CharacterEntity>(character);
                    if (dbContext.Characters.Contains(characterEntity))
                    {
                        dbContext.Characters.Remove(characterEntity);
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

        public async Task<IEnumerable<CharacterModel>> GetAllCharacters()
        {
            using (CardDesignerDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<CharacterEntity> characterEntities = await
                    context.Characters
                    .Include(c => c.SpellDeckDescriptors)
                    .Include(c => c.ItemDeckDescriptors)
                    .Include(c => c.CharacterDeckDescriptors)
                    .Include(c => c.DeckBackgroundDesign)
                    .Include(c => c.Classes)
                    .Include(c => c.CasterStats)
                    .Include(c => c.Abilities)
                    .ToListAsync();

                return characterEntities.Select(c => _mapper.Map<CharacterModel>(c));
            }
        }
    }
}
