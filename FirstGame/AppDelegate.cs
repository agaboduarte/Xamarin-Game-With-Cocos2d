using System.Reflection;
using Microsoft.Xna.Framework;
using Cocos2D;
using CocosDenshion;

namespace FirstGame
{
    public class AppDelegate : CCApplication
    {
        public AppDelegate(Game game, GraphicsDeviceManager graphics)
            : base(game, graphics)
        {
            CCDrawManager.InitializeDisplay(game, graphics, DisplayOrientation.Default);
        }

        /// <summary>
        /// Implement for initialize OpenGL instance, set source path, etc...
        /// </summary>
        public override bool InitInstance()
        {
            return base.InitInstance();
        }

        /// <summary>
        ///  Implement CCDirector and CCScene init code here.
        /// </summary>
        /// <returns>
        ///  true  Initialize success, app continue.
        ///  false Initialize failed, app terminate.
        /// </returns>
        public override bool ApplicationDidFinishLaunching()
        {
            CCDirector.SharedDirector.SetOpenGlView();
            CCDirector.SharedDirector.Projection = CCDirectorProjection.Projection2D;
            CCDirector.SharedDirector.RunWithScene(IntroLayer.Scene);

            CCDrawManager.SetDesignResolutionSize(width: 480, height: 320, resolutionPolicy: CCResolutionPolicy.FixedHeight);

            return true;
        }

        /// <summary>
        /// The function be called when the application enter background
        /// </summary>
        public override void ApplicationDidEnterBackground()
        {
            CCDirector.SharedDirector.Pause();

            // if you use SimpleAudioEngine, it must be pause
            //CCSimpleAudioEngine.SharedEngine.PauseBackgroundMusic = true;
        }

        /// <summary>
        /// The function be called when the application enter foreground  
        /// </summary>
        public override void ApplicationWillEnterForeground()
        {
            CCDirector.SharedDirector.Resume();

            // if you use SimpleAudioEngine, it must resume here
            //CCSimpleAudioEngine.SharedEngine.PauseBackgroundMusic = false;

        }
    }
}