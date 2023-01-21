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
                Character characterEntity = _mapper.Map<Character>(character);

                Character createdCharacterEntity = dbContext.Characters.Add(characterEntity).Entity;
                await dbContext.SaveChangesAsync();

                return _mapper.Map<CharacterModel>(createdCharacterEntity);
            }
        }

        public async Task<CharacterModel> UpdateCharacter(CharacterModel character)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                try
                {
                    // Get character from database
                    Character characterEntity = dbContext.Characters
                        .Include(d => d.SpellDeck)
                        .Single(d => d.ID == character.ID);

                    // Get spell deck from database
                    SpellDeck deckEntity = dbContext.SpellDecks.Single(c => c.Name == character.SpellDeck.Name);
                    characterEntity.SpellDeck = deckEntity;

                    // Update database
                    if (dbContext.Characters.Contains(characterEntity))
                    {
                        dbContext.Characters.Update(characterEntity);
                        await dbContext.SaveChangesAsync();
                        return _mapper.Map<CharacterModel>(characterEntity); ;
                    }
                    return null;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<bool> DeleteCharacter(CharacterModel character)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                try
                {
                    Character characterEntity = _mapper.Map<Character>(character);
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
                IEnumerable<Character> characterEntities = await
                    context.Characters
                    .Include(c => c.SpellDeck)
                    .Include(d => d.SpellDeck.SpellCards)
                    .ToListAsync();

                //IEnumerable<Character> characterEntities = await
                //   context.Characters.ToListAsync();

                return characterEntities.Select(c => _mapper.Map<CharacterModel>(c));
            }
        }
    }
}
