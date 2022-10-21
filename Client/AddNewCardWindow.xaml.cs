using Client.Interfaces;
using Client.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using System.IO;
using System.Net.Http;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Client
{
    public partial class AddNewCardWindow : Window
    {
        private string path = null;
        private string fileName = null;
        private string _id = null;
        private string _info = null;
        private readonly IInfoCardsService _service;
        private readonly MainWindow _mainWindow;

        public AddNewCardWindow(string id, string info, IInfoCardsService service, MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _id = id;
            _info = info;
            _service = service;
            InitializeComponent();
        }

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == true)
            {
                fileName = dlg.SafeFileName;
                path = dlg.FileName;

                using (Stream stream = dlg.OpenFile())
                {
                    var bitmap = new BitmapImage();

                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                    bitmap.Freeze();
                    ImagePhotoShow.Source = bitmap;
                }
            }
        }

        private void ButtonUpload_Click(object sender, RoutedEventArgs e)
        {
            if (path == null || path == "")
            {
                MessageBox.Show("Pleas select image");
                return;
            }

            if (TextBoxInfo.Text == "" || TextBoxInfo.Text == null)
            {
                MessageBox.Show("Pleas write descriotion");
                return;
            }

            if (_id == null)
                _service.AddCard(new AddCardParams { info = TextBoxInfo.Text, path = path, fileName = fileName });
            else
                _service.UpadateCard(new UpadateCardParams { id = _id, info = TextBoxInfo.Text, path = path, fileName = fileName });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _mainWindow.Show();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_id != null)
            {
                Title = "Edit info card";
                TextBoxInfo.Text = _info;
                ButtonUpload.Content = "Edit info card";

                ImagePhotoShow.Source = await _service.UploadImage(_id);
            }
            else 
            {
                Title = "Add card";
            }
        }
    }
}
