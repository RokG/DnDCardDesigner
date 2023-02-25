using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace CardDesigner.UI.ViewModels
{
    public partial class CharacterCardViewModel : ViewModelBase
    {
        #region Private fields

        private readonly CardDesignerStore _cardDesignerStore;
        private readonly NavigationStore _navigationStore;

        #endregion

        #region Properties

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateCharacterCardCommand))]
        private string characterCardName;

        [ObservableProperty]
        private CharacterModel selectedCharacter;

        [ObservableProperty]
        private CharacterCardModel selectedCharacterCard;

        [ObservableProperty]
        private CharacterDeckDesignModel selectedCharacterDeckDesign = new();

        [ObservableProperty]
        private ObservableCollection<CharacterCardModel> allCharacterCards;

        [ObservableProperty]
        private ObservableCollection<CharacterModel> allCharacters;

        #endregion

        #region Constructor

        public CharacterCardViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore)
        {
            Name = Regex.Replace(nameof(CharacterCardViewModel).Replace("ViewModel", ""), "(\\B[A-Z])", " $1");
            Description = "Create, view and edit Character Cards";
            Type = ViewModelType.CharacterCardCreator;

            _cardDesignerStore = cardDesignerStore;
            _navigationStore = navigationStore;

            SetUnsetDatabaseEvents(true);

            LoadData();

            SetSelectionFromNavigation();
        }

        #endregion

        #region Private methods

        private async void LoadData()
        {
            await _cardDesignerStore.Load();

            AllCharacterCards = new(_cardDesignerStore.CharacterCards);
            SelectedCharacterCard = AllCharacterCards.FirstOrDefault();
            AllCharacters = new(_cardDesignerStore.Characters);
            SelectedCharacter = AllCharacters.FirstOrDefault();
        }

        #endregion

        #region Public methods

        public static CharacterCardViewModel LoadViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore)
        {
            CharacterCardViewModel viewModel = new(cardDesignerStore, navigationStore);
            viewModel.LoadData();

            return viewModel;
        }

        #endregion

        #region Database update methods

        private void SetUnsetDatabaseEvents(bool set)
        {
            if (set)
            {
                _cardDesignerStore.CharacterCardChanged += OnCharacterCardChanged;
                _navigationStore.CurrentViewModelChanged += OnNavigatingAway;
            }
            else
            {
                _cardDesignerStore.CharacterCardChanged -= OnCharacterCardChanged;
                _navigationStore.CurrentViewModelChanged -= OnNavigatingAway;
            }
        }

        private void OnCharacterCardChanged(CharacterCardModel characterCard, DataChangeType change)
        {
            switch (change)
            {
                case DataChangeType.Created:
                    AllCharacterCards.Add(characterCard);
                    SelectedCharacterCard = characterCard;
                    break;
                case DataChangeType.Deleted:
                    AllCharacterCards.Remove(characterCard);
                    SelectedCharacterCard = AllCharacterCards.FirstOrDefault();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Navigation

        private void SetSelectionFromNavigation()
        {
            if (_navigationStore != null)
            {
                if (_navigationStore.UseSelection)
                {
                    switch (_navigationStore.CurrentViewModel.Type)
                    {
                        case ViewModelType.Home:
                            SelectedCharacter = AllCharacters.FirstOrDefault(ic => ic.ID == _navigationStore.SelectedCharacter.ID);
                            SelectedCharacterCard = AllCharacterCards.FirstOrDefault(ic => ic.ID == _navigationStore.SelectedCharacterCard.ID);
                            SelectedCharacterDeckDesign = _navigationStore.SelectedCharacterDeckDesign;
                            return;
                        default:
                            break;
                    }
                }
                else
                {
                    SelectedCharacter = AllCharacters.FirstOrDefault();
                    SelectedCharacterCard = AllCharacterCards.FirstOrDefault();
                    SelectedCharacterDeckDesign = new();
                }
            }
        }

        private void OnNavigatingAway(ViewModelType type)
        {
            SetUnsetDatabaseEvents(false);
        }

        #endregion

        #region Commands

        [RelayCommand(CanExecute = nameof(CanCreateCharacterCard))]
        private async void CreateCharacterCard()
        {
            await _cardDesignerStore.CreateCharacterCard(new CharacterCardModel() { Name = CharacterCardName });
        }

        private bool CanCreateCharacterCard()
        {
            return CharacterCardName != null
                && CharacterCardName != string.Empty
                && !AllCharacterCards.Where(c => c.Name == CharacterCardName).Any();
        }

        [RelayCommand]
        private async void UpdateCharacterCard()
        {
            switch (SelectedCharacterCard.Type)
            {
                case CharacterCardType.Avatar:
                    SelectedCharacterCard.Title = SelectedCharacter.Title + " - Avatar";
                    break;
                case CharacterCardType.Stats:
                    SelectedCharacterCard.Title = SelectedCharacter.Title + " - Stats";
                    break;
                case CharacterCardType.Abilities:
                    SelectedCharacterCard.Title = SelectedCharacter.Title + " - Abilities";
                    break;
                case CharacterCardType.Caster:
                    SelectedCharacterCard.Title = SelectedCharacter.Title + " - Caster stats";
                    break;
                default:
                    break;
            }

            await _cardDesignerStore.UpdateCharacterCard(SelectedCharacterCard);
        }

        [RelayCommand]
        private async void DeleteCharacterCard()
        {
            await _cardDesignerStore.DeleteCharacterCard(SelectedCharacterCard);
        }

        #endregion
    }
}
