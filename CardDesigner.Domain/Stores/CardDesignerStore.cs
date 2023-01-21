using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Stores
{
    public class CardDesignerStore
    {
        private readonly Lazy<Task> _initializeLazy;

        private readonly ICharacterCreator _characterCreator;
        private readonly ICharacterProvider _characterProvider;
        private readonly ICharacterUpdater _characterUpdater;
        private readonly ICharacterDeleter _characterDeleter;

        private readonly ISpellDeckCreator _spellDeckCreator;
        private readonly ISpellDeckUpdater _spellDeckUpdater;
        private readonly ISpellDeckDeleter _spellDeckDeleter;
        private readonly ISpellDeckProvider _spellDeckProvider;

        private readonly ISpellCardCreator _spellCardCreator;
        private readonly ISpellCardUpdater _spellCardUpdater;
        private readonly ISpellCardDeleter _spellCardDeleter;
        private readonly ISpellCardProvider _spellCardProvider;

        private readonly IItemCardCreator  _itemCardCreator;
        private readonly IItemCardUpdater  _itemCardUpdater;
        private readonly IItemCardDeleter  _itemCardDeleter;
        private readonly IItemCardProvider _itemCardProvider;

        private readonly IItemDeckCreator  _itemDeckCreator;
        private readonly IItemDeckUpdater  _itemDeckUpdater;
        private readonly IItemDeckDeleter  _itemDeckDeleter;
        private readonly IItemDeckProvider _itemDeckProvider;

        private readonly List<SpellCardModel> _spellCards;
        private readonly List<ItemCardModel> _itemCards;
        private readonly List<SpellDeckModel> _spellDecks;
        private readonly List<ItemDeckModel> _itemDecks;
        private readonly List<CharacterModel> _characters;

        public IEnumerable<SpellCardModel> SpellCards => _spellCards;
        public IEnumerable<ItemCardModel> ItemCards => _itemCards;
        public IEnumerable<SpellDeckModel> SpellDecks => _spellDecks;
        public IEnumerable<ItemDeckModel> ItemDecks => _itemDecks;
        public IEnumerable<CharacterModel> Characters => _characters;

        public event Action<CharacterModel> CharacterCreated;
        public event Action<CharacterModel> CharacterUpdated;
        public event Action<CharacterModel> CharacterDeleted;

        public event Action<SpellDeckModel> SpellDeckCreated;
        public event Action<SpellDeckModel> SpellDeckUpdated;
        public event Action<SpellDeckModel> SpellDeckDeleted;

        public event Action<SpellCardModel> SpellCardCreated;
        public event Action<SpellCardModel> SpellCardUpdated;
        public event Action<SpellCardModel> SpellCardDeleted;

        public event Action<ItemDeckModel> ItemDeckCreated;
        public event Action<ItemDeckModel> ItemDeckUpdated;
        public event Action<ItemDeckModel> ItemDeckDeleted;

        public event Action<ItemCardModel> ItemCardCreated;
        public event Action<ItemCardModel> ItemCardUpdated;
        public event Action<ItemCardModel> ItemCardDeleted;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="characterCreator"></param>
        /// <param name="characterProvider"></param>
        /// <param name="spellCardCreator"></param>
        /// <param name="spellCardProvider"></param>
        public CardDesignerStore(
            ICharacterCreator characterCreator,
            ICharacterUpdater characterUpdater,
            ICharacterDeleter characterDeleter,
            ICharacterProvider characterProvider,
            IItemCardCreator  itemCardCreator,
            IItemCardUpdater  itemCardUpdater,
            IItemCardDeleter  itemCardDeleter,
            IItemCardProvider itemCardProvider,
            ISpellCardCreator spellCardCreator,
            ISpellCardUpdater spellCardUpdater,
            ISpellCardDeleter spellCardDeleter,
            ISpellCardProvider spellCardProvider,
            ISpellDeckCreator spellDeckCreator,
            ISpellDeckUpdater spellDeckUpdater,
            ISpellDeckDeleter spellDeckDeleter,
            ISpellDeckProvider spellDeckProvider,
            IItemDeckCreator  itemDeckCreator,
            IItemDeckUpdater  itemDeckUpdater,
            IItemDeckDeleter  itemDeckDeleter,
            IItemDeckProvider itemDeckProvider)
        {
            _characterCreator = characterCreator;
            _characterProvider = characterProvider;
            _characterUpdater = characterUpdater;
            _characterDeleter = characterDeleter;
            _spellCardCreator = spellCardCreator;
            _spellCardUpdater = spellCardUpdater;
            _spellCardDeleter = spellCardDeleter;
            _spellCardProvider = spellCardProvider;
            _itemCardCreator = itemCardCreator;
            _itemCardUpdater = itemCardUpdater;
            _itemCardDeleter = itemCardDeleter;
            _itemCardProvider = itemCardProvider;
            _spellDeckCreator = spellDeckCreator;
            _spellDeckUpdater = spellDeckUpdater;
            _spellDeckDeleter = spellDeckDeleter;
            _spellDeckProvider = spellDeckProvider;
            _itemDeckCreator =  itemDeckCreator;
            _itemDeckUpdater =  itemDeckUpdater;
            _itemDeckDeleter =  itemDeckDeleter;
            _itemDeckProvider = itemDeckProvider;

            _initializeLazy = new Lazy<Task>(Initialize);

            _characters = new();
            _itemCards = new();
            _spellCards = new();
            _spellDecks = new();
            _itemDecks = new();
        }

        /// <summary>
        /// Initialize store method
        /// </summary>
        /// <returns></returns>
        private async Task Initialize()
        {
            await UpdateSpellCardsFromDb();
            await UpdateItemCardsFromDb();
            await UpdateCharactersFromDb();
            await UpdateSpellDecksFromDb();
            await UpdateItemDecksFromDb();
        }

        /// <summary>
        /// Load method
        /// </summary>
        /// <returns></returns>
        public async Task Load()
        {
            await _initializeLazy.Value;
        }

        #region Create methods

        public async Task CreateCharacter(CharacterModel character)
        {
            CharacterModel createdCharacter = await _characterCreator.CreateCharacter(character);
            _characters.Add(createdCharacter);
            OnCharacterCreated(createdCharacter);
        }

        public async Task CreateSpellDeck(SpellDeckModel spellDeck)
        {
            SpellDeckModel createdSpellDeck = await _spellDeckCreator.CreateSpellDeck(spellDeck);
            _spellDecks.Add(createdSpellDeck);
            OnSpellDeckCreated(createdSpellDeck);
        }

        public async Task CreateItemDeck(ItemDeckModel itemDeck)
        {
            ItemDeckModel createdItemDeck = await _itemDeckCreator.CreateItemDeck(itemDeck);
            _itemDecks.Add(createdItemDeck);
            OnItemDeckCreated(createdItemDeck);
        }

        public async Task CreateSpellCard(SpellCardModel spellCard)
        {
            SpellCardModel createdSpellCard = await _spellCardCreator.CreateSpellCard(spellCard);
            _spellCards.Add(createdSpellCard);
            OnSpellCardCreated(createdSpellCard);
        }
        public async Task CreateItemCard(ItemCardModel itemCard)
        {
            ItemCardModel createdItemCard = await _itemCardCreator.CreateItemCard(itemCard);
            _itemCards.Add(createdItemCard);
            OnItemCardCreated(createdItemCard);
        }

        #endregion

        #region Update methods

        public async Task UpdateCharacter(CharacterModel character)
        {
            if (await _characterUpdater.UpdateCharacter(character) is CharacterModel updatedCharacter)
            {
                await UpdateCharactersFromDb();
                OnCharacterUpdated(updatedCharacter);
            }
        }

        public async Task UpdateSpellDeck(SpellDeckModel spellDeck)
        {
            if (await _spellDeckUpdater.UpdateSpellDeck(spellDeck) is SpellDeckModel updatedSpellDeck)
            {
                await UpdateSpellDecksFromDb();
                OnSpellDeckUpdated(updatedSpellDeck);
            }
        }
        public async Task UpdateItemDeck(ItemDeckModel itemDeck)
        {
            if (await _itemDeckUpdater.UpdateItemDeck(itemDeck) is ItemDeckModel updatedItemDeck)
            {
                await UpdateSpellDecksFromDb();
                OnItemDeckUpdated(updatedItemDeck);
            }
        }

        public async Task UpdateSpellCard(SpellCardModel spellCard)
        {
            if (await _spellCardUpdater.UpdateSpellCard(spellCard) is SpellCardModel updatedSpellCard)
            {
                OnSpellCardUpdated(updatedSpellCard);
            }
        }

        public async Task UpdateItemCard(ItemCardModel itemCard)
        {
            if (await _itemCardUpdater.UpdateItemCard(itemCard) is ItemCardModel updatedItemCard)
            {
                OnItemCardUpdated(updatedItemCard);
            }
        }

        #endregion

        #region Delete methods

        public async Task DeleteCharacter(CharacterModel character)
        {
            bool success = await _characterDeleter.DeleteCharacter(character);
            if (success)
            {
                _characters.Remove(character);
                OnCharacterDeleted(character);
            }
        }

        public async Task DeleteSpellDeck(SpellDeckModel spellDeck)
        {
            // Update data from database
            await UpdateSpellDecksFromDb();
            // Find the deck to delete
            IEnumerable<SpellDeckModel> a = SpellDecks.Where(sd => sd.ID == spellDeck.ID);
            // Remove it from database if found
            if (a.Any())
            {
                bool success = await _spellDeckDeleter.DeleteSpellDeck(a.First());
                if (success)
                {
                    _spellDecks.Remove(spellDeck);
                    OnSpellDeckDeleted(spellDeck);
                }
            }
        }

        public async Task DeleteItemDeck(ItemDeckModel itemDeck)
        {
            // Update data from database
            await UpdateItemDecksFromDb();
            // Find the deck to delete
            IEnumerable<ItemDeckModel> a = ItemDecks.Where(sd => sd.ID == itemDeck.ID);
            // Remove it from database if found
            if (a.Any())
            {
                bool success = await _itemDeckDeleter.DeleteItemDeck(a.First());
                if (success)
                {
                    _itemDecks.Remove(itemDeck);
                    OnItemDeckDeleted(itemDeck);
                }
            }
        }

        public async Task DeleteSpellCard(SpellCardModel spellCard)
        {
            bool success = await _spellCardDeleter.DeleteSpellCard(spellCard);
            if (success)
            {
                _spellCards.Remove(spellCard);
                OnSpellCardDeleted(spellCard);
            }
        }
        public async Task DeleteItemCard(ItemCardModel itemCard)
        {
            bool success = await _itemCardDeleter.DeleteItemCard(itemCard);
            if (success)
            {
                _itemCards.Remove(itemCard);
                OnItemCardDeleted(itemCard);
            }
        }
        #endregion

        #region Invokers

        private void OnCharacterCreated(CharacterModel character)
        {
            CharacterCreated?.Invoke(character);
        }

        private void OnCharacterUpdated(CharacterModel character)
        {
            CharacterUpdated?.Invoke(character);
        }

        private void OnCharacterDeleted(CharacterModel character)
        {
            CharacterDeleted?.Invoke(character);
        }

        private void OnSpellDeckCreated(SpellDeckModel spellDeck)
        {
            SpellDeckCreated?.Invoke(spellDeck);
        }

        private void OnSpellDeckUpdated(SpellDeckModel spellDeck)
        {
            SpellDeckUpdated?.Invoke(spellDeck);
        }

        private void OnSpellDeckDeleted(SpellDeckModel spellDeck)
        {
            SpellDeckDeleted?.Invoke(spellDeck);
        }

        private void OnItemDeckCreated(ItemDeckModel itemDeck)
        {
            ItemDeckCreated?.Invoke(itemDeck);
        }

        private void OnItemDeckUpdated(ItemDeckModel itemDeck)
        {
            ItemDeckUpdated?.Invoke(itemDeck);
        }

        private void OnItemDeckDeleted(ItemDeckModel itemDeck)
        {
            ItemDeckDeleted?.Invoke(itemDeck);
        }

        private void OnSpellCardCreated(SpellCardModel spellCard)
        {
            SpellCardCreated?.Invoke(spellCard);
        }

        private void OnItemCardCreated(ItemCardModel itemCard)
        {
            ItemCardCreated?.Invoke(itemCard);
        }

        private void OnItemCardDeleted(ItemCardModel itemCard)
        {
            ItemCardDeleted?.Invoke(itemCard);
        }

        private void OnItemCardUpdated(ItemCardModel itemCard)
        {
            ItemCardUpdated?.Invoke(itemCard);
        }

        private void OnSpellCardUpdated(SpellCardModel spellCard)
        {
            SpellCardUpdated?.Invoke(spellCard);
        }

        private void OnSpellCardDeleted(SpellCardModel spellCard)
        {
            SpellCardDeleted?.Invoke(spellCard);
        }

        #endregion

        #region Updates

        private async Task UpdateCharactersFromDb()
        {
            IEnumerable<CharacterModel> characters = await _characterProvider.GetAllCharacters();
            _characters.Clear();
            _characters.AddRange(characters);
        } 
        
        private async Task UpdateItemCardsFromDb()
        {
            IEnumerable<ItemCardModel> itemCards = await _itemCardProvider.GetAllItemCards();
            _itemCards.Clear();
            _itemCards.AddRange(itemCards);
        }

        private async Task UpdateSpellDecksFromDb()
        {
            IEnumerable<SpellDeckModel> spellDecks = await _spellDeckProvider.GetAllSpellDecks();
            _spellDecks.Clear();
            _spellDecks.AddRange(spellDecks);
        }

        private async Task UpdateSpellCardsFromDb()
        {
            IEnumerable<SpellCardModel> spellCards = await _spellCardProvider.GetAllSpellCards();
            _spellCards.Clear();
            _spellCards.AddRange(spellCards);
        }

        private async Task UpdateItemDecksFromDb()
        {
            IEnumerable<ItemDeckModel> itemDecks = await _itemDeckProvider.GetAllItemDecks();
            _itemDecks.Clear();
            _itemDecks.AddRange(itemDecks);
        }

        #endregion

    }
}