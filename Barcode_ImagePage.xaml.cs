namespace Barcode_App3;

public partial class Barcode_Image : ContentView
{
    private byte[] _imageBytes;
    private string _barcodeValue;
    private string _barcodeType;

    public Barcode_Image(byte[] imageBytes, string value, string type)
	{
		InitializeComponent();


        _imageBytes = imageBytes;
        _barcodeValue = value;
        _barcodeType = type;

        // Display the barcode
        var stream = new MemoryStream(imageBytes);
        barcodeImage.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));

    }
}