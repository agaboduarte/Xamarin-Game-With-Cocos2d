using Android.App;
using Android.Widget;
using Android.OS;
using Microsoft.Xna.Framework;
using Android.Views;
using Android.Content.PM;

namespace FirstGame.Android
{
    [Activity(
        Label = "FirstGame.Android", 
        MainLauncher = true, 
        Icon = "@drawable/icon",
        Theme = "@android:style/Theme.NoTitleBar.Fullscreen",
        AlwaysRetainTaskState = true,
        LaunchMode = LaunchMode.SingleInstance,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize,
        ScreenOrientation = ScreenOrientation.SensorLandscape)]
    public class MainActivity : AndroidGameActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var game = new MyGame();

            SetContentView(game.Services.GetService<View>());

            game.Run();
        }
    }
}

