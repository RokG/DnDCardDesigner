using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using System;
using System.Collections.Generic;
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
        private readonly ISpellCardProvider _spellCardProvider;

        private readonly List<SpellCardModel> _spellCards;
        private readonly List<SpellDeckModel> _spellDecks;
        private readonly List<CharacterModel> _characters;

        public IEnumerable<SpellCardModel> SpellCards => _spellCards;
        public IEnumerable<SpellDeckModel> SpellDecks => _spellDecks;
        public IEnumerable<CharacterModel> Characters => _characters;

        public event Action<CharacterModel> CharacterCreated;
        public event Action<CharacterModel> CharacterUpdated;
        public event Action<CharacterModel> CharacterDeleted;

        public event Action<SpellDeckModel> SpellDeckCreated;
        public event Action<SpellDeckModel> SpellDeckUpdated;
        public event Action<SpellDeckModel> SpellDeckDeleted;

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
            ISpellCardCreator spellCardCreator,
            ISpellCardProvider spellCardProvider,
            ISpellDeckCreator spellDeckCreator,
            ISpellDeckUpdater spellDeckUpdater,
            ISpellDeckDeleter spellDeckDeleter,
            ISpellDeckProvider spellDeckProvider)
        {
            _characterCreator = characterCreator;
            _characterProvider = characterProvider;
            _characterUpdater = characterUpdater;
            _characterDeleter = characterDeleter;
            _spellCardCreator = spellCardCreator;
            _spellCardProvider = spellCardProvider;
            _spellDeckCreator = spellDeckCreator;
            _spellDeckUpdater = spellDeckUpdater;
            _spellDeckDeleter = spellDeckDeleter;
            _spellDeckProvider = spellDeckProvider;

            _initializeLazy = new Lazy<Task>(Initialize);

            _characters = new();
            _spellCards = new();
            _spellDecks = new();
        }

        /// <summary>
        /// Initialize store method
        /// </summary>
        /// <returns></returns>
        private async Task Initialize()
        {
            IEnumerable<SpellCardModel> spellCards = await _spellCardProvider.GetAllSpellCards();
            IEnumerable<SpellDeckModel> spellDecks = await _spellDeckProvider.GetAllSpellDecks();
            IEnumerable<CharacterModel> characters = await _characterProvider.GetAllCharacters();

            _spellCards.Clear();
            _spellDecks.Clear();
            _characters.Clear();

            _spellCards.AddRange(spellCards);
            _spellDecks.AddRange(spellDecks);
            _characters.AddRange(characters);
        }

        /// <summary>
        /// Load method
        /// </summary>
        /// <returns></returns>
        public async Task Load()
        {
            await _initializeLazy.Value;
        }

        #region Character methods

        public async Task CreateCharacter(CharacterModel character)
        {
            CharacterModel createdCharacter = await _characterCreator.CreateCharacter(character);
            _characters.Add(createdCharacter);
            OnCharacterCreated(createdCharacter);
        }

        public async Task DeleteCharacter(CharacterModel character)
        {
            bool success = await _characterDeleter.DeleteCharacter(character);
            if (success)
            { 
            OnCharacterDeleted(character);
            }
        }

        public async Task UpdateCharacter(CharacterModel character)
        {
            bool success = await _characterUpdater.UpdateCharacter(character);
            if (success)
            { 
            OnCharacterUpdated(character);
            }
        }

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

        #endregion

        #region SpellDeck methods

        public async Task CreateSpellDeck(SpellDeckModel spellDeck)
        {
            SpellDeckModel createdSpellDeck = await _spellDeckCreator.CreateSpellDeck(spellDeck);
            _spellDecks.Add(createdSpellDeck);
            OnSpellDeckCreated(createdSpellDeck);
        }

        public async Task UpdateSpellDeck(SpellDeckModel spellDeck)
        {
            bool success = await _spellDeckUpdater.UpdateSpellDeck(spellDeck);
            if (success)
            { 
            OnSpellDeckUpdated(spellDeck);
            }
        }

        public async Task DeleteSpellDeck(SpellDeckModel spellDeck)
        {
            bool success = await _spellDeckDeleter.DeleteSpellDeck(spellDeck);
            if (success)
            {
                _spellDecks.Remove(spellDeck);
                OnSpellDeckDeleted(spellDeck);
            }
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
        #endregion

        #region SpellCard methods

        public async Task CreateSpellCard(SpellCardModel spellCard)
        {
            await _spellCardCreator.CreateSpellCard(spellCard);
        }

        #endregion

    }
}