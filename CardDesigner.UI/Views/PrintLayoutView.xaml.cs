using CardDesigner.Domain.HelperModels;
using CardDesigner.UI.ViewModels;
using Microsoft.Win32;
using PdfSharp.Drawing;
using System;
using System.IO;
using System.IO.Packaging;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace CardDesigner.UI.Views
{
    /// <summary>
    /// Interaction logic for PrintLayoutView.xaml
    /// </summary>
    public partial class PrintLayoutView : UserControl
    {
        public PrintLayoutView()
        {
            InitializeComponent();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (sender is TreeView treeView)
            {
                if (treeView.DataContext is PrintLayoutViewModel plvm)
                {
                    plvm.SetSelectedItem((TreeItemModel)treeView.SelectedItem);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();

            dialog.AddExtension = true;
            dialog.DefaultExt = "pdf";
            dialog.Filter = "PDF Document (*.pdf)|*.pdf";

            if (dialog.ShowDialog() == false)
                return;

            MemoryStream lMemoryStream = new MemoryStream();
            Package package = Package.Open(lMemoryStream, FileMode.Create);
            XpsDocument doc = new XpsDocument(package);
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);

            var collator = writer.CreateVisualsCollator();

            // Have to navigate tabs to create pages

            collator.BeginBatchWrite();
            collator.Write(itemCardsToPDF);
            collator.Write(spellCardsToPDF);
            collator.EndBatchWrite();

            doc.Close();
            package.Close();

            // Convert 
            MemoryStream outStream = new MemoryStream();
            PdfSharp.Xps.XpsConverter.Convert(lMemoryStream, outStream, false);

            // Write pdf file
            FileStream fileStream = new FileStream(dialog.FileName, FileMode.Create);
            outStream.CopyTo(fileStream);

            // Clean up
            outStream.Flush();
            outStream.Close();
            fileStream.Flush();
            fileStream.Close();
        }

    }
}
