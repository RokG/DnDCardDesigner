using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CardDesigner.UI.Controls
{
    /// <summary>
    /// Interaction logic for FilePicker.xaml
    /// </summary>
    public partial class FilePicker : UserControl
    {
        public FilePicker()
        {
            InitializeComponent();
        }

        //https://stackoverflow.com/questions/1922204/open-directory-dialog
        
        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public static readonly DependencyProperty DescriptionProperty =
                DependencyProperty.Register(nameof(Description), typeof(string), typeof(FilePicker), new PropertyMetadata(string.Empty));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
                DependencyProperty.Register(nameof(Text), typeof(string), typeof(FilePicker), new PropertyMetadata(string.Empty));

        private void BrowseFolder(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                Text = openFileDialog.FileName;
                BindingExpression be = GetBindingExpression(TextProperty);
                if (be != null)
                {
                    be.UpdateSource();
                }
            }

            //using (OpenFileDialog dlg = new OpenFileDialog())
            //{
            //    DialogResult result = dlg.ShowDialog();
            //    if (result == System.Windows.Forms.DialogResult.OK)
            //    {
            //        Text = dlg.FileName;
            //        BindingExpression be = GetBindingExpression(TextProperty);
            //        if (be != null)
            //        {
            //            be.UpdateSource();
            //        }
            //    }
            //}
        }
    }
}
