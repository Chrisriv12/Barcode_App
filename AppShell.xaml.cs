using Microsoft.Maui.Controls;

namespace Barcode_App3
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register routes for pages you want to navigate to via Shell
            Routing.RegisterRoute(nameof(Generate_Barcode), typeof(Generate_Barcode));
            Routing.RegisterRoute(nameof(Scan_Barcode), typeof(Scan_Barcode));
            Routing.RegisterRoute(nameof(QR_Generator), typeof(QR_Generator));
        }
    }
}
