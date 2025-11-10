using System.IO;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;
using SkiaSharp;
using CommunityToolkit.Maui.Extensions;

namespace Barcode_App3;

public partial class QR_Generator : ContentPage
{
	public QR_Generator()
	{
		InitializeComponent();
	}

	private async void GenerateQRCode(object sender, EventArgs e)
	{
		string text = QRCodeEntry?.Text;

		if (string.IsNullOrWhiteSpace(text))
		{
			await DisplayAlert("Error", "Please enter a value for the QR image.", "OK");
			return;
		}

		// Use QR format directly (was using undefined 'format' variable)
		var writer = new BarcodeWriter<SKBitmap>
		{
			Format = BarcodeFormat.QR_CODE,
			Options = new EncodingOptions
			{
				Width = 300,
				Height = 300,
				Margin = 1
			},
			Renderer = new SKBitmapRenderer()
		};

		var bitmap = writer.Write(text);

		using var image = SKImage.FromBitmap(bitmap);
		using var data = image.Encode(SKEncodedImageFormat.Png, 100);

        // Copy encoded bytes into a MemoryStream so the stream stays alive for the ImageSource
        var memoryStream = new MemoryStream();
        data.SaveTo(memoryStream);
        memoryStream.Position = 0;

		//Convert to ImageSource for the data stream 
		var imageSource = ImageSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));

		//Show popup and pass QR code image plus the value 
		var popup = new QR_ImagePage(imageSource, text, memoryStream);
		await this.ShowPopupAsync(popup);
	}

	// Custom renderer for SkiaSharp
	public class SKBitmapRenderer : IBarcodeRenderer<SKBitmap>
	{
		public SKBitmap Render(BitMatrix matrix, BarcodeFormat format, string content)
		{
			return Render(matrix, format, content, new EncodingOptions());
		}

		public SKBitmap Render(BitMatrix matrix, BarcodeFormat format, string content, EncodingOptions options)
		{
			int width = matrix.Width;
			int height = matrix.Height;

			var bitmap = new SKBitmap(width, height);

			var foreground = SKColors.Black;
			var background = SKColors.White;

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					bitmap.SetPixel(x, y, matrix[x, y] ? foreground : background);
				}
			}

			return bitmap;
		}
	}
}