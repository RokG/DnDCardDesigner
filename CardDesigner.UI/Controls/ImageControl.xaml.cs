﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace CardDesigner.UI.Controls
{
    /// <summary>
    /// Interaction logic for ImageControl.xaml
    /// </summary>
    public partial class ImageControl : UserControl
    {
        public ImageControl()
        {
            InitializeComponent();
        }

        public Uri ImageSource
        {
            get { return (Uri)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register(nameof(ImageSource), typeof(Uri), typeof(ImageControl), new PropertyMetadata(new Uri("\\Resources\\Images\\dnd-icon-13.png", UriKind.Relative), PropChange));

        public Stretch ImageStretch
        {
            get { return (Stretch)GetValue(ImageStretchProperty); }
            set { SetValue(ImageStretchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageStretch.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageStretchProperty =
            DependencyProperty.Register(nameof(ImageStretch), typeof(Stretch), typeof(ImageControl), new PropertyMetadata(Stretch.Uniform, PropChange));

        private static void PropChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is ImageControl imageControl)
            {
                if (imageControl.ImageStretch == Stretch.UniformToFill)
                {
                    double imageWidth = imageControl.itemImage.ActualWidth;
                    double imageHeight = imageControl.itemImage.ActualHeight;
                    double canvasWidth = imageControl.ActualWidth;
                    double canvasHeight = imageControl.ActualHeight;

                    if ((imageHeight == 0 || imageWidth == 0) && !(imageControl.ImageSource == null))
                    {
                        // Uri was changed but image did not yet load
                        BitmapImage image = new BitmapImage(imageControl.ImageSource);
                        imageWidth = image.Width;
                        imageHeight = image.Height;
                    }
                    else
                    {
                        imageWidth = 1000;
                        imageHeight = 1000;
                    }

                    double offsetX = 0;
                    double offsetY = 0;

                    double imageRatioX = imageHeight / imageWidth;
                    double imageRatioY = canvasHeight / canvasWidth;


                    if (imageRatioX > imageRatioY) 
                    {
                        // Fit to Width
                        offsetY = (canvasHeight - imageHeight) / 2;
                    }
                    else
                    {
                        // Fit to Height
                        offsetX = (canvasWidth - imageWidth) / 2;
                    }

                    imageControl.itemImage.RenderTransform = new TranslateTransform(offsetX, offsetY);
                }
                else
                {
                    imageControl.itemImage.RenderTransform = new ScaleTransform(1, 1);
                }
            }
        }

    }
}