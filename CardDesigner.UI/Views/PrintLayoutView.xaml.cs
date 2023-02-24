using System;
using System.IO.Packaging;
using System.IO;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CardDesigner.Domain.Enums;
using Microsoft.Win32;
using System.Xml.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Drawing.Printing;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //// Printa v kvadrat?
            /*
            *  Convert WPF -> XPS -> PDF
            */
            MemoryStream lMemoryStream = new MemoryStream();
            Package package = Package.Open(lMemoryStream, FileMode.Create);
            XpsDocument doc = new XpsDocument(package);
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);

            // This is your window
            writer.Write(itemCardsToPDF);

            doc.Close();
            package.Close();

            // Convert 
            MemoryStream outStream = new MemoryStream();
            PdfSharp.Xps.XpsConverter.Convert(lMemoryStream, outStream, false);

            // Write pdf file
            FileStream fileStream = new FileStream("E:\\test1" +DateTime.Now.ToString("yyyy_mm_dd_hh_mm_ss")+ ".pdf", FileMode.Create);
            outStream.CopyTo(fileStream);

            // Clean up
            outStream.Flush();
            outStream.Close();
            fileStream.Flush();
            fileStream.Close();
        }
    }
}
