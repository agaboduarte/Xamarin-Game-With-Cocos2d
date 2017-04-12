using Android.App;
using Android.Widget;
using Android.OS;
using Microsoft.Xna.Framework;
using Android.Views;
using Android.Content.PM;

namespace FirstGame.Android
{
    [Activity(Label = "FirstGame.Android", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Landscape)]
    public class MainActivity : AndroidGameActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Game game = new MyGame();

            var frameLayout = new FrameLayout(this);

            frameLayout.AddView(game.Services.GetService<View>());

            SetContentView(frameLayout);

            game.Run(GameRunBehavior.Asynchronous);
        }
    }
}

