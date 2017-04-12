using System;
using Microsoft.Xna.Framework;
using Cocos2D;

namespace FirstGame
{
    public class GameLevel : CCLayer
    {
        static GameLevel layerInstance;

        bool openWithMenuInsteadOfGame;

        // Menu
        CCPoint menuStartPosition;

        //menu button
        CCMenu MenuButton;

        public GameLevel()
        {
            layerInstance = this;

            // enable touches
#if XBOX || OUYA
            TouchEnabled = false;
            GamePadEnabled = true;
#else
            TouchEnabled = true;
#endif

            // enable accelerometer
            AccelerometerEnabled = false;

            // ask director for the window size
            var screenSize = CCDirector.SharedDirector.WinSize;

            // create and initialize a Label
            var label = new CCLabelTTF("Game Layer", "MarkerFelt", 22);

            // position the label on the center of the screen
            label.Position = CCDirector.SharedDirector.WinSize.Center;

            // add the label as a child to this Layer
            AddChild(label);

            menuStartPosition = new CCPoint(70, screenSize.Height - 24);

            var gameMenuLabel = new CCLabelTTF("Game Menu", "MarkerFelt", 18);
            var button1 = new CCMenuItemLabel(gameMenuLabel, ShowMenu);

            MenuButton = new CCMenu(button1);
            MenuButton.Position = menuStartPosition;

            AddChild(MenuButton, 10);

            openWithMenuInsteadOfGame = false;

            if (GameData.SharedData.FirstRunEver && openWithMenuInsteadOfGame)
            {
                CCLog.Log("First run ever");
                Schedule(ShowMenuFromSelector, 2f);
                GameData.SharedData.FirstRunEver = false;
            }
        }

        public static GameLevel SharedLevel
        {
            get
            {
                return layerInstance;
            }
        }

        void ShowMenuFromSelector(float dt)
        {
            ShowMenu(null);
        }

        void ShowMenu(object sender)
        {
            CCLog.Log("Show Menu");
            CCDirector.SharedDirector.PushScene(GameMenu.Scene);
        }

        public static CCScene Scene
        {
            get
            {
                var scene = new CCScene();

                var layer = new GameLevel();

                // add layer as a child to scene
                scene.AddChild(layer);

                // return the scene
                return scene;
            }
        }

        public void TransitionAfterMenuPop()
        {
            //transition upon coming back from the menu
            UnscheduleAllSelectors();

            ScheduleOnce(TransitionOut, 0.1f);
        }

        void TransitionOut(float delta)
        {
            var transition = new CCTransitionFadeDown(1, GameLevel.Scene);

            CCDirector.SharedDirector.ReplaceScene(transition);
        }
    }
}

