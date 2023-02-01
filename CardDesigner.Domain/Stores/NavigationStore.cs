using CardDesigner.Domain.Interfaces;
using CardDesigner.Domain.Models;
using System;

namespace CardDesigner.Domain.Stores
{
    public class NavigationStore
    {
        public event Action CurrentViewModelChanged;

        public ItemCardModel SelectedItemCard;
        public SpellCardModel SelectedSpellCard;
        public ItemDeckModel SelectedItemDeck;
        public SpellDeckModel SelectedSpellDeck;
        public CharacterModel SelectedCharacter;
        public CharacterDeckDesignModel SelectedBackgroundDesign;
        public ItemDeckDesignModel SelectedItemDeckDesign;
        public SpellDeckDesignModel SelectedSpellDeckDesign;

        private IViewModelBase _currentViewModel;

        public IViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}