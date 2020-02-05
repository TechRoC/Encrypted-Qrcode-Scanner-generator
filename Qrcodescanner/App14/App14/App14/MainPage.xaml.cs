using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;
using System.Security.Cryptography;

namespace App14
{
    public partial class MainPage : ContentPage
    {
        private static string Key = "Rohit[Dogs{Dik}]Rahul[Fish{Sap}]";
        private static string IV = "Akshay[Dogs{Ros}";
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var options = new MobileBarcodeScanningOptions
            {

                AutoRotate = true,
                UseFrontCameraIfAvailable = false,
                TryHarder = true,
                PossibleFormats = new List<ZXing.BarcodeFormat>
                {
                    ZXing.BarcodeFormat.QR_CODE , ZXing.BarcodeFormat.QR_CODE
                }
            };
            var scan = new ZXingScannerPage(options);
            await Navigation.PushAsync(scan);
           
            scan.OnScanResult += (result) =>
              {
                  Device.BeginInvokeOnMainThread(async () =>
                 {
                     animationView.IsVisible = false;
                     b1.IsVisible = false;
                     await Navigation.PopAsync();
                     try
                     {
                         var url = Decrypt(result.Text);
                         Browser.Source = url;
                     }
                     catch(Exception ex)
                     {
                         await DisplayAlert("Qr_code", "This is not the required Qr_code", "ok");
                         animationView.IsVisible = true;
                         b1.IsVisible = true;
                     }
                    
                     
                 });
              };
            
          
           
           
            
            
        }
        public static string Decrypt(string encrypted)
        {
            byte[] encryptedbytes = Convert.FromBase64String(encrypted);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(Key);
            aes.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV);
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            ICryptoTransform crypto = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] secret = crypto.TransformFinalBlock(encryptedbytes, 0, encryptedbytes.Length);
            return System.Text.ASCIIEncoding.ASCII.GetString(secret);
        }
    }
}
