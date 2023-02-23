using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Enums;
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
    public partial class CharacterViewModel : ViewModelBase
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
        [NotifyCanExecuteChangedFor(nameof(AddClassToCharacterCommand))]
        [NotifyCanExecuteChangedFor(nameof(RemoveClassFromCharacterCommand))]
        private CharacterModel selectedCharacter;

        [ObservableProperty]
        private ObservableCollection<CharacterModel> allCharacters;

        [ObservableProperty]
        private string selectedSpecialization;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddClassToCharacterCommand))]
        private ClassModel selectedClass;

        [ObservableProperty]
        private ObservableCollection<ClassModel> allClasses;

        [ObservableProperty]
        private CharacterClassModel characterClasses;
        #endregion

        #region Constructor

        public CharacterViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore)
        {
            Name = Regex.Replace(nameof(CharacterViewModel).Replace("ViewModel", ""), "(\\B[A-Z])", " $1");
            Description = "Create, view and edit Characters";
            Type = ViewModelType.CharacterCreator;

            _cardDesignerStore = cardDesignerStore;
            _navigationStore = navigationStore;

            SetUnsetDatabaseEvents(true);

            LoadData();

            SelectedCharacter = AllCharacters.FirstOrDefault();
        }

        #endregion

        #region Private methods

        private async void LoadData()
        {
            await _cardDesignerStore.Load();

            AllCharacters = new(_cardDesignerStore.Characters);
            AllClasses = new(_cardDesignerStore.Classes);
        }

        private void SetUnsetDatabaseEvents(bool set)
        {
            if (set)
            {
                _cardDesignerStore.CharacterChanged += OnCharacterChanged;
                _navigationStore.CurrentViewModelChanged += OnNavigatingAway;
            }
            else
            {
                _cardDesignerStore.CharacterChanged -= OnCharacterChanged;
                _navigationStore.CurrentViewModelChanged -= OnNavigatingAway;
            }
        }

        #endregion

        #region Public methods

        public static CharacterViewModel LoadViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore)
        {
            CharacterViewModel viewModel = new(cardDesignerStore, navigationStore);

            viewModel.LoadData();

            return viewModel;
        }

        #endregion

        #region Database update methods

        private void OnCharacterChanged(CharacterModel character, DataChangeType change)
        {
            switch (change)
            {
                case DataChangeType.Created:
                    AllCharacters.Add(character);
                    SelectedCharacter = character;
                    break;
                case DataChangeType.Updated:
                    SelectedCharacter = character;
                    break;
                case DataChangeType.Deleted:
                    AllCharacters.Remove(SelectedCharacter);
                    SelectedCharacter = AllCharacters.FirstOrDefault();
                    break;
                default:
                    break;
            }
        }

        #endregion  

        #region Navigation

        private void OnNavigatingAway(ViewModelType type)
        {
            SetUnsetDatabaseEvents(false);
        }

        #endregion

        #region Commands


        [RelayCommand(CanExecute = nameof(CanAddClassToCharacter))]
        private async void AddClassToCharacter(ClassModel classModel)
        {
            CharacterClassModel characterClassModel = new()
            {
                Class = classModel,
                ClassID = classModel.ID,
                ClassSpecialization = classModel.Specializations.FirstOrDefault()
            };

            if (SelectedCharacter.Classes == null)
            {
                SelectedCharacter.Classes = new()
                {
                    characterClassModel
                };
            }
            else
            {
                SelectedCharacter.Classes.Add(characterClassModel);
            }
            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
            OnPropertyChanged(nameof(SelectedCharacter.Classes));
        }

        private bool CanAddClassToCharacter()
        {
            return (SelectedClass != null) && (SelectedCharacter == null ? false : SelectedCharacter.Classes.Count < 3);
        }

        [RelayCommand(CanExecute = nameof(CanRemoveClassFromCharacter))]
        private async void RemoveClassFromCharacter(ClassModel classModel)
        {
            if (SelectedCharacter.Classes != null && classModel != null)
            {
                if (SelectedCharacter.Classes.Count > 0)
                {
                    CharacterClassModel existingClass = SelectedCharacter.Classes.FirstOrDefault(c => c.ClassID == classModel.ID);
                    if (existingClass != null)
                    {
                        SelectedCharacter.Classes.Remove(existingClass);
                        await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
                    }
                }
            }
        }

        private bool CanRemoveClassFromCharacter()
        {
            return SelectedCharacter == null ? false : SelectedCharacter.Classes.Count > 0;
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
        private async void UpdateCharacter()
        {
            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }

        #endregion
    }
}
