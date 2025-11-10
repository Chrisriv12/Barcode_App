using CommunityToolkit.Maui.Views;
using Barcode_App3.Helpers; // <-- include the helper
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace Barcode_App3
{
    public partial class Barcode_ImagePage : Popup
    {
        private readonly Stream barcode_Stream;
        private readonly string barcode_Value;

        public Barcode_ImagePage(ImageSource barcodeSource, string barcodeValue, Stream barcodeStream)
        {
            InitializeComponent();

            barcode_Stream = barcodeStream;
            barcode_Value = barcodeValue;
            barcodeImage.Source = barcodeSource;
            barcodeValueLabel.Text = barcodeValue;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                string fileName = $"Barcode_{DateTime.Now:yyyyMMdd_HHmmss}.png";

                // ✅ Use the cross-platform helper
                string savedPath = await FileHelper.SaveBarcodeAsync(barcode_Stream, fileName);

                await Shell.Current.DisplayAlert("Success", $"Barcode saved to:\n{savedPath}", "OK");

                // Optional: Share the file
                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Share Barcode",
                    File = new ShareFile(savedPath)
                });
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Barcode not saved: {ex.Message}", "OK");
            }
        }
    }
}

