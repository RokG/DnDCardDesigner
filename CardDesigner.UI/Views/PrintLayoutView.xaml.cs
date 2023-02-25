using CardDesigner.Domain.HelperModels;
using CardDesigner.UI.Controls;
using CardDesigner.UI.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
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
            string baseName = string.Empty;
            if (sender is Button button)
            {
                if (button.DataContext is PrintLayoutViewModel plvm)
                {
                    baseName = plvm.SelectedCharacter.Name + "_" + plvm.SelectedDeck.Name + "_" + DateTime.Now.ToString("yymmdd_hhmmss");
                }
            }

            SaveFileDialog dialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "pdf",
                FileName = baseName,
                Filter = "PDF Document (*.pdf)|*.pdf"
            };

            if (dialog.ShowDialog() == false)
            {
                return;
            }

            MemoryStream lMemoryStream = new MemoryStream();
            Package package = Package.Open(lMemoryStream, FileMode.Create);
            XpsDocument doc = new XpsDocument(package);
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);

            System.Windows.Documents.Serialization.SerializerWriterCollator collator = writer.CreateVisualsCollator();

            int sIdx = cardPages.SelectedIndex;
            int tabs = cardPages.Items.Count;

            // Loop over tab elements and write all pages
            collator.BeginBatchWrite();
            List<UIElement> list = new List<UIElement>();
            for (int i = 0; i < tabs; i++)
            {
                cardPages.SelectedIndex = i;
                AllowUIToUpdate();
                UIElement control = FindChild<CardPageControl>(cardPages, "cardPageControl");
                collator.Write(control);
                list.Add(control);
            }
            collator.EndBatchWrite();

            // Reslect last page
            cardPages.SelectedIndex = sIdx;

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

        private static void AllowUIToUpdate()
        {
            DispatcherFrame frame = new();
            // DispatcherPriority set to Input, the highest priority
            Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Input, new DispatcherOperationCallback(delegate (object parameter)
            {
                frame.Continue = false;
                Thread.Sleep(20); // Stop all processes to make sure the UI update is perform
                return null;
            }), null);
            Dispatcher.PushFrame(frame);
            // DispatcherPriority set to Input, the highest priority
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Input, new Action(delegate { }));
        }

        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null)
            {
                return null;
            }

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null)
                    {
                        break;
                    }
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    FrameworkElement frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }
    }

}
