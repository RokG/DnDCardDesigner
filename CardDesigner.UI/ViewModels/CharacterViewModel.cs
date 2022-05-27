using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CardDesigner.UI.ViewModels
{
    public class CharacterViewModel : ViewModelBase
    {
        #region Private fields

        private readonly CharacterModel _character;
        private readonly CardDesignerStore _cardDesignerStore;

        #endregion

        #region Properties

        private string _cardName;
        public string CardName
        {
            get => _cardName;
            set => SetProperty(ref _cardName, value);
        }

        private List<SpellCardModel> _allSpellCards;
        public List<SpellCardModel> AllSpellCards
        {
            get => _allSpellCards;
            set => SetProperty(ref _allSpellCards, value);
        }

        private List<SpellDeckModel> _allSpellDecks;
        public List<SpellDeckModel> AllSpellDecks
        {
            get => _allSpellDecks;
            set => SetProperty(ref _allSpellDecks, value);
        }

        private List<CharacterModel> _characters;
        public List<CharacterModel> AllCharacters
        {
            get => _characters;
            set => SetProperty(ref _characters, value);
        }

        #endregion

        #region Actions, Events, Commands

        public ICommand DoNavigateCommand { get; }

        #endregion Actions, Events, Commands

        #region Constructor

        public CharacterViewModel(CardDesignerStore cardDesignerStore)
        {
            Name = nameof(CharacterViewModel).Replace("ViewModel", "");

            _cardDesignerStore = cardDesignerStore;
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
