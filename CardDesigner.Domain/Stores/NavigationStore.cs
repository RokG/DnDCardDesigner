using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Interfaces;
using CardDesigner.Domain.Models;
using System;

namespace CardDesigner.Domain.Stores
{
    public class NavigationStore
    {
        public event Action<ViewModelType> CurrentViewModelChanged;

        public bool UseSelection;
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
            set => _currentViewModel = value;
        }

        public void NavigateTo(ViewModelType type)
        {
            UseSelection = true;
            CurrentViewModelChanged?.Invoke(type);
        }
    }
}