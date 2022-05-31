﻿using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CardDesigner.UI.ViewModels
{
    public class CharacterViewModel : ViewModelBase
    {
        #region Private fields

        private readonly CardDesignerStore _cardDesignerStore;

        #endregion

        #region Properties

        private string _addedCharacterName;
        public string AddedCharacterName
        {
            get => _addedCharacterName;
            set => SetProperty(ref _addedCharacterName, value);
        }

        private string _addedSpellDeckName;
        public string AddedSpellDeckName
        {
            get => _addedSpellDeckName;
            set => SetProperty(ref _addedSpellDeckName, value);

        }
        private CharacterModel _selectedCharacter;
        public CharacterModel SelectedCharacter
        {
            get => _selectedCharacter;
            set
            {
                SetProperty(ref _selectedCharacter, value);
                if (value.SpellDeck != null)
                {
                    SelectedSpellDeck = AllSpellDecks.Where(s => s.ID == value.SpellDeck.ID).FirstOrDefault();
                }
            }
        }

        private SpellDeckModel _selectedSpellDeck;
        public SpellDeckModel SelectedSpellDeck
        {
            get => _selectedSpellDeck;
            set
            {
                SetProperty(ref _selectedSpellDeck, value);
                SelectedSpellDeckCards = new ObservableCollection<SpellCardModel>(value.SpellCards);
            }
        }

        private ObservableCollection<SpellCardModel> _selectedSpellDeckCards;
        public ObservableCollection<SpellCardModel> SelectedSpellDeckCards
        {
            get => _selectedSpellDeckCards;
            set => SetProperty(ref _selectedSpellDeckCards, value);
        }

        private ObservableCollection<SpellCardModel> _allSpellCards;
        public ObservableCollection<SpellCardModel> AllSpellCards
        {
            get => _allSpellCards;
            set => SetProperty(ref _allSpellCards, value);
        }

        private ObservableCollection<SpellDeckModel> _allSpellDecks;
        public ObservableCollection<SpellDeckModel> AllSpellDecks
        {
            get => _allSpellDecks;
            set => SetProperty(ref _allSpellDecks, value);
        }

        private ObservableCollection<CharacterModel> _characters;
        public ObservableCollection<CharacterModel> AllCharacters
        {
            get => _characters;
            set => SetProperty(ref _characters, value);
        }

        #endregion

        #region Actions, Events, Commands

        public ICommand DoNavigateCommand { get; }
        public ICommand CreateCharacterCommand { get; }
        public ICommand CreateSpellDeckCommand { get; }

        #endregion Actions, Events, Commands

        #region Constructor

        public CharacterViewModel(CardDesignerStore cardDesignerStore)
        {
            Name = nameof(CharacterViewModel).Replace("ViewModel", "");

            _cardDesignerStore = cardDesignerStore;
            CreateCharacterCommand = new CreateCharacterCommand(this, cardDesignerStore);
            CreateSpellDeckCommand = new CreateSpellDeckCommand(this, cardDesignerStore);
        }

        #endregion

        #region Private methods

        #endregion

        #region Public methods

        public static CharacterViewModel LoadViewModel(CardDesignerStore cardDesignerStore)
        {
            CharacterViewModel viewModel = new(cardDesignerStore);
            viewModel.LoadData();

            return viewModel;
        }

        private async void LoadData()
        {
            await _cardDesignerStore.Load();

            AllCharacters = new(_cardDesignerStore.Characters);
            AllSpellCards = new(_cardDesignerStore.SpellCards);
            AllSpellDecks = new(_cardDesignerStore.SpellDecks);
        }

        #endregion
    }
}
