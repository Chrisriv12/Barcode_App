using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace Barcode_App3;

public partial class Scan_Barcode : ContentPage
{
    public Scan_Barcode()
    {
        InitializeComponent();
    }

    private void CameraBarcodeReaderView_CamerasLoaded(object sender, EventArgs e)
    {
        // The CameraBarcodeReaderView type does not have StartCameraAsync/StopCameraAsync methods.
        // To start/stop the camera, set IsDetecting to true/false.

        MainThread.BeginInvokeOnMainThread(() =>
        {
            cameraView.IsDetecting = false;
            cameraView.IsDetecting = true;
        });
    }

    private void CameraBarcodeReaderView_BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (e.Results != null && e.Results.Length > 0)
            {
                barcodeResult.Text = $"{e.Results[0].Format}: {e.Results[0].Value}";
            }
        });
    }
}
