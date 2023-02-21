using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Interfaces;
using CardDesigner.Domain.Models;
using System;

namespace CardDesigner.Domain.Stores
{
    public class NavigationStore
    {
        public event Action<ViewModelType> CurrentViewModelChanged;

        public CardType SelectedCardType;
        public ItemCardModel SelectedItemCard;
        public SpellCardModel SelectedSpellCard;
        public CharacterCardModel SelectedCharacterCard;
        public ItemDeckModel SelectedItemDeck;
        public SpellDeckModel SelectedSpellDeck;
        public CharacterDeckModel SelectedCharacterDeck;
        public CharacterModel SelectedCharacter;
        public DeckBackgroundDesignModel SelectedDeckBackgroundDesign;
        public CharacterDeckDesignModel SelectedCharacterDeckDesign;
        public ItemDeckDesignModel SelectedItemDeckDesign;
        public SpellDeckDesignModel SelectedSpellDeckDesign;

        private IViewModelBase _currentViewModel;

        public IViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                if (value != null && _currentViewModel != null)
                {
                    if (_currentViewModel.Type != value.Type)
                    {
                        OnCurrentViewModelChanged(value.Type);
                    }
                }
            }
        }

        private void OnCurrentViewModelChanged(ViewModelType viewModelType)
        {
            CurrentViewModelChanged?.Invoke(viewModelType);
        }

        public void NavigateTo(ViewModelType type)
        {
            CurrentViewModelChanged?.Invoke(type);
        }
    }
}