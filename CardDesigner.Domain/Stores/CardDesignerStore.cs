﻿using CardDesigner.Domain.Models;
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

        private readonly List<SpellCardModel> _spellCards;
        private readonly List<CharacterModel> _characters;
        public IEnumerable<SpellCardModel> SpellCards => _spellCards;
        public IEnumerable<CharacterModel> Characters => _characters;

        public CardDesignerStore(
            ICharacterCreator characterCreator,
            ICharacterProvider characterProvider,
            ISpellCardCreator spellCardCreator,
            ISpellCardProvider spellCardProvider)
        {
            _initializeLazy = new Lazy<Task>(Initialize);
            _characterCreator = characterCreator;
            _characterProvider = characterProvider;
            _spellCardCreator = spellCardCreator;
            _spellCardProvider = spellCardProvider;

            _initializeLazy = new Lazy<Task>(Initialize);

            _characters = new();
            _spellCards = new();
        }

        private async Task Initialize()
        {
            IEnumerable<SpellCardModel> spellCards = await _spellCardProvider.GetAllSpellCards();
            IEnumerable<CharacterModel> characters = await _characterProvider.GetAllCharacters();

            _characters.Clear();
            _spellCards.Clear();

            _characters.AddRange(characters);
            _spellCards.AddRange(spellCards);
        }

        public async Task Load()
        {
            await _initializeLazy.Value;
        }
    }
}