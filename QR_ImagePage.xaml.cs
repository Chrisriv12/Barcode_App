namespace Barcode_App3;
using Barcode_App3.Helpers; // <-- include the helper
using Camera.MAUI;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Storage;
using System;
using System.IO;
using System.Threading.Tasks;

public partial class QR_ImagePage : Popup
{
	private readonly Stream QRcode_Stream;
	private readonly string QRcode_Value;

	public QR_ImagePage(ImageSource qrSource, string qrCodeValue, Stream qrCodeStream)
	{
		InitializeComponent();

		QRcode_Stream = qrCodeStream;
		QRcode_Value = qrCodeValue;
        QRcodeImage.Source = qrSource;
        qrCodeValueLabel.Text = qrCodeValue;

    }

	private async void OnQrSaveClicked(object sender, EventArgs e)
	{
		try
		{
			QRcode_Stream.Position = 0;
			string fileName = $"QRCode_{DateTime.Now:yyyyMMdd_HHmmss}.png";

			string savedPath = await FileHelper.SaveBarcodeAsync(QRcode_Stream, fileName);

			await Shell.Current.DisplayAlert("Success", $"QR Code saved to:\n{savedPath}", "OK");

			await Share.RequestAsync(new ShareFileRequest
			{
				Title = "Share QR Code",
				File = new ShareFile(savedPath)
			});
		}
		catch (Exception ex) {
			await Shell.Current.DisplayAlert("Error", $"QR code not saved: {ex.Message}", "OK");


        }
	}
}