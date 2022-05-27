using CardDesigner.Domain.Enums;
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
        private readonly ISpellCardCreator _spellCardCreator;
        private readonly ISpellCardProvider _spellCardProvider;
        private readonly ISpellDeckCreator _spellDeckCreator;
        private readonly ISpellDeckProvider _spellDeckProvider;

        private readonly List<SpellCardModel> _spellCards;
        private readonly List<CharacterModel> _characters;
        public IEnumerable<SpellCardModel> SpellCards => _spellCards;
        public IEnumerable<CharacterModel> Characters => _characters;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="characterCreator"></param>
        /// <param name="characterProvider"></param>
        /// <param name="spellCardCreator"></param>
        /// <param name="spellCardProvider"></param>
        public CardDesignerStore(
            ICharacterCreator characterCreator,
            ICharacterProvider characterProvider,
            ISpellCardCreator spellCardCreator,
            ISpellCardProvider spellCardProvider,
            ISpellDeckCreator spellDeckCreator,
            ISpellDeckProvider spellDeckProvider)
        {
            _initializeLazy = new Lazy<Task>(Initialize);
            _characterCreator = characterCreator;
            _characterProvider = characterProvider;
            _spellCardCreator = spellCardCreator;
            _spellCardProvider = spellCardProvider;
            _spellDeckCreator = spellDeckCreator;
            _spellDeckProvider = spellDeckProvider;

            _initializeLazy = new Lazy<Task>(Initialize);

            _characters = new();
            _spellCards = new();
        }

        /// <summary>
        /// Initialize store method
        /// </summary>
        /// <returns></returns>
        private async Task Initialize()
        {
            IEnumerable<SpellCardModel> spellCards = await _spellCardProvider.GetAllSpellCards();
            IEnumerable<CharacterModel> characters = await _characterProvider.GetAllCharacters();

            _characters.Clear();
            _spellCards.Clear();

            _characters.AddRange(characters);
            _spellCards.AddRange(spellCards);
        }

        /// <summary>
        /// Load method
        /// </summary>
        /// <returns></returns>
        public async Task Load()
        {
            await _initializeLazy.Value;
        }

        public async Task AddCardToCharacter(CharacterModel character, ICard card)
        {
            switch (card.Type)
            {
                case CardType.Spell:
                    {
                        SpellCardModel spellCardModel = (SpellCardModel)card;
                        await _spellCardCreator.CreateSpellCard(spellCardModel);
                    }
                    break;
                case CardType.Item:
                    break;
                default:
                    break;
            }


        }

        public async Task CreateCharacter(CharacterModel character)
        {
            await _characterCreator.CreateCharacter(character);
        }

        public async Task CreateSpellDeck(SpellDeckModel spellDeck)
        {
            await _spellDeckCreator.CreateSpellDeck(spellDeck);
        }

        public async Task CreateSpellCard(SpellCardModel spellCard)
        {
            await _spellCardCreator.CreateSpellCard(spellCard);
        }
    }
}