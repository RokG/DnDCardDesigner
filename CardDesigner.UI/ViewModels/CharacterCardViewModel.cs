using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Input;

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
        [NotifyCanExecuteChangedFor(nameof(CreateCharacterCommand))]
        private string addedCharacterName;

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

        #region Actions, Events, Commands

        public ICommand DoNavigateCommand { get; }

        #endregion Actions, Events, Commands

        #region Constructor

        public CharacterCardViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore)
        {
            Name = Regex.Replace(nameof(CharacterCardViewModel).Replace("ViewModel", ""), "(\\B[A-Z])", " $1");
            Description = "Create, view and edit Character Cards";
            Type = ViewModelType.CharacterCardCreator;

            _cardDesignerStore = cardDesignerStore;
            _navigationStore = navigationStore;

            _cardDesignerStore.CharacterCardChanged += OnCharacterCardChanged;

            LoadData();
        }
        private void SetSelectionFromNavigation()
        {
            if (_navigationStore != null)
            {
                switch (_navigationStore.CurrentViewModel.Type)
                {
                    case ViewModelType.Unknown:
                        return;
                    case ViewModelType.Home:
                        return;
                    case ViewModelType.SpellCardCreator:
                        return;
                    case ViewModelType.CharacterCardCreator:
                        return;
                    case ViewModelType.DeckCreator:
                        return;
                    case ViewModelType.CharacterCreator:
                        return;
                    case ViewModelType.DeckDesigner:
                        //SelectedCharacterDeckDesign = _navigationStore.SelectedCharacterDeckDesign;
                        return;
                    default:
                        break;
                }
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
                default:
                    break;
            }
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
            await _cardDesignerStore.UpdateCharacterCard(SelectedCharacterCard);
        }


        [RelayCommand(CanExecute = nameof(CanCreateCharacter))]
        private async void CreateCharacter()
        {
            await _cardDesignerStore.CreateCharacter(
                new CharacterModel()
                {
                    Name = AddedCharacterName,
                    Classes = new() {
                        new CharacterClassModel() { Class = CharacterClassType.Barbarian, Level=3 },
                        new CharacterClassModel() { Class = CharacterClassType.Druid, Level=5 }},
                    Skills = new() {
                        new CharacterSkillModel() {Skill = Skill.Athletics, IsProficient=true},
                        new CharacterSkillModel() {Skill = Skill.Acrobatics},
                        new CharacterSkillModel() {Skill = Skill.AnimalHandling, IsExpert=true},
                    }
                });
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


        #endregion

    }
}
