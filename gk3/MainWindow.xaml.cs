using System;
using System.Collections.Generic;
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
using System.Windows.Forms;
using System.Drawing;

namespace gk3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Bitmap image;
        private Bitmap convertedImage;
        public MainWindow()
        {
            InitializeComponent();
            ErrorDiffusionDitheringComboBox.ItemsSource = new string[] { "Floyd and Steinberg", "Burkes", "Stucky" };
            ErrorDiffusionDitheringComboBox.SelectedIndex = 0;
        }

        private void LoadImageButton_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Title = "Load Texture";
            fileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            fileDialog.FilterIndex = 4;
            fileDialog.RestoreDirectory = true;
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                image = new Bitmap(fileDialog.FileName);
                //OriginalImage.MinWidth = image.Width;
                //OriginalImage.MinHeight = image.Height;
                OriginalImage.Source = BitmapConverter.ConvertBitmapToSource(image);
            }
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            if (image == null)
                return;
            int n;
            int.TryParse(NumberOfColors.Text, out n);
            if(n==0 || n<2)
            {
                return;
            }
            if(n<1)
            {
                n = 1;
            }
            if(ErrorDiffusionDitheringRadioButton.IsChecked.Value)
            {
                convertedImage = ErrorDiffusionDithering.ReduceColors(image, (FilterMatrix)ErrorDiffusionDitheringComboBox.SelectedIndex, n, true);
                //ConvertedImage.Width = convertedImage.Width;
                //ConvertedImage.Height = convertedImage.Height;
            }
            else if(PopularityAlgorithmRadioButton.IsChecked.Value)
            {
                convertedImage = PopularityAlgorithm.ReduceColors(image, n);
            }
            else if (KMeansAlgorithmRadioButton.IsChecked.Value)
            {
                convertedImage = KMeansAlgorithm.ReduceColors(image, n);
            }
            ConvertedImage.Source = BitmapConverter.ConvertBitmapToSource(convertedImage);
        }
    }
}
