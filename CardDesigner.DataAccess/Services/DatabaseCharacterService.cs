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
                // Get matching entity
                CharacterEntity characterEntity = dbContext.Characters
                    .Include(c => c.SpellDeckDescriptors)
                    .Include(c => c.ItemDeckDescriptors)
                    .Include(c => c.DeckBackgroundDesign)
                    .Include(c => c.Classes)
                    .Single(c => c.ID == characterModel.ID);

                // Update attributes and caster stats
                characterEntity.Attributes = _mapper.Map<CharacterAttributesEntity>(characterModel.Attributes);
                characterEntity.CasterStats = _mapper.Map<CasterStatsEntity>(characterModel.CasterStats);

                // Add classes
                foreach (CharacterClassModel characterClassModel in characterModel.Classes)
                {
                    CharacterClassEntity matchingEntity = characterEntity.Classes.FirstOrDefault(c => c.ClassID == characterClassModel.ClassID);
                    //If any class is new, add it to the list
                    if (matchingEntity == null)
                    {
                        CharacterClassEntity characterClassEntity = _mapper.Map<CharacterClassEntity>(characterClassModel);
                        characterEntity.Classes.Add(characterClassEntity);
                    }
                    // If any class exists, update it
                    else
                    {
                        matchingEntity.Level = characterClassModel.Level;
                    }
                }

                // Remove Spell Decks
                CharacterClassEntity removedClassEntity = characterEntity.Classes
                    .FirstOrDefault(p => characterModel.Classes.All(p2 => p2.ClassID != p.ClassID));
                if (removedClassEntity != null)
                {
                    characterEntity.Classes.Remove(removedClassEntity);
                    dbContext.CharacterClasses.Remove(removedClassEntity);
                }

                // Add Spell Decks
                foreach (SpellDeckDesignLinkerModel spellDeckDesignLinkerModel in characterModel.SpellDeckDescriptors)
                {
                    SpellDeckDesignLinkerEntity matchingEntity = characterEntity.SpellDeckDescriptors
                        .FirstOrDefault(c => c.Character.ID == characterModel.ID && c.SpellDeckID == spellDeckDesignLinkerModel.SpellDeckID);
                    //If any class is new, add it to the list
                    if (matchingEntity == null)
                    {
                        SpellDeckDesignLinkerEntity characterClassEntity = _mapper.Map<SpellDeckDesignLinkerEntity>(spellDeckDesignLinkerModel);
                        characterEntity.SpellDeckDescriptors.Add(characterClassEntity);
                    }
                    // If any class exists, update it
                    else
                    {
                        matchingEntity.DesignID = spellDeckDesignLinkerModel.DesignID;
                    }
                }

                // Remove Spell Decks
                SpellDeckDesignLinkerEntity removedSpellDeckEntity = characterEntity.SpellDeckDescriptors
                    .FirstOrDefault(p => characterModel.SpellDeckDescriptors.All(p2 => p2.SpellDeckID != p.SpellDeckID));
                if (removedSpellDeckEntity != null)
                {
                    characterEntity.SpellDeckDescriptors.Remove(removedSpellDeckEntity);
                    dbContext.SpellDeckDesignLinkers.Remove(removedSpellDeckEntity);
                }

                // Add Item Decks
                foreach (ItemDeckDesignLinkerModel itemDeckDesignLinkerModel in characterModel.ItemDeckDescriptors)
                {
                    ItemDeckDesignLinkerEntity matchingEntity = characterEntity.ItemDeckDescriptors
                        .FirstOrDefault(c => c.Character.ID == characterModel.ID && c.ItemDeckID == itemDeckDesignLinkerModel.ItemDeckID);
                    //If any class is new, add it to the list
                    if (matchingEntity == null)
                    {
                        ItemDeckDesignLinkerEntity characterClassEntity = _mapper.Map<ItemDeckDesignLinkerEntity>(itemDeckDesignLinkerModel);
                        characterEntity.ItemDeckDescriptors.Add(characterClassEntity);
                    }
                    // If any class exists, update it
                    else
                    {
                        matchingEntity.DesignID = itemDeckDesignLinkerModel.DesignID;
                    }
                }

                // Remove Item Decks
                ItemDeckDesignLinkerEntity removedItemDeckEntity = characterEntity.ItemDeckDescriptors
                    .FirstOrDefault(p => characterModel.ItemDeckDescriptors.All(p2 => p2.ItemDeckID != p.ItemDeckID));
                if (removedItemDeckEntity != null)
                {
                    characterEntity.ItemDeckDescriptors.Remove(removedItemDeckEntity);
                    dbContext.ItemDeckDesignLinkers.Remove(removedItemDeckEntity);
                }

                CharacterEntity createdItemCardEntity = dbContext.Characters.Update(characterEntity).Entity;

                await dbContext.SaveChangesAsync();

                return _mapper.Map<CharacterModel>(createdItemCardEntity);
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
                    .Include(c => c.DeckBackgroundDesign)
                    .Include(c => c.Classes)
                    .Include(c => c.CasterStats)
                    .Include(c => c.Attributes)
                    .ToListAsync();

                return characterEntities.Select(c => _mapper.Map<CharacterModel>(c));
            }
        }
    }
}
