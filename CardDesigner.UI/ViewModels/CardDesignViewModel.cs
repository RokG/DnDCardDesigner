using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
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
        [NotifyCanExecuteChangedFor(nameof(CreateCardDesignCommand))]
        private string addedCardDesignName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateCharacterCommand))]
        private string addedCharacterName;

        [ObservableProperty]
        private string addedSpellDeckName;

        [ObservableProperty]
        private string addedItemDeckName;

        [ObservableProperty]
        private CardDesignModel selectedCardDesign;

        [ObservableProperty]
        private CharacterModel selectedCharacter;

        [ObservableProperty]
        private SpellDeckModel selectedSpellDeck;

        [ObservableProperty]
        private ItemDeckModel selectedItemDeck;


        [ObservableProperty]
        private ObservableCollection<CardDesignModel> allCardDesigns;

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

            LoadData();

            SelectedCardDesign = AllCardDesigns.FirstOrDefault();
            SelectedCharacter = AllCharacters.FirstOrDefault();
            SelectedItemDeck = AllItemDecks.FirstOrDefault();

            GetCharacterSpellDecks();
            GetCharacterItemDecks();
            UpdateCardDesign();
        }

        private void OnCardDesignUpdated(CardDesignModel obj)
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
                foreach (SpellDeckDesignModel deckDescriptor in SelectedCharacter.SpellDeckDescriptors)
                {
                    CharacterSpellDecks.Add(AllSpellDecks.First(i => i.ID == deckDescriptor.SpellDeckID));
                }
            }
        }

        private void GetCharacterItemDecks()
        {
            CharacterItemDecks = new();
            if (SelectedCharacter != null)
            {
                foreach (ItemDeckDesignModel deckDescriptor in SelectedCharacter.ItemDeckDescriptors)
                {
                    CharacterItemDecks.Add(AllItemDecks.First(i => i.ID == deckDescriptor.ItemDeckID));
                }
            }
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
            AllCardDesigns = new(_cardDesignerStore.CardDesigns);
        }

        #endregion

        #region Commands

        [RelayCommand(CanExecute = nameof(CanCreateCardDesign))]
        private async void CreateCardDesign()
        {
            await _cardDesignerStore.CreateCardDesign(new CardDesignModel() { Name = AddedCardDesignName });
        }

        private bool CanCreateCardDesign()
        {
            bool noName = (AddedCardDesignName == string.Empty || AddedCardDesignName == null);

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
        private async void DeleteCardDesign()
        {
            await _cardDesignerStore.DeleteCardDesign(SelectedCardDesign);
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
                        .DesignID = SelectedCardDesign == null ? 0 : SelectedCardDesign.ID;
                }
                else
                {
                    SelectedCharacter.SpellDeckDescriptors.Add(new()
                    {
                        SpellDeckID = spellDeck.ID,
                        DesignID = SelectedCardDesign == null ? 0 : SelectedCardDesign.ID
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
                        DesignID = SelectedCardDesign == null ? 0 : SelectedCardDesign.ID
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
                        .DesignID = SelectedCardDesign == null ? 0 : SelectedCardDesign.ID;
                }
                else
                {
                    SelectedCharacter.ItemDeckDescriptors.Add(new()
                    {
                        ItemDeckID = itemDeck.ID,
                        DesignID = SelectedCardDesign == null ? 0 : SelectedCardDesign.ID
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
                        DesignID = SelectedCardDesign == null ? 0 : SelectedCardDesign.ID
                    }
                };
            }

            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }

        [RelayCommand]
        private async void RemoveSpellDeckFromCharacter(SpellDeckModel spellDeck)
        {
            SpellDeckDesignModel toRemove = SelectedCharacter.SpellDeckDescriptors.FirstOrDefault(sd => sd.SpellDeckID == spellDeck.ID);
            SelectedCharacter.SpellDeckDescriptors.Remove(toRemove);
            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }

        [RelayCommand]
        private async void RemoveItemDeckFromCharacter(ItemDeckModel itemDeck)
        {
            ItemDeckDesignModel toRemove = SelectedCharacter.ItemDeckDescriptors.FirstOrDefault(sd => sd.ItemDeckID == itemDeck.ID);
            SelectedCharacter.ItemDeckDescriptors.Remove(toRemove);
            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }

        [RelayCommand]
        private async void UpdateCardDesign()
        {
            await _cardDesignerStore.UpdateCardDesign(SelectedCardDesign);
        }
        #endregion
    }
}
