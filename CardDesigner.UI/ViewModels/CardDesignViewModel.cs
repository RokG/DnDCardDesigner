﻿using CardDesigner.Domain.Interfaces;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace CardDesigner.UI.ViewModels
{
    public partial class CardDesignViewModel : ViewModelBase
    {
        #region Private fields

        private readonly CardDesignerStore _cardDesignerStore;

        #endregion

        #region Properties

        [ObservableProperty]
        private ItemCardModel testItemCard;

        [ObservableProperty]
        private SpellCardModel testSpellCard;


        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateSpellCardDesignCommand))]
        private string addedSpellCardDesignName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateItemCardDesignCommand))]
        private string addedItemCardDesignName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateCharacterCommand))]
        private string addedCharacterName;

        [ObservableProperty]
        private string addedSpellDeckName;

        [ObservableProperty]
        private string addedItemDeckName;

        [ObservableProperty]
        private SpellDeckDesignModel selectedSpellDeckDesign;

        [ObservableProperty]
        private ItemDeckDesignModel selectedItemDeckDesign;

        [ObservableProperty]
        private CharacterDeckDesignModel selectedCharacterDeckDesign;

        [ObservableProperty]
        private CharacterModel selectedCharacter;

        [ObservableProperty]
        private SpellDeckModel selectedSpellDeck;

        [ObservableProperty]
        private ItemDeckModel selectedItemDeck;


        [ObservableProperty]
        private ObservableCollection<SpellDeckDesignModel> allSpellDeckDesigns;

        [ObservableProperty]
        private ObservableCollection<ItemDeckDesignModel> allItemDeckDesigns;

        [ObservableProperty]
        private ObservableCollection<CharacterDeckDesignModel> allCharacterDeckDesigns;

        [ObservableProperty]
        private ObservableCollection<SpellDeckModel> allSpellDecks;

        [ObservableProperty]
        private ObservableCollection<SpellDeckModel> characterSpellDecks;

        [ObservableProperty]
        private ObservableCollection<ItemDeckModel> allItemDecks;

        [ObservableProperty]
        private ObservableCollection<ItemDeckModel> characterItemDecks;

        [ObservableProperty]
        private ObservableCollection<CharacterModel> allCharacters;

        #endregion

        #region Actions, Events, Commands

        public ICommand DoNavigateCommand { get; }

        #endregion Actions, Events, Commands

        #region Constructor

        public CardDesignViewModel(CardDesignerStore cardDesignerStore)
        {
            Name = Regex.Replace(nameof(CardDesignViewModel).Replace("ViewModel", ""), "(\\B[A-Z])", " $1");
            Description = "Create, view and edit Card designs";

            _cardDesignerStore = cardDesignerStore;

            _cardDesignerStore.CharacterCreated += OnCharacterCreated;
            _cardDesignerStore.CharacterUpdated += OnCharacterUpdated;
            _cardDesignerStore.CharacterDeleted += OnCharacterDeleted;

            _cardDesignerStore.CardDesignUpdated += OnCardDesignUpdated;
            _cardDesignerStore.CardDesignCreated += OnCardDesignCreated;
            _cardDesignerStore.CardDesignDeleted += OnCardDesignDeleted;

            LoadData();

            SelectedSpellDeckDesign = AllSpellDeckDesigns.FirstOrDefault();
            SelectedItemDeckDesign = AllItemDeckDesigns.FirstOrDefault();
            SelectedCharacterDeckDesign = AllCharacterDeckDesigns.FirstOrDefault();
            SelectedCharacter = AllCharacters.FirstOrDefault();
            SelectedItemDeck = AllItemDecks.FirstOrDefault();

            GetCharacterSpellDecks();
            GetCharacterItemDecks();
            UpdateSpellDeckDesign();
            UpdateItemDeckDesign();
        }

        private void OnCardDesignDeleted(ICardDesign cardDesign)
        {
            switch (cardDesign)
            {
                case SpellDeckDesignModel spellDeckDesignModel:
                    AllSpellDeckDesigns.Remove(spellDeckDesignModel);
                    SelectedSpellDeckDesign = AllSpellDeckDesigns.FirstOrDefault();
                    break;
                case ItemDeckDesignModel characterDeckDesignModel:
                    AllItemDeckDesigns.Remove(characterDeckDesignModel);
                    SelectedItemDeckDesign = AllItemDeckDesigns.FirstOrDefault();
                    break;
                case CharacterDeckDesignModel itemDeckDesignModel:
                    AllCharacterDeckDesigns.Remove(itemDeckDesignModel);
                    SelectedCharacterDeckDesign = AllCharacterDeckDesigns.FirstOrDefault();
                    break;
                default:
                    break;
            }
        }

        private void OnCardDesignCreated(ICardDesign cardDesign)
        {
            switch (cardDesign)
            {
                case SpellDeckDesignModel spellDeckDesignModel:
                    AllSpellDeckDesigns.Add(spellDeckDesignModel);
                    SelectedSpellDeckDesign = spellDeckDesignModel;
                    break;
                case ItemDeckDesignModel itemDeckDesignModel:
                    AllItemDeckDesigns.Add(itemDeckDesignModel);
                    SelectedItemDeckDesign = itemDeckDesignModel;
                    break;
                case CharacterDeckDesignModel characterDeckDesignModel:
                    AllCharacterDeckDesigns.Add(characterDeckDesignModel);
                    SelectedCharacterDeckDesign = characterDeckDesignModel;
                    break;
                default:
                    break;
            }
        }

        private void OnCardDesignUpdated(ICardDesign cardDesign)
        {
            try
            {
                //TestItemCard = SelectedCharacter?.ItemDecks?.FirstOrDefault().ItemCards?.FirstOrDefault();
                //TestSpellCard = SelectedCharacter?.SpellDecks?.FirstOrDefault().SpellCards?.FirstOrDefault();
            }
            catch (System.Exception)
            {
            }
        }

        private void GetCharacterSpellDecks()
        {
            CharacterSpellDecks = new();
            if (SelectedCharacter != null)
            {
                foreach (SpellDeckDesignLinkerModel deckDescriptor in SelectedCharacter.SpellDeckDescriptors)
                {
                    CharacterSpellDecks.Add(AllSpellDecks.First(i => i.ID == deckDescriptor.SpellDeckID));
                }
            }
            SelectedSpellDeck = CharacterSpellDecks.FirstOrDefault();
        }

        private void GetCharacterItemDecks()
        {
            CharacterItemDecks = new();
            if (SelectedCharacter != null)
            {
                foreach (ItemDeckDesignLinkerModel deckDescriptor in SelectedCharacter.ItemDeckDescriptors)
                {
                    CharacterItemDecks.Add(AllItemDecks.First(i => i.ID == deckDescriptor.ItemDeckID));
                }
            }
            SelectedItemDeck = CharacterItemDecks.FirstOrDefault();
        }

        private void OnCharacterUpdated(CharacterModel character)
        {
            SelectedCharacter = character;
            GetCharacterSpellDecks();
            GetCharacterItemDecks();
        }

        #endregion

        #region Private methods
        private void OnCharacterCreated(CharacterModel character)
        {
            AllCharacters.Add(character);
            SelectedCharacter = character;
        }

        private void OnCharacterDeleted(CharacterModel character)
        {
            AllCharacters.Remove(SelectedCharacter);
            SelectedCharacter = AllCharacters.FirstOrDefault();
        }

        #endregion

        #region Public methods

        public static CardDesignViewModel LoadViewModel(CardDesignerStore cardDesignerStore)
        {
            CardDesignViewModel viewModel = new(cardDesignerStore);

            viewModel.LoadData();

            return viewModel;
        }

        private async void LoadData()
        {
            await _cardDesignerStore.Load();

            AllCharacters = new(_cardDesignerStore.Characters);
            AllSpellDecks = new(_cardDesignerStore.SpellDecks);
            AllItemDecks = new(_cardDesignerStore.ItemDecks);
            AllSpellDeckDesigns = new(_cardDesignerStore.SpellDeckDesigns);
            AllItemDeckDesigns = new(_cardDesignerStore.ItemDeckDesigns);
            AllCharacterDeckDesigns = new(_cardDesignerStore.CharacterDeckDesigns);
        }

        #endregion

        #region Commands

        [RelayCommand(CanExecute = nameof(CanCreateSpellCardDesign))]
        private async void CreateSpellCardDesign()
        {
            await _cardDesignerStore.CreateCardDesign(new SpellDeckDesignModel() { Name = AddedSpellCardDesignName });
        }

        private bool CanCreateSpellCardDesign()
        {
            bool noName = (AddedSpellCardDesignName == string.Empty || AddedSpellCardDesignName == null);

            return !noName;
        }

        [RelayCommand(CanExecute = nameof(CanCreateItemCardDesign))]
        private async void CreateItemCardDesign()
        {
            await _cardDesignerStore.CreateCardDesign(new ItemDeckDesignModel() { Name = AddedItemCardDesignName });
        }

        private bool CanCreateItemCardDesign()
        {
            bool noName = (AddedItemCardDesignName == string.Empty || AddedItemCardDesignName == null);

            return !noName;
        }

        [RelayCommand(CanExecute = nameof(CanCreateCharacter))]
        private async void CreateCharacter()
        {
            await _cardDesignerStore.CreateCharacter(new CharacterModel() { Name = AddedCharacterName });
        }

        private bool CanCreateCharacter()
        {
            bool noName = (AddedCharacterName == string.Empty || AddedCharacterName == null);
            bool spellDeckExists = AllCharacters == null ? false : AllCharacters.Where(c => c.Name == AddedCharacterName).Any();

            return (!noName && !spellDeckExists);
        }

        [RelayCommand]
        private async void DeleteCharacter()
        {
            await _cardDesignerStore.DeleteCharacter(SelectedCharacter);
        }

        [RelayCommand]
        private async void DeleteSpellCardDesign()
        {
            //await _cardDesignerStore.DeleteCardDesign(SelectedCardDesign);
        }
        [RelayCommand]
        private async void DeleteItemCardDesign()
        {
            //await _cardDesignerStore.DeleteCardDesign(SelectedCardDesign);
        }

        [RelayCommand]
        private async void AddSpellDeckToCharacter(SpellDeckModel spellDeck)
        {
            if (SelectedCharacter.SpellDeckDescriptors.Any())
            {
                // If character has any descriptors
                if (SelectedCharacter.SpellDeckDescriptors.Any(d => d.SpellDeckID == spellDeck.ID))
                {
                    SelectedCharacter.SpellDeckDescriptors
                        .First(d => d.SpellDeckID == spellDeck.ID)
                        .DesignID = SelectedSpellDeckDesign == null ? 0 : SelectedSpellDeckDesign.ID;
                }
                else
                {
                    SelectedCharacter.SpellDeckDescriptors.Add(new()
                    {
                        SpellDeckID = spellDeck.ID,
                        DesignID = SelectedSpellDeckDesign == null ? 0 : SelectedSpellDeckDesign.ID
                    });
                }
            }
            else
            {
                // Otherwise make a new list
                SelectedCharacter.SpellDeckDescriptors = new()
                {
                    new()
                    {
                        SpellDeckID = spellDeck.ID,
                        DesignID = SelectedSpellDeckDesign == null ? 0 : SelectedSpellDeckDesign.ID
                    }
                };
            }

            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }

        [RelayCommand]
        private async void AddItemDeckToCharacter(ItemDeckModel itemDeck)
        {
            if (SelectedCharacter.ItemDeckDescriptors.Any())
            {
                // If character has any descriptors
                if (SelectedCharacter.ItemDeckDescriptors.Any(d => d.ItemDeckID == itemDeck.ID))
                {
                    SelectedCharacter.ItemDeckDescriptors
                        .First(d => d.ItemDeckID == itemDeck.ID)
                        .DesignID = SelectedItemDeckDesign == null ? 0 : SelectedItemDeckDesign.ID;
                }
                else
                {
                    SelectedCharacter.ItemDeckDescriptors.Add(new()
                    {
                        ItemDeckID = itemDeck.ID,
                        DesignID = SelectedItemDeckDesign == null ? 0 : SelectedItemDeckDesign.ID
                    });
                }
            }
            else
            {
                // Otherwise make a new list
                SelectedCharacter.ItemDeckDescriptors = new()
                {
                    new()
                    {
                        ItemDeckID = itemDeck.ID,
                        DesignID = SelectedItemDeckDesign == null ? 0 : SelectedItemDeckDesign.ID
                    }
                };
            }

            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }

        [RelayCommand]
        private async void RemoveSpellDeckFromCharacter(SpellDeckModel spellDeck)
        {
            SpellDeckDesignLinkerModel toRemove = SelectedCharacter.SpellDeckDescriptors.FirstOrDefault(sd => sd.SpellDeckID == spellDeck.ID);
            SelectedCharacter.SpellDeckDescriptors.Remove(toRemove);
            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }

        [RelayCommand]
        private async void RemoveItemDeckFromCharacter(ItemDeckModel itemDeck)
        {
            ItemDeckDesignLinkerModel toRemove = SelectedCharacter.ItemDeckDescriptors.FirstOrDefault(sd => sd.ItemDeckID == itemDeck.ID);
            SelectedCharacter.ItemDeckDescriptors.Remove(toRemove);
            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }

        [RelayCommand]
        private async void UpdateSpellDeckDesign()
        {
            await _cardDesignerStore.UpdateCardDesign(SelectedSpellDeckDesign);
        }
        [RelayCommand]
        private async void UpdateItemDeckDesign()
        {
            await _cardDesignerStore.UpdateCardDesign(SelectedItemDeckDesign);
        }
        #endregion
    }
}
