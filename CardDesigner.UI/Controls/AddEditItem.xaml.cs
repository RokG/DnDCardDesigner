using CardDesigner.Domain.Interfaces;
using CardDesigner.Domain.Models;
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
        }

        public Visibility EditMode
        {
            get => (Visibility)GetValue(EditModeProperty);
            set => SetValue(EditModeProperty, value);
        }

        public static readonly DependencyProperty EditModeProperty =
            DependencyProperty.Register(nameof(EditMode), typeof(Visibility), typeof(AddEditItem), new PropertyMetadata(Visibility.Collapsed));

        public ObservableCollection<CharacterModel> ItemsSource
        {
            get => (ObservableCollection<CharacterModel>)GetValue(ListOfObjectsProperty);
            set => SetValue(ListOfObjectsProperty, value);
        }

        public static readonly DependencyProperty ListOfObjectsProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable<CharacterModel>), typeof(AddEditItem), new PropertyMetadata(null));

        public CharacterModel SelectedItem
        {
            get => (CharacterModel)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(CharacterModel), typeof(AddEditItem), new PropertyMetadata(null));

        public string PlaceholderText
        {
            get => (string)GetValue(PlaceholderTextProperty);
            set => SetValue(PlaceholderTextProperty, value);
        }

        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(nameof(PlaceholderText), typeof(string), typeof(AddEditItem), new PropertyMetadata(string.Empty));

        public ICommand SaveCommand
        {
            get => (ICommand)GetValue(SaveCommandProperty);
            set => SetValue(SaveCommandProperty, value);
        }

        public static readonly DependencyProperty SaveCommandProperty =
            DependencyProperty.Register(nameof(SaveCommand), typeof(ICommand), typeof(AddEditItem), new PropertyMetadata(null));

        public ISelectableItem SelectedItemMid { get; set; }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(AddEditItem), new PropertyMetadata(string.Empty));

        private void OpenEditMode(object sender, RoutedEventArgs e)
        {
            EditMode = Visibility.Visible;
            if (sender == EditButton)
            {
                PlaceholderText = SelectedItem.Name;
            }
            if (sender == AddButton)
            {
                PlaceholderText = string.Empty;
            }
        }

        private void CloseEditMode(object sender, RoutedEventArgs e)
        {
            EditMode = Visibility.Collapsed;
        }
    }
}
