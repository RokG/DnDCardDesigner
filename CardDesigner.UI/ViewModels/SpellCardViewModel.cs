using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CardDesigner.UI.ViewModels
{
    public class SpellCardViewModel : ViewModelBase
    {
        #region Private fields

        private readonly CharacterModel _character;

        #endregion

        #region Properties

        private MagicSchool _magicSchoolType;
        public MagicSchool MagicSchoolType
        {
            get => _magicSchoolType;
            set => SetProperty(ref _magicSchoolType, value);
        }

        private ICard _selectedCard;
        public ICard SelectedCard
        {
            get => _selectedCard;
            set => SetProperty(ref _selectedCard, value);
        }
        public CharacterModel SelectedCharacter { get; set; }
        public SpellCardModel ConstantCard = new SpellCardModel() {Name = "bbb", ID = 1 };
        public SpellCardModel SelectedSpellCard { get; set; }
        public SpellDeckModel SelectedSpellDeck { get; set; }

        #endregion Properties

        #region Actions, Events, Commands

        public ICommand AddCardCommand { get; }
        public ICommand CreateCharacterCommand { get; }
        public ICommand CreateSpellDeckCommand { get; }
        public ICommand DoNavigateCommand { get; }

        #endregion Actions, Events, Commands

        #region Constructor

        public SpellCardViewModel(CardDesignerStore cardDesignerStore)
        {
            Name = nameof(SpellCardViewModel).Replace("ViewModel", "");

            SelectedCharacter = new CharacterModel(RandomString(6));
            SelectedSpellCard = new SpellCardModel() { Name = RandomString(6) };
            SelectedSpellDeck = new SpellDeckModel() { Name = "aababa"};

            SelectedSpellDeck.SpellCards = new List<SpellCardModel>();
            SelectedSpellDeck.SpellCards.Add(SelectedSpellCard);

            AddCardCommand = new AddCardCommand(this, cardDesignerStore);
            CreateCharacterCommand = new CreateCharacterCommand(this, cardDesignerStore);
            CreateSpellDeckCommand = new CreateSpellDeckCommand(this, cardDesignerStore);
            //DoNavigateCommand = new NavigateCommand(navigationService);

        }

        #endregion

        #region Private methods
        private static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        #endregion

        #region Public methods

        public void AddRandomCard()
        {
            Random random = new Random();
            SelectedSpellCard = new SpellCardModel() { Name = RandomString(6), ID = random.Next() };
        }

        public void AddRandomDeck()
        {
            Random random = new Random();
            SelectedSpellDeck = new SpellDeckModel() { Name = RandomString(6) , ID = random.Next() };
            SelectedSpellDeck.SpellCards = new List<SpellCardModel>();
        }

        public void AddRandomCardToDeck()
        {
            Random random = new Random();
            SelectedSpellCard = new SpellCardModel() { Name = RandomString(6), ID = random.Next() };

            SelectedSpellDeck.SpellCards.Add(SelectedSpellCard);
        }

        public void AddConstantCardToDeck()
        {
            SelectedSpellDeck.SpellCards.Add(ConstantCard);
        }

        public static SpellCardViewModel LoadViewModel(CardDesignerStore cardDesignerStore)
        {
            return new(cardDesignerStore);
        }

        #endregion

    }
}