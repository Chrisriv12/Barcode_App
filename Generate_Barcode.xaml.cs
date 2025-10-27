// Generate_Barcode.xaml.cs
using ZXing;
using ZXing.Common;
using ZXing.SkiaSharp;
using ZXing.SkiaSharp.Rendering;

namespace Barcode_App3
{
    public partial class Generate_Barcode : ContentPage
    {
        public Generate_Barcode()
        {
            InitializeComponent();
            barcodeList.SelectedIndex = 0;
        }

        private async void GenerateBarcodeImage(object sender, EventArgs e)
        {
            string text = barcodeEntry.Text;

            if (string.IsNullOrWhiteSpace(text))
            {
                await DisplayAlert("Error", "Please enter a value for the barcode.", "OK");
                return;
            }

            if (barcodeList.SelectedItem == null)
            {
                await DisplayAlert("Error", "Please select a barcode type.", "OK");
                return;
            }

            string selectedType = barcodeList.SelectedItem.ToString();

            // Convert picker value to ZXing BarcodeFormat
            var format = selectedType switch
            {
                "Code128" => BarcodeFormat.CODE_128,
                "Code39" => BarcodeFormat.CODE_39,
                "UPC_A" => BarcodeFormat.UPC_A,
                "EAN13" => BarcodeFormat.EAN_13,
                "ITF" => BarcodeFormat.ITF,
                "QR_CODE" => BarcodeFormat.QR_CODE,
                _ => BarcodeFormat.CODE_128
            };

            try
            {
                // Create barcode writer with SkiaSharp renderer
                var writer = new BarcodeWriter<SkiaSharp.SKBitmap>
                {
                    Format = format,
                    Options = new EncodingOptions
                    {
                        Width = 300,
                        Height = 150,
                        Margin = 10
                    },
                    Renderer = new SKBitmapRenderer()
                };

                // Generate barcode as SKBitmap
                var bitmap = writer.Write(text);

                // Convert SKBitmap to Stream for MAUI Image
                using var image = SkiaSharp.SKImage.FromBitmap(bitmap);
                using var data = image.Encode(SkiaSharp.SKEncodedImageFormat.Png, 100);
                var stream = data.AsStream();

                barcodeImage.Source = ImageSource.FromStream(() => stream);
                statusLabel.Text = $"Generated {selectedType} barcode";
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to generate barcode: {ex.Message}", "OK");
            }
        }
    }
}