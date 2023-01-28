using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CardDesigner.Domain.Stores
{
    public class CardDesignerStore
    {
        private readonly Lazy<Task> _initializeLazy;

        private readonly ICharacterService _characterService;
        private readonly ICardDesignService _cardDesignService;
        private readonly ISpellDeckService _spellDeckService;
        private readonly ISpellCardService _spellCardService;
        private readonly IItemCardService _itemCardService;
        private readonly IItemDeckService _itemDeckService;
        private readonly IJsonFileItemService _jsonFileItemService;

        private readonly List<SpellCardModel> _spellCards;
        private readonly List<ItemCardModel> _itemCards;
        private readonly List<SpellDeckModel> _spellDecks;
        private readonly List<ItemDeckModel> _itemDecks;
        private readonly List<CharacterModel> _characters;
        private readonly List<CardDesignModel> _cardDesigns;
        private readonly List<WeaponModel> _weapons;
        private readonly List<ArmourModel> _armours;

        public IEnumerable<SpellCardModel> SpellCards => _spellCards;
        public IEnumerable<ItemCardModel> ItemCards => _itemCards;
        public IEnumerable<SpellDeckModel> SpellDecks => _spellDecks;
        public IEnumerable<ItemDeckModel> ItemDecks => _itemDecks;
        public IEnumerable<CharacterModel> Characters => _characters;
        public IEnumerable<CardDesignModel> CardDesigns => _cardDesigns;
        public IEnumerable<WeaponModel> Weapons => _weapons;
        public IEnumerable<ArmourModel> Armours => _armours;

        public event Action<CharacterModel> CharacterCreated;
        public event Action<CharacterModel> CharacterUpdated;
        public event Action<CharacterModel> CharacterDeleted;

        public event Action<CardDesignModel> CardDesignCreated;
        public event Action<CardDesignModel> CardDesignUpdated;
        public event Action<CardDesignModel> CardDesignDeleted;

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
        /// <param name="characterService"></param>
        /// <param name="characterService"></param>
        /// <param name="spellCardService"></param>
        /// <param name="spellCardService"></param>
        public CardDesignerStore(
            ICharacterService characterService,
            ICardDesignService cardDesignService,
            ISpellDeckService spellDeckService,
            ISpellCardService spellCardService,
            IItemCardService itemCardService,
            IItemDeckService itemDeckService,
            IJsonFileItemService jsonFileItemService)
        {
            _characterService = characterService;
            _cardDesignService = cardDesignService;
            _spellDeckService = spellDeckService;
            _spellCardService = spellCardService;
            _itemCardService = itemCardService;
            _itemDeckService = itemDeckService;

            _initializeLazy = new Lazy<Task>(Initialize);

            _characters = new();
            _cardDesigns = new();
            _itemCards = new();
            _spellCards = new();
            _spellDecks = new();
            _itemDecks = new();
            _armours = new();
            _weapons = new();
            _jsonFileItemService = jsonFileItemService;
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
            await UpdateCardDesignsFromDb();
            await UpdateSpellDecksFromDb();
            await UpdateItemDecksFromDb();
            ReadAllItems();
            AssignItemsToCards();
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
            CharacterModel createdCharacter = await _characterService.CreateCharacter(character);
            _characters.Add(createdCharacter);
            OnCharacterCreated(createdCharacter);
        }
        public async Task CreateCardDesign(CardDesignModel cardDesignModel)
        {
            CardDesignModel createdCardDesign = await _cardDesignService.CreateCardDesign(cardDesignModel);
            _cardDesigns.Add(createdCardDesign);
            OnCardDesignCreated(createdCardDesign);
        }

        public async Task CreateSpellDeck(SpellDeckModel spellDeck)
        {
            SpellDeckModel createdSpellDeck = await _spellDeckService.CreateSpellDeck(spellDeck);
            _spellDecks.Add(createdSpellDeck);
            OnSpellDeckCreated(createdSpellDeck);
        }

        public async Task CreateItemDeck(ItemDeckModel itemDeck)
        {
            ItemDeckModel createdItemDeck = await _itemDeckService.CreateItemDeck(itemDeck);
            _itemDecks.Add(createdItemDeck);
            OnItemDeckCreated(createdItemDeck);
        }

        public async Task CreateSpellCard(SpellCardModel spellCard)
        {
            SpellCardModel createdSpellCard = await _spellCardService.CreateSpellCard(spellCard);
            _spellCards.Add(createdSpellCard);
            OnSpellCardCreated(createdSpellCard);
        }
        public async Task CreateItemCard(ItemCardModel itemCard)
        {
            ItemCardModel createdItemCard = await _itemCardService.CreateItemCard(itemCard);
            _itemCards.Add(createdItemCard);
            OnItemCardCreated(createdItemCard);
        }

        #endregion

        #region Update methods

        public async Task UpdateCharacter(CharacterModel character)
        {
            if (await _characterService.UpdateCharacter(character) is CharacterModel updatedCharacter)
            {
                await UpdateCharactersFromDb();
                OnCharacterUpdated(updatedCharacter);
            }
        }
        public async Task UpdateCardDesign(CardDesignModel cardDesignModel)
        {
            if (await _cardDesignService.UpdateCardDesign(cardDesignModel) is CardDesignModel updatedcardDesignModel)
            {
                await UpdateCardDesignsFromDb();
                OnCardDesignUpdated(updatedcardDesignModel);
            }
        }
        public async Task UpdateSpellDeck(SpellDeckModel spellDeck)
        {
            if (await _spellDeckService.UpdateSpellDeck(spellDeck) is SpellDeckModel updatedSpellDeck)
            {
                await UpdateSpellDecksFromDb();
                OnSpellDeckUpdated(updatedSpellDeck);
            }
        }
        public async Task UpdateItemDeck(ItemDeckModel itemDeck)
        {
            if (await _itemDeckService.UpdateItemDeck(itemDeck) is ItemDeckModel updatedItemDeck)
            {
                await UpdateSpellDecksFromDb();
                OnItemDeckUpdated(updatedItemDeck);
            }
        }

        public async Task UpdateSpellCard(SpellCardModel spellCard)
        {
            if (await _spellCardService.UpdateSpellCard(spellCard) is SpellCardModel updatedSpellCard)
            {
                OnSpellCardUpdated(updatedSpellCard);
            }
        }

        public async Task UpdateItemCard(ItemCardModel itemCard)
        {
            if (await _itemCardService.UpdateItemCard(itemCard) is ItemCardModel updatedItemCard)
            {
                OnItemCardUpdated(updatedItemCard);
            }
        }

        #endregion

        #region Delete methods

        public async Task DeleteCharacter(CharacterModel character)
        {
            bool success = await _characterService.DeleteCharacter(character);
            if (success)
            {
                _characters.Remove(character);
                OnCharacterDeleted(character);
            }
        }

        public async Task DeleteCardDesign(CardDesignModel cardDesignModel)
        {
            bool success = await _cardDesignService.DeleteCardDesign(cardDesignModel);
            if (success)
            {
                _cardDesigns.Remove(cardDesignModel);
                OnCardDesignDeleted(cardDesignModel);
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
                bool success = await _spellDeckService.DeleteSpellDeck(a.First());
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
                bool success = await _itemDeckService.DeleteItemDeck(a.First());
                if (success)
                {
                    _itemDecks.Remove(itemDeck);
                    OnItemDeckDeleted(itemDeck);
                }
            }
        }

        public async Task DeleteSpellCard(SpellCardModel spellCard)
        {
            bool success = await _spellCardService.DeleteSpellCard(spellCard);
            if (success)
            {
                _spellCards.Remove(spellCard);
                OnSpellCardDeleted(spellCard);
            }
        }
        public async Task DeleteItemCard(ItemCardModel itemCard)
        {
            bool success = await _itemCardService.DeleteItemCard(itemCard);
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

        private void OnCardDesignDeleted(CardDesignModel cardDesign)
        {
            CardDesignDeleted?.Invoke(cardDesign);
        }
        private void OnCardDesignUpdated(CardDesignModel cardDesign)
        {
            CardDesignUpdated?.Invoke(cardDesign);
        }
        private void OnCardDesignCreated(CardDesignModel cardDesign)
        {
            CardDesignCreated?.Invoke(cardDesign);
        }

        #endregion

        #region Updates

        private async Task UpdateCharactersFromDb()
        {
            IEnumerable<CharacterModel> characters = await _characterService.GetAllCharacters();
            _characters.Clear();
            _characters.AddRange(characters);
        }

        private async Task UpdateItemCardsFromDb()
        {
            IEnumerable<ItemCardModel> itemCards = await _itemCardService.GetAllItemCards();
            _itemCards.Clear();
            _itemCards.AddRange(itemCards);
        }

        private async Task UpdateSpellDecksFromDb()
        {
            IEnumerable<SpellDeckModel> spellDecks = await _spellDeckService.GetAllSpellDecks();
            _spellDecks.Clear();
            _spellDecks.AddRange(spellDecks);
        }

        private async Task UpdateCardDesignsFromDb()
        {
            IEnumerable<CardDesignModel> cardDesigns = await _cardDesignService.GetAllCardDesigns();
            _cardDesigns.Clear();
            _cardDesigns.AddRange(cardDesigns);
        }

        private async Task UpdateSpellCardsFromDb()
        {
            IEnumerable<SpellCardModel> spellCards = await _spellCardService.GetAllSpellCards();
            _spellCards.Clear();
            _spellCards.AddRange(spellCards);
        }

        private async Task UpdateItemDecksFromDb()
        {
            IEnumerable<ItemDeckModel> itemDecks = await _itemDeckService.GetAllItemDecks();
            _itemDecks.Clear();
            _itemDecks.AddRange(itemDecks);
        }

        #endregion

        #region JsonFIleReader

        private void ReadAllItems()
        {
            _armours.Clear();
            _armours.AddRange(_jsonFileItemService.LoadArmours(@".\Resources\Items\Armour\ChestArmours.json"));
            _armours.AddRange(_jsonFileItemService.LoadArmours(@".\Resources\Items\Armour\HeadArmours.json"));
            _armours.AddRange(_jsonFileItemService.LoadArmours(@".\Resources\Items\Armour\LegArmours.json"));
            _armours.AddRange(_jsonFileItemService.LoadArmours(@".\Resources\Items\Armour\Shields.json"));

            _weapons.Clear();
            _weapons.AddRange(_jsonFileItemService.LoadWeapons(@".\Resources\Items\Weapons\MeleeWeapons.json"));
            _weapons.AddRange(_jsonFileItemService.LoadWeapons(@".\Resources\Items\Weapons\RangedWeapons.json"));
        }

        private void AssignItemsToCards()
        {
            foreach (ItemCardModel itemCard in ItemCards)
            {
                switch (itemCard.Type)
                {
                    case Enums.ItemType.Armour:
                        itemCard.Item = Armours.FirstOrDefault(a => a.ID == itemCard.ItemID);
                        break;
                    case Enums.ItemType.Weapon:
                        itemCard.Item = Weapons.FirstOrDefault(a => a.ID == itemCard.ItemID);
                        break;
                    case Enums.ItemType.Consumable:
                        break;
                    case Enums.ItemType.Usable:
                        break;
                    case Enums.ItemType.Cloathing:
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion
    }
}