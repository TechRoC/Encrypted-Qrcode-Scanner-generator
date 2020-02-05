using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Lottie.Forms.Droid;

namespace App14.Droid
{
    [Activity(Label = "App14", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        readonly string[] Permission =
            {
                Android.Manifest.Permission.Camera
            };
        const int RequestId = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            RequestPermissions(Permission, RequestId);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            AnimationViewRenderer.Init();
            int uiOptions = (int)Window.DecorView.SystemUiVisibility;
            //uiOptions |= (int)SystemUiFlags.LowProfile;
            uiOptions |= (int)SystemUiFlags.HideNavigation;
            uiOptions |= (int)SystemUiFlags.ImmersiveSticky;
            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        bool doubleClick = false;
        public override void OnBackPressed()
        {
            if (doubleClick)
            {
                base.OnBackPressed();
                Java.Lang.JavaSystem.Exit(0);
                return;
            }
            this.doubleClick = true;
            Toast.MakeText(this, "Please tab twice to exit", ToastLength.Long).Show();
            new Handler().PostDelayed(() => { doubleClick = false; }, 2000);
        }
       
    }
}