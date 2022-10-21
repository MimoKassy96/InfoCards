using Client.Interfaces;
using Client.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace Client
{
    public partial class MainWindow : Window
    {
        private readonly IInfoCardsService _service;
        private List<InfoCardModel> listInfoCards = new List<InfoCardModel>();

        public MainWindow(IInfoCardsService service)
        {
            InitializeComponent();
            _service = service;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listInfoCards = await _service.GetInfoAsync();

            if (listInfoCards.Count == 0)
                DataGridInfoCards.ItemsSource = null;
            else
                DataGridInfoCards.ItemsSource = listInfoCards;
        }

        private void ButtonAddInfoCard_Click(object sender, RoutedEventArgs e)
        {
            string id = null;
            string info = null;
            var addCardWindow = new AddNewCardWindow(id, info, _service, new MainWindow(_service));

            addCardWindow.Show();
            Close();
        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridInfoCards.SelectedCells.Count == 0)
            {
                MessageBox.Show("Please select card");
                return;
            }

            string header;
            string id = null;
            string info = null;

            for (int i = 0; i < DataGridInfoCards.SelectedCells.Count; i++)
            {
                header = DataGridInfoCards.SelectedCells[i].Column.Header.ToString();
                if (header == "Info")
                {
                    info = (DataGridInfoCards.SelectedCells[i].Column.GetCellContent(DataGridInfoCards.SelectedCells[i].Item) as TextBlock).Text;
                }
                else { id = (DataGridInfoCards.SelectedCells[i].Column.GetCellContent(DataGridInfoCards.SelectedCells[i].Item) as TextBlock).Text; }
            }

            var addNewCardWindow = new AddNewCardWindow(id, info, _service, new MainWindow(_service));

            addNewCardWindow.Show();
            Close();
        }

        private async void Button_Upload_Click(object sender, RoutedEventArgs e)
        {
            var cellInfo = DataGridInfoCards.SelectedCells[0];
            var id = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;

            photos_show.Source = await _service.UploadImage(id);
        }

        private async void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            var cellInfo = DataGridInfoCards.SelectedCells[0];
            var id = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;

            listInfoCards = await _service.DeleteInfoCard(id);

            DataGridInfoCards.ItemsSource = listInfoCards;
            DataGridInfoCards.Items.Refresh();
            photos_show.Source = null;
            MessageBox.Show("Deletion was successful");
        }
    }
}
