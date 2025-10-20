using Microsoft.Maui.Controls;

namespace Barcode_App3
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnScanClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(Scan_Barcode));
        }

        private async void OnQrClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(QR_Generator));
        }

        private async void OnGenerateClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(Generate_Barcode));
        }
    }
}