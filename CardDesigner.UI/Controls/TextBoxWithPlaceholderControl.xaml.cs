using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CardDesigner.UI.Controls
{
    /// <summary>
    /// Interaction logic for TextBoxWithPlaceholder.xaml
    /// </summary>
    public partial class TextBoxWithPlaceholderControl : UserControl
    {
        public TextBoxWithPlaceholderControl()
        {
            InitializeComponent();
        }

        public string InputText
        {
            get => (string)GetValue(InputTextProperty);
            set => SetValue(InputTextProperty, value);
        }

        public static readonly DependencyProperty InputTextProperty =
            DependencyProperty.Register(nameof(InputText), typeof(string), typeof(TextBoxWithPlaceholderControl), new PropertyMetadata(string.Empty));

        public string PlaceholderText
        {
            get => (string)GetValue(PlaceholderTextProperty);
            set => SetValue(PlaceholderTextProperty, value);
        }

        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(nameof(PlaceholderText), typeof(string), typeof(TextBoxWithPlaceholderControl), new PropertyMetadata("Enter text.. (Ctrl+Wheel to resize)"));

        private void inputTextBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (e.Delta > 0)
                {
                    FontSize += .5;
                }
                else
                {
                    FontSize += -.5;
                }
                FontSize = Math.Clamp(FontSize, 8.0, 20.0);
            }
        }
    }
}
