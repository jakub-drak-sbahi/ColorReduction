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
        public MainWindow()
        {
            InitializeComponent();
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
                OriginalImage.Width = image.Width;
                OriginalImage.Height = image.Height;
                OriginalImage.Source = BitmapConverter.ConvertBitmapToSource(image);
            }
        }
    }
}
