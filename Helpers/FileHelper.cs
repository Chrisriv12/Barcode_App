using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace Barcode_App3.Helpers
{

    public static class FileHelper
    {
        public static async Task<string> SaveBarcodeAsync(Stream barcodeStream, string fileName)
        {
            string filePath = string.Empty;

            barcodeStream.Position = 0; // reset stream

#if ANDROID
        // Public Pictures folder
        var picturesPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures).AbsolutePath;
        filePath = Path.Combine(picturesPath, fileName);

        using var fileStream = File.Create(filePath);
        await barcodeStream.CopyToAsync(fileStream);

#elif IOS || MACCATALYST
            // Use Photos or Documents folder
            var documents = FileSystem.AppDataDirectory;
            filePath = Path.Combine(documents, fileName);

            using var fileStream = File.Create(filePath);
            await barcodeStream.CopyToAsync(fileStream);

#elif WINDOWS
        // Use Pictures library
        var pictures = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
        filePath = Path.Combine(pictures, fileName);

        using var fileStream = File.Create(filePath);
        await barcodeStream.CopyToAsync(fileStream);

#else
        // fallback
        filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
        using var fileStream = File.Create(filePath);
        await barcodeStream.CopyToAsync(fileStream);
#endif

            return filePath;
        }
    }
}