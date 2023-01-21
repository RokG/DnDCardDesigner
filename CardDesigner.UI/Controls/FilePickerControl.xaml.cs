using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

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

        // Using a DependencyProperty as the backing store for Description.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DescriptionProperty =
                DependencyProperty.Register("Description", typeof(string), typeof(FilePicker), new PropertyMetadata(string.Empty));


        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
                DependencyProperty.Register("Text", typeof(string), typeof(FilePicker), new PropertyMetadata(string.Empty));



        private void BrowseFolder(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            string file = string.Empty;
            if (openFileDialog.ShowDialog() == true)
            {
                file = openFileDialog.FileName;
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
