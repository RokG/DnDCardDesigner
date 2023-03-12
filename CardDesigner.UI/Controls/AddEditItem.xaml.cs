using CardDesigner.Domain.Interfaces;
using CardDesigner.UI.Behaviours;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CardDesigner.UI.Controls
{
    /// <summary>
    /// Interaction logic for AddEditItem.xaml
    /// </summary>
    public partial class AddEditItem : UserControl
    {
        public AddEditItem()
        {
            InitializeComponent();
            Loaded += InitalizeSetup;
            InputBindingBehavior.SetPropagateInputBindingsToWindow(this, true);
        }

        private void InitalizeSetup(object sender, EventArgs e)
        {
            if (!ViewOnly)
            {
                InputBindings.Clear();
                InputBindings.Add(new KeyBinding(SaveChangesCommand, Key.S, ModifierKeys.Control));
                InputBindings.Add(new KeyBinding(OpenEditModeCommand, Key.N, ModifierKeys.Control));
                InputBindings.Add(new KeyBinding(CloseEditModeCommand, Key.Escape, ModifierKeys.None));
                InputBindings.Add(new KeyBinding(CreateItemCommand, Key.Enter, ModifierKeys.None));
            }
        }

        #region Events

        public event RoutedEventHandler SelectionChanged;

        #endregion

        #region Properties

        public Orientation Orientation
        {
            get => (Orientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(AddEditItem), new PropertyMetadata(Orientation.Vertical));

        public bool ViewOnly
        {
            get => (bool)GetValue(ViewOnlyProperty);
            set => SetValue(ViewOnlyProperty, value);
        }

        public static readonly DependencyProperty ViewOnlyProperty =
            DependencyProperty.Register(nameof(ViewOnly), typeof(bool), typeof(AddEditItem), new PropertyMetadata(false));

        public bool IsEditEnabled
        {
            get => (bool)GetValue(IsEditEnabledProperty);
            set => SetValue(IsEditEnabledProperty, value);
        }

        public static readonly DependencyProperty IsEditEnabledProperty =
            DependencyProperty.Register(nameof(IsEditEnabled), typeof(bool), typeof(AddEditItem), new PropertyMetadata(false));

        public Visibility EditMode
        {
            get => (Visibility)GetValue(EditModeProperty);
            set => SetValue(EditModeProperty, value);
        }

        public static readonly DependencyProperty EditModeProperty =
            DependencyProperty.Register(nameof(EditMode), typeof(Visibility), typeof(AddEditItem), new PropertyMetadata(Visibility.Collapsed));

        public ObservableCollection<ISelectableItem> ItemsSource
        {
            get => (ObservableCollection<ISelectableItem>)GetValue(ListOfObjectsProperty);
            set => SetValue(ListOfObjectsProperty, value);
        }

        public static readonly DependencyProperty ListOfObjectsProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable<ISelectableItem>), typeof(AddEditItem), new PropertyMetadata(null));

        public ISelectableItem SelectedItem
        {
            get => (ISelectableItem)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(ISelectableItem), typeof(AddEditItem), new PropertyMetadata(null));

        public string AddedItemName
        {
            get => (string)GetValue(PlaceholderTextProperty);
            set => SetValue(PlaceholderTextProperty, value);
        }

        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(nameof(AddedItemName), typeof(string), typeof(AddEditItem), new PropertyMetadata(string.Empty));

        public ICommand SaveCommand
        {
            get => (ICommand)GetValue(SaveCommandProperty);
            set => SetValue(SaveCommandProperty, value);
        }

        public static readonly DependencyProperty SaveCommandProperty =
            DependencyProperty.Register(nameof(SaveCommand), typeof(ICommand), typeof(AddEditItem), new PropertyMetadata(null));

        public ICommand UpdateCommand
        {
            get => (ICommand)GetValue(UpdateCommandProperty);
            set => SetValue(UpdateCommandProperty, value);
        }

        public static readonly DependencyProperty UpdateCommandProperty =
            DependencyProperty.Register(nameof(UpdateCommand), typeof(ICommand), typeof(AddEditItem), new PropertyMetadata(null));

        public ICommand DeleteCommand
        {
            get => (ICommand)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }

        public static readonly DependencyProperty DeleteCommandProperty =
            DependencyProperty.Register(nameof(DeleteCommand), typeof(ICommand), typeof(AddEditItem), new PropertyMetadata(null));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(AddEditItem), new PropertyMetadata(string.Empty));

        #endregion

        #region Methods

        private void OpenEditMode(object sender, RoutedEventArgs e)
        {
            EditMode = Visibility.Visible;
            if (sender == EditButton)
            {
                AddedItemName = SelectedItem.Name;
            }
            if (sender == AddButton)
            {
                AddedItemName = string.Empty;
            }
            NameField.Focus();
        }

        private void CloseEditMode(object sender, RoutedEventArgs e)
        {
            EditMode = Visibility.Collapsed;
        }

        private void SourceItemControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedItem != null)
            {
                IsEditEnabled = true;
            }

            SelectionChanged?.Invoke(this, new RoutedEventArgs());
        }

        #endregion

        #region Shortcut commands

        [RelayCommand]
        public void OpenEditMode()
        {
            if (!ViewOnly)
            {
                OpenEditMode(AddButton, new());
            }
        }

        [RelayCommand]
        public void SaveChanges()
        {
            if (!ViewOnly)
            {
                UpdateCommand.Execute(this);
            }
        }

        [RelayCommand]
        public void CloseEditMode()
        {
            if (!ViewOnly)
            {
                CloseEditMode(this, new());
            }
        }

        [RelayCommand]
        public void CreateItem()
        {
            if (!ViewOnly && EditMode == Visibility.Visible)
            {
                SaveCommand.Execute(this);
                CloseEditMode();
            }
        }

        #endregion
    }
}
