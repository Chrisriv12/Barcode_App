.NET MAUI Barcode & QR Code Generator
Overview

This cross-platform mobile application allows users to generate and save both barcodes and QR codes. Built using .NET MAUI, it runs seamlessly on Android, iOS, Windows, and macOS. Users can input text, generate corresponding codes, and save them as image files to their device storage.

Features

Generate barcodes and QR codes from user input.

Save images to device storage using platform-specific logic.

Built with the CommunityToolkit.Maui for enhanced UI and functionality.

Supports cross-platform saving paths.

Uses helper methods to manage file saving operations cleanly.

Technologies Used

.NET MAUI

CommunityToolkit.Maui

ZXing.Net.Maui for barcode and QR generation

C# for logic and helper functions

XAML for UI layout

Project Structure
Barcode_App3/
│
├── Helpers/
│   ├── FileHelper.cs        # Handles saving barcode/QR code images to local storage
│
├── Views/
│   ├── Barcode_GeneratorPage.xaml
│   ├── QR_ImagePage.xaml
│
├── App.xaml.cs
├── MainPage.xaml
└── MainPage.xaml.cs

How It Works
1. Barcode Generation

The user inputs text.

The app generates a barcode image.

The barcode can be saved using the SaveBarcodeAsync() helper method, which writes the image stream to a cross-platform file path.

2. QR Code Generation

The user inputs text.

The app generates a QR code image.

The same saving logic as barcodes is applied via SaveQrCodeAsync(), which stores the file based on the current platform.

Helper Logic (FileHelper.cs)

The FileHelper class ensures files are saved properly on each platform:

Android: Saves under Android.OS.Environment.DirectoryPictures/Barcodes/.

iOS/macOS: Uses the FileSystem.AppDataDirectory.

Windows: Uses the Environment.SpecialFolder.MyPictures folder.

Each saved image returns a full file path that’s displayed to the user via a success alert.
