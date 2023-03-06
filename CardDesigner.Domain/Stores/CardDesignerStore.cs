using CardDesigner.Domain.Enums;
using CardDesigner.Domain.HelperModels;
using CardDesigner.Domain.Interfaces;
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
        #region Private fields

        private readonly Lazy<Task> _initializeLazy;

        // Services
        private readonly ICharacterService _characterService;
        private readonly IMinionService _minionService;
        private readonly ICardDesignService _cardDesignService;
        private readonly IDeckService _deckService;
        private readonly ICardService _cardService;
        private readonly IJsonFileItemService _jsonFileItemService;

        // Character
        private readonly List<CharacterModel> _characters;
        private readonly List<DeckBackgroundDesignModel> _deckBackgroundDesigns;

        // Minions
        private readonly List<MinionModel> _minions;

        // Spell cards
        private readonly List<SpellCardModel> _spellCards;
        private readonly List<SpellDeckModel> _spellDecks;
        private readonly List<SpellDeckDesignModel> _spellDeckDesigns;

        // Item cards
        private readonly List<ItemCardModel> _itemCards;
        private readonly List<ItemDeckModel> _itemDecks;
        private readonly List<ItemDeckDesignModel> _itemDeckDesigns;

        // Character cards
        private readonly List<CharacterCardModel> _characterCards;
        private readonly List<CharacterDeckModel> _characterDecks;
        private readonly List<CharacterDeckDesignModel> _characterDeckDesigns;

        // Minion cards
        private readonly List<MinionCardModel> _minionCards;
        private readonly List<MinionDeckModel> _minionDecks;
        private readonly List<MinionDeckDesignModel> _minionDeckDesigns;

        // Other
        private readonly List<WeaponModel> _weapons;
        private readonly List<ArmourModel> _armours;
        private readonly List<ClassModel> _classes;
        private readonly List<UsableModel> _usables;
        private readonly List<ClothingModel> _clothings;
        private readonly List<ConsumableModel> _consumables;

        #endregion

        #region Properties

        // Character
        public IEnumerable<CharacterModel> Characters => _characters;
        public IEnumerable<DeckBackgroundDesignModel> DeckBackgroundDesigns => _deckBackgroundDesigns;

        // Minion
        public IEnumerable<MinionModel> Minions => _minions;

        // Spell cards
        public IEnumerable<SpellCardModel> SpellCards => _spellCards;
        public IEnumerable<SpellDeckModel> SpellDecks => _spellDecks;
        public IEnumerable<SpellDeckDesignModel> SpellDeckDesigns => _spellDeckDesigns;

        // Item cards
        public IEnumerable<ItemCardModel> ItemCards => _itemCards;
        public IEnumerable<ItemDeckModel> ItemDecks => _itemDecks;
        public IEnumerable<ItemDeckDesignModel> ItemDeckDesigns => _itemDeckDesigns;

        // Character cards
        public IEnumerable<CharacterCardModel> CharacterCards => _characterCards;
        public IEnumerable<CharacterDeckModel> CharacterDecks => _characterDecks;
        public IEnumerable<CharacterDeckDesignModel> CharacterDeckDesigns => _characterDeckDesigns;

        // Minion cards
        public IEnumerable<MinionCardModel> MinionCards => _minionCards;
        public IEnumerable<MinionDeckModel> MinionDecks => _minionDecks;
        public IEnumerable<MinionDeckDesignModel> MinionDeckDesigns => _minionDeckDesigns;

        // Other
        public IEnumerable<WeaponModel> Weapons => _weapons;
        public IEnumerable<ArmourModel> Armours => _armours;
        public IEnumerable<ClassModel> Classes => _classes;
        public IEnumerable<UsableModel> Usables => _usables;
        public IEnumerable<ClothingModel> Clothings => _clothings;
        public IEnumerable<ConsumableModel> Consumables => _consumables;

        #endregion

        #region Events

        public event Action<CharacterModel, DataChangeType> CharacterChanged;
        public event Action<DeckBackgroundDesignModel, DataChangeType> DeckBackgroundDesignChanged;

        public event Action<MinionModel, DataChangeType> MinionChanged;

        public event Action<SpellCardModel, DataChangeType> SpellCardChanged;
        public event Action<SpellDeckModel, DataChangeType> SpellDeckChanged;
        public event Action<SpellDeckDesignModel, DataChangeType> SpellDeckDesignChanged;

        public event Action<ItemCardModel, DataChangeType> ItemCardChanged;
        public event Action<ItemDeckModel, DataChangeType> ItemDeckChanged;
        public event Action<ItemDeckDesignModel, DataChangeType> ItemDeckDesignChanged;

        public event Action<MinionCardModel, DataChangeType> MinionCardChanged;
        public event Action<MinionDeckModel, DataChangeType> MinionDeckChanged;
        public event Action<MinionDeckDesignModel, DataChangeType> MinionDeckDesignChanged;

        public event Action<CharacterCardModel, DataChangeType> CharacterCardChanged;
        public event Action<CharacterDeckModel, DataChangeType> CharacterDeckChanged;
        public event Action<CharacterDeckDesignModel, DataChangeType> CharacterDeckDesignChanged;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public CardDesignerStore(
            ICharacterService characterService,
            IMinionService minionService,
            ICardDesignService cardDesignService,
            IDeckService deckService,
            ICardService cardService,
            IJsonFileItemService jsonFileItemService)
        {
            _characterService = characterService;
            _minionService = minionService;
            _cardDesignService = cardDesignService;
            _cardService = cardService;
            _deckService = deckService;

            _initializeLazy = new Lazy<Task>(Initialize);

            _characters = new();
            _deckBackgroundDesigns = new();

            _minions = new();

            _spellCards = new();
            _spellDecks = new();
            _spellDeckDesigns = new();

            _itemCards = new();
            _itemDecks = new();
            _itemDeckDesigns = new();

            _characterCards = new();
            _characterDecks = new();
            _characterDeckDesigns = new();

            _minionCards = new();
            _minionDecks = new();
            _minionDeckDesigns = new();

            _armours = new();
            _weapons = new();
            _classes = new();
            _usables = new();
            _clothings = new();
            _consumables = new();
            _jsonFileItemService = jsonFileItemService;
        }

        /// <summary>
        /// Initialize store method
        /// </summary>
        /// <returns></returns>
        private async Task Initialize()
        {
            ReadAllItems();

            await UpdateCharactersFromDb();
            await UpdateMinionsFromDb();

            await UpdateSpellCardsFromDb();
            await UpdateSpellDecksFromDb();

            await UpdateItemCardsFromDb();
            await UpdateItemDecksFromDb();

            await UpdateCharacterCardsFromDb();
            await UpdateCharacterDecksFromDb();

            await UpdateMinionCardsFromDb();
            await UpdateMinionDecksFromDb();

            await UpdateCardDesignsFromDb();
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
            CharacterChanged?.Invoke(createdCharacter, DataChangeType.Created);
        }

        public async Task CreateMinion(MinionModel minion)
        {
            MinionModel createdMinion = await _minionService.CreateMinion(minion);
            _minions.Add(createdMinion);
            MinionChanged?.Invoke(createdMinion, DataChangeType.Created);
        }

        public async Task CreateCardDesign(ICardDesign cardDesignModel)
        {
            if (await _cardDesignService.CreateCardDesign(cardDesignModel) is ICardDesign createdCardDesign)
            {
                switch (createdCardDesign)
                {
                    case SpellDeckDesignModel spellDeckDesignModel:
                        _spellDeckDesigns.Add(spellDeckDesignModel);
                        SpellDeckDesignChanged?.Invoke(spellDeckDesignModel, DataChangeType.Created);
                        break;
                    case ItemDeckDesignModel ItemDeckDesignModel:
                        _itemDeckDesigns.Add(ItemDeckDesignModel);
                        ItemDeckDesignChanged?.Invoke(ItemDeckDesignModel, DataChangeType.Created);
                        break;
                    case CharacterDeckDesignModel CharacterDeckDesignModel:
                        _characterDeckDesigns.Add(CharacterDeckDesignModel);
                        CharacterDeckDesignChanged?.Invoke(CharacterDeckDesignModel, DataChangeType.Created);
                        break;
                    case MinionDeckDesignModel MinionDeckDesignModel:
                        _minionDeckDesigns.Add(MinionDeckDesignModel);
                        MinionDeckDesignChanged?.Invoke(MinionDeckDesignModel, DataChangeType.Created);
                        break;
                    case DeckBackgroundDesignModel characterDeckDesignModel:
                        _deckBackgroundDesigns.Add(characterDeckDesignModel);
                        DeckBackgroundDesignChanged?.Invoke(characterDeckDesignModel, DataChangeType.Created);
                        break;
                    default:
                        break;
                }
            }
        }

        public async Task CreateSpellDeck(SpellDeckModel spellDeck)
        {
            SpellDeckModel createdSpellDeck = (SpellDeckModel)await _deckService.CreateDeck(spellDeck);
            _spellDecks.Add(createdSpellDeck);
            SpellDeckChanged?.Invoke(createdSpellDeck, DataChangeType.Created);
        }

        public async Task CreateItemDeck(ItemDeckModel itemDeck)
        {
            ItemDeckModel createdItemDeck = (ItemDeckModel)await _deckService.CreateDeck(itemDeck);
            _itemDecks.Add(createdItemDeck);
            ItemDeckChanged?.Invoke(createdItemDeck, DataChangeType.Created);
        }

        public async Task CreateCharacterDeck(CharacterDeckModel characterDeck)
        {
            CharacterDeckModel createdCharacterDeck = (CharacterDeckModel)await _deckService.CreateDeck(characterDeck);
            _characterDecks.Add(createdCharacterDeck);
            CharacterDeckChanged?.Invoke(createdCharacterDeck, DataChangeType.Created);
        }

        public async Task CreateMinionDeck(MinionDeckModel minionDeck)
        {
            MinionDeckModel createdMinionDeck = (MinionDeckModel)await _deckService.CreateDeck(minionDeck);
            _minionDecks.Add(createdMinionDeck);
            MinionDeckChanged?.Invoke(createdMinionDeck, DataChangeType.Created);
        }

        public async Task CreateSpellCard(SpellCardModel spellCard)
        {
            SpellCardModel createdSpellCard = (SpellCardModel)await _cardService.CreateCard(spellCard);
            _spellCards.Add(createdSpellCard);
            SpellCardChanged?.Invoke(createdSpellCard, DataChangeType.Created);
        }

        public async Task CreateItemCard(ItemCardModel itemCard)
        {
            ItemCardModel createdItemCard = (ItemCardModel)await _cardService.CreateCard(itemCard);
            _itemCards.Add(createdItemCard);
            ItemCardChanged?.Invoke(createdItemCard, DataChangeType.Created);
        }

        public async Task CreateCharacterCard(CharacterCardModel characterCard)
        {
            CharacterCardModel createdCharacterCard = (CharacterCardModel)await _cardService.CreateCard(characterCard);
            _characterCards.Add(createdCharacterCard);
            CharacterCardChanged?.Invoke(createdCharacterCard, DataChangeType.Created);
        }

        public async Task CreateMinionCard(MinionCardModel minionCard)
        {
            MinionCardModel createdMinionCard = (MinionCardModel)await _cardService.CreateCard(minionCard);
            _minionCards.Add(createdMinionCard);
            MinionCardChanged?.Invoke(createdMinionCard, DataChangeType.Created);
        }

        #endregion

        #region Update methods

        public async Task UpdateCharacter(CharacterModel character)
        {
            if (await _characterService.UpdateCharacter(character) is CharacterModel updatedCharacter)
            {
                await UpdateCharactersFromDb();
                AssignClassesToCharacter(updatedCharacter);
                CharacterChanged?.Invoke(updatedCharacter, DataChangeType.Updated);
            }
        }

        public async Task UpdateMinion(MinionModel minion)
        {
            if (await _minionService.UpdateMinion(minion) is MinionModel updatedMinion)
            {
                await UpdateMinionsFromDb();
                MinionChanged?.Invoke(updatedMinion, DataChangeType.Updated);
            }
        }

        public async Task UpdateCardDesign(ICardDesign cardDesignModel)
        {
            if (await _cardDesignService.UpdateCardDesign(cardDesignModel) is ICardDesign updatedcardDesignModel)
            {
                await UpdateCardDesignsFromDb();
                switch (updatedcardDesignModel)
                {
                    case SpellDeckDesignModel spellDeckDesignModel:
                        SpellDeckDesignChanged?.Invoke(spellDeckDesignModel, DataChangeType.Updated);
                        break;
                    case ItemDeckDesignModel itemDeckDesignModel:
                        ItemDeckDesignChanged?.Invoke(itemDeckDesignModel, DataChangeType.Updated);
                        break;
                    case CharacterDeckDesignModel characterDeckDesignModel:
                        CharacterDeckDesignChanged?.Invoke(characterDeckDesignModel, DataChangeType.Updated);
                        break;
                    case MinionDeckDesignModel minionDeckDesignModel:
                        MinionDeckDesignChanged?.Invoke(minionDeckDesignModel, DataChangeType.Updated);
                        break;
                    case DeckBackgroundDesignModel characterDeckDesignModel:
                        DeckBackgroundDesignChanged?.Invoke(characterDeckDesignModel, DataChangeType.Updated);
                        break;
                    default:
                        break;
                }
            }
        }

        public async Task UpdateSpellDeck(SpellDeckModel spellDeck)
        {
            if (await _deckService.UpdateDeck(spellDeck) is SpellDeckModel updatedSpellDeck)
            {
                await UpdateSpellDecksFromDb();
                SpellDeckChanged?.Invoke(updatedSpellDeck, DataChangeType.Updated);
            }
        }

        public async Task UpdateItemDeck(ItemDeckModel itemDeck)
        {
            if (await _deckService.UpdateDeck(itemDeck) is ItemDeckModel updatedItemDeck)
            {
                await UpdateItemDecksFromDb();
                ItemDeckChanged?.Invoke(updatedItemDeck, DataChangeType.Updated);
            }
        }

        public async Task UpdateCharacterDeck(CharacterDeckModel characterDeck)
        {
            if (await _deckService.UpdateDeck(characterDeck) is CharacterDeckModel updatedCharacterDeck)
            {
                await UpdateCharacterDecksFromDb();
                CharacterDeckChanged?.Invoke(updatedCharacterDeck, DataChangeType.Updated);
            }
        }

        public async Task UpdateMinionDeck(MinionDeckModel minionDeck)
        {
            if (await _deckService.UpdateDeck(minionDeck) is MinionDeckModel updatedMinionDeck)
            {
                await UpdateMinionDecksFromDb();
                MinionDeckChanged?.Invoke(updatedMinionDeck, DataChangeType.Updated);
            }
        }

        public async Task UpdateSpellCard(SpellCardModel spellCard)
        {
            if ((SpellCardModel)await _cardService.UpdateCard(spellCard) is SpellCardModel updatedSpellCard)
            {
                SpellCardChanged?.Invoke(updatedSpellCard, DataChangeType.Updated);
            }
        }

        public async Task UpdateItemCard(ItemCardModel itemCard)
        {
            if ((ItemCardModel)await _cardService.UpdateCard(itemCard) is ItemCardModel updatedItemCard)
            {
                ItemCardChanged?.Invoke(updatedItemCard, DataChangeType.Updated);
            }
        }

        public async Task UpdateMinionCard(MinionCardModel minionCard)
        {
            if ((MinionCardModel)await _cardService.UpdateCard(minionCard) is MinionCardModel updatedMinionCard)
            {
                MinionCardChanged?.Invoke(updatedMinionCard, DataChangeType.Updated);
            }
        }

        public async Task UpdateCharacterCard(CharacterCardModel characterCard)
        {
            if ((CharacterCardModel)await _cardService.UpdateCard(characterCard) is CharacterCardModel updatedCharacterCard)
            {
                CharacterCardChanged?.Invoke(updatedCharacterCard, DataChangeType.Updated);
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
                CharacterChanged?.Invoke(character, DataChangeType.Deleted);
            }
        }

        public async Task DeleteMinion(MinionModel minion)
        {
            bool success = await _minionService.DeleteMinion(minion);
            if (success)
            {
                _minions.Remove(minion);
                MinionChanged?.Invoke(minion, DataChangeType.Deleted);
            }
        }

        public async Task DeleteCardDesign(ICardDesign cardDesignModel)
        {
            if (await _cardDesignService.DeleteCardDesign(cardDesignModel))
            {
                switch (cardDesignModel)
                {
                    case SpellDeckDesignModel spellDeckDesignModel:
                        _spellDeckDesigns.Remove(spellDeckDesignModel);
                        SpellDeckDesignChanged?.Invoke(spellDeckDesignModel, DataChangeType.Deleted);
                        break;
                    case ItemDeckDesignModel itemDeckDesignModel:
                        _itemDeckDesigns.Remove(itemDeckDesignModel);
                        ItemDeckDesignChanged?.Invoke(itemDeckDesignModel, DataChangeType.Deleted);
                        break;
                    case CharacterDeckDesignModel characterDeckDesignModel:
                        _characterDeckDesigns.Remove(characterDeckDesignModel);
                        CharacterDeckDesignChanged?.Invoke(characterDeckDesignModel, DataChangeType.Deleted);
                        break;
                    case MinionDeckDesignModel minionDeckDesignModel:
                        _minionDeckDesigns.Remove(minionDeckDesignModel);
                        MinionDeckDesignChanged?.Invoke(minionDeckDesignModel, DataChangeType.Deleted);
                        break;
                    case DeckBackgroundDesignModel characterDeckDesignModel:
                        _deckBackgroundDesigns.Remove(characterDeckDesignModel);
                        DeckBackgroundDesignChanged?.Invoke(characterDeckDesignModel, DataChangeType.Deleted);
                        break;
                    default:
                        break;
                }
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
                bool success = await _deckService.DeleteDeck(a.First());
                if (success)
                {
                    _spellDecks.Remove(spellDeck);
                    SpellDeckChanged?.Invoke(spellDeck, DataChangeType.Deleted);
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
                bool success = await _deckService.DeleteDeck(a.First());
                if (success)
                {
                    _itemDecks.Remove(itemDeck);
                    ItemDeckChanged?.Invoke(itemDeck, DataChangeType.Deleted);
                }
            }
        }

        public async Task DeleteCharacterDeck(CharacterDeckModel characterDeck)
        {
            // Update data from database
            await UpdateCharacterDecksFromDb();
            // Find the deck to delete
            IEnumerable<CharacterDeckModel> a = CharacterDecks.Where(sd => sd.ID == characterDeck.ID);
            // Remove it from database if found
            if (a.Any())
            {
                bool success = await _deckService.DeleteDeck(a.First());
                if (success)
                {
                    _characterDecks.Remove(characterDeck);
                    CharacterDeckChanged?.Invoke(characterDeck, DataChangeType.Deleted);
                }
            }
        }

        public async Task DeleteMinionDeck(MinionDeckModel minionDeck)
        {
            // Update data from database
            await UpdateMinionDecksFromDb();
            // Find the deck to delete
            IEnumerable<MinionDeckModel> a = MinionDecks.Where(sd => sd.ID == minionDeck.ID);
            // Remove it from database if found
            if (a.Any())
            {
                bool success = await _deckService.DeleteDeck(a.First());
                if (success)
                {
                    _minionDecks.Remove(minionDeck);
                    MinionDeckChanged?.Invoke(minionDeck, DataChangeType.Deleted);
                }
            }
        }

        public async Task DeleteSpellCard(SpellCardModel spellCard)
        {
            bool success = await _cardService.DeleteCard(spellCard);
            if (success)
            {
                _spellCards.Remove(spellCard);
                SpellCardChanged(spellCard, DataChangeType.Deleted);
            }
        }

        public async Task DeleteItemCard(ItemCardModel itemCard)
        {
            bool success = await _cardService.DeleteCard(itemCard);
            if (success)
            {
                _itemCards.Remove(itemCard);
                ItemCardChanged?.Invoke(itemCard, DataChangeType.Deleted);
            }
        }

        public async Task DeleteMinionCard(MinionCardModel minionCard)
        {
            bool success = await _cardService.DeleteCard(minionCard);
            if (success)
            {
                _minionCards.Remove(minionCard);
                MinionCardChanged?.Invoke(minionCard, DataChangeType.Deleted);
            }
        }

        public async Task DeleteCharacterCard(CharacterCardModel characterCard)
        {
            bool success = await _cardService.DeleteCard(characterCard);
            if (success)
            {
                _characterCards.Remove(characterCard);
                CharacterCardChanged?.Invoke(characterCard, DataChangeType.Deleted);
            }
        }

        #endregion

        #region Updates

        private async Task UpdateCharactersFromDb()
        {
            IEnumerable<CharacterModel> characters = await _characterService.GetAllCharacters();
            _characters.Clear();
            _characters.AddRange(characters);
            AssignClassesToAllCharacter(Characters);
        }

        private async Task UpdateMinionsFromDb()
        {
            IEnumerable<MinionModel> minions = await _minionService.GetAllMinions();
            _minions.Clear();
            _minions.AddRange(minions);
        }

        private async Task UpdateSpellCardsFromDb()
        {
            IEnumerable<SpellCardModel> spellCards = await _cardService.GetAllCards<SpellCardModel>();
            _spellCards.Clear();
            _spellCards.AddRange(spellCards);
        }

        private async Task UpdateSpellDecksFromDb()
        {
            IEnumerable<SpellDeckModel> spellDecks = await _deckService.GetAllDecks<SpellDeckModel>();
            _spellDecks.Clear();
            _spellDecks.AddRange(spellDecks);
        }

        private async Task UpdateItemCardsFromDb()
        {
            IEnumerable<ItemCardModel> itemCards = await _cardService.GetAllCards<ItemCardModel>();
            _itemCards.Clear();
            _itemCards.AddRange(itemCards);
            AssignItemsToCards(_itemCards);
        }

        private async Task UpdateItemDecksFromDb()
        {
            IEnumerable<ItemDeckModel> itemDecks = await _deckService.GetAllDecks<ItemDeckModel>();
            _itemDecks.Clear();
            _itemDecks.AddRange(itemDecks);
            AssignItemsToItemDecks(_itemDecks);
        }

        private async Task UpdateCharacterCardsFromDb()
        {
            IEnumerable<CharacterCardModel> characterCards = await _cardService.GetAllCards<CharacterCardModel>();
            _characterCards.Clear();
            _characterCards.AddRange(characterCards);
        }

        private async Task UpdateCharacterDecksFromDb()
        {
            IEnumerable<CharacterDeckModel> characterDecks = await _deckService.GetAllDecks<CharacterDeckModel>();
            _characterDecks.Clear();
            _characterDecks.AddRange(characterDecks);
        }

        private async Task UpdateMinionCardsFromDb()
        {
            IEnumerable<MinionCardModel> minionCards = await _cardService.GetAllCards<MinionCardModel>();
            _minionCards.Clear();
            _minionCards.AddRange(minionCards);
        }

        private async Task UpdateMinionDecksFromDb()
        {
            IEnumerable<MinionDeckModel> minionDecks = await _deckService.GetAllDecks<MinionDeckModel>();
            _minionDecks.Clear();
            _minionDecks.AddRange(minionDecks);
        }

        private async Task UpdateCardDesignsFromDb()
        {
            _spellDeckDesigns.Clear();
            _spellDeckDesigns.AddRange(await _cardDesignService.GetAllSpellDeckDesigns());
            _itemDeckDesigns.Clear();
            _itemDeckDesigns.AddRange(await _cardDesignService.GetAllItemDeckDesigns());
            _characterDeckDesigns.Clear();
            _characterDeckDesigns.AddRange(await _cardDesignService.GetAllCharacterDeckDesigns());
            _minionDeckDesigns.Clear();
            _minionDeckDesigns.AddRange(await _cardDesignService.GetAllMinionDeckDesigns());
            _deckBackgroundDesigns.Clear();
            _deckBackgroundDesigns.AddRange(await _cardDesignService.GetAllBackgroundDeckDesigns());
        }

        #endregion

        #region JsonFIleReader

        private void ReadAllItems()
        {
            _armours.Clear();
            _armours.AddRange(_jsonFileItemService.LoadArmours(@".\Resources\Configs\Armour\ChestArmours.json"));
            _armours.AddRange(_jsonFileItemService.LoadArmours(@".\Resources\Configs\Armour\HeadArmours.json"));
            _armours.AddRange(_jsonFileItemService.LoadArmours(@".\Resources\Configs\Armour\LegArmours.json"));
            _armours.AddRange(_jsonFileItemService.LoadArmours(@".\Resources\Configs\Armour\Shields.json"));

            _weapons.Clear();
            _weapons.AddRange(_jsonFileItemService.LoadWeapons(@".\Resources\Configs\Weapons\MeleeWeapons.json"));
            _weapons.AddRange(_jsonFileItemService.LoadWeapons(@".\Resources\Configs\Weapons\RangedWeapons.json"));

            _classes.Clear();
            _classes.AddRange(_jsonFileItemService.LoadClasses(@".\Resources\Configs\Classes\Classes.json"));

            _usables.Clear();
            _usables.AddRange(_jsonFileItemService.LoadUsables(@".\Resources\Configs\Usables\Gems.json"));
            _usables.AddRange(_jsonFileItemService.LoadUsables(@".\Resources\Configs\Usables\Scrolls.json"));
            _usables.AddRange(_jsonFileItemService.LoadUsables(@".\Resources\Configs\Usables\Bombs.json"));

            _clothings.Clear();
            _clothings.AddRange(_jsonFileItemService.LoadClothings(@".\Resources\Configs\Clothing\Capes.json"));

            _consumables.Clear();
            _consumables.AddRange(_jsonFileItemService.LoadConsumables(@".\Resources\Configs\Consumables\Potions.json"));
            _consumables.AddRange(_jsonFileItemService.LoadConsumables(@".\Resources\Configs\Consumables\Drinks.json"));
            _consumables.AddRange(_jsonFileItemService.LoadConsumables(@".\Resources\Configs\Consumables\Food.json"));
        }

        private void AssignItemsToCards(IEnumerable<ItemCardModel> itemCardModels)
        {
            foreach (ItemCardModel itemCard in itemCardModels)
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
                        itemCard.Item = Consumables.FirstOrDefault(a => a.ID == itemCard.ItemID);
                        break;
                    case Enums.ItemType.Usable:
                        itemCard.Item = Usables.FirstOrDefault(a => a.ID == itemCard.ItemID);
                        break;
                    case Enums.ItemType.Clothing:
                        itemCard.Item = Clothings.FirstOrDefault(a => a.ID == itemCard.ItemID);
                        break;
                    default:
                        break;
                }
            }
        }

        private void AssignItemsToItemDecks(IEnumerable<ItemDeckModel> itemDecks)
        {
            foreach (ItemDeckModel itemDeck in itemDecks)
            {
                AssignItemsToCards(itemDeck.Cards);
            }
        }

        private void AssignClassesToAllCharacter(IEnumerable<CharacterModel> characterModels)
        {
            foreach (CharacterModel characterModel in characterModels)
            {
                AssignClassesToCharacter(characterModel);
            }
        }

        private void AssignClassesToCharacter(CharacterModel characterModel)
        {
            foreach (CharacterClassModel characterClassModel in characterModel.Classes)
            {
                characterClassModel.Class = Classes.First(c => c.ID == characterClassModel.ClassID);
            }
        }

        #endregion
    }
}