using System;

using Microsoft.Xna.Framework;
using Cocos2D;
using System.Diagnostics;

namespace FirstGame
{
    public class GameMenu : CCLayer, IGameLoop
    {
        CCMenu voiceFXMenu;
        CCMenu soundFXMenu;
        CCMenu ambientFXMenu;

        CCLabelTTF backLabel;
        CCMenu backMenu;

        CCPoint voiceFXMenuLocation;
        CCPoint soundFXMenuLocation;
        CCPoint ambientFXMenuLocation;

        string voiceButtonName;
        string voiceButtonNameDim;

        string soundButtonName;
        string soundButtonNameDim;

        string ambientButtonName;
        string ambientButtonNameDim;

        Stopwatch time;

        public GameMenu()
        {
            voiceButtonName = "VoiceFX";
            voiceButtonNameDim = "VoiceFX";

            soundButtonName = "SoundFX";
            soundButtonNameDim = "SoundFX";

            ambientButtonName = "AmbientFX";
            ambientButtonNameDim = "AmbientFX";

            soundFXMenuLocation = new CCPoint(110, 55);
            voiceFXMenuLocation = new CCPoint(230, 55);
            ambientFXMenuLocation = new CCPoint(355, 55);

            time = Stopwatch.StartNew();

            TouchEnabled = true;

            IsSoundFXMenuItemActive = !GameData.SharedData.AreSoundFXMuted;
            IsVoiceFXMenuActive = !GameData.SharedData.AreVoiceFXMuted;
            IsAmbientFXMenuActive = !GameData.SharedData.AreAmbientFXMuted;

            backLabel = new CCLabelTTF("Back", "MarkerFelt", 18);
                      
            var menuItemLabel = new CCMenuItemLabel(backLabel)
            {
                Color = new CCColor3B(Color.Blue)
            };

            menuItemLabel.SetTarget((cc) =>
            {
                CCDirector.SharedDirector.PopScene();
            });

            backMenu = new CCMenu(menuItemLabel)
            {
                Position = CCDirector.SharedDirector.WinSize.Center
            };

            AddChild(backMenu);
        }

        void PlayNegativeSound(object sender)
        {
            //play a sound indicating this level isn't available
        }

        public static CCScene Scene
        {
            get
            {
                // 'scene' is an autorelease object.
                CCScene scene = new CCScene();

                // 'layer' is an autorelease object.
                GameMenu layer = new GameMenu();

                // add layer as a child to scene
                scene.AddChild(layer);

                // return the scene
                return scene;
            }
        }

        #region SECTION BUTTONS

        #region POP (remove) SCENE and Transition to new level

        void PopAndTransition()
        {
            CCDirector.SharedDirector.PopScene();

            //when TheLevel scene reloads it will start with a new level
            GameLevel.SharedLevel.TransitionAfterMenuPop();
        }

        #endregion

        #region  POP (remove) SCENE and continue playing current level

        public override void TouchesBegan(System.Collections.Generic.List<CCTouch> touches)
        {
            CCDirector.SharedDirector.PopScene();
        }

        #endregion

        #region VOICE FX

        bool IsVoiceFXMenuActive
        {
            set
            {
                RemoveChild(voiceFXMenu, true);
                CCMenuItem button1;
                CCLabelTTF label;

                if (!value)
                {
                    label = new CCLabelTTF(voiceButtonNameDim, "MarkerFelt", 18);
                    label.Color = new CCColor3B(Color.Gray);
                    button1 = new CCMenuItemLabel(label, TurnVoiceFXOn);
                }
                else
                {
                    label = new CCLabelTTF(voiceButtonName, "MarkerFelt", 18);
                    label.Color = new CCColor3B(Color.Yellow);
                    button1 = new CCMenuItemLabel(label, TurnVoiceFXOff);
                }

                voiceFXMenu = new CCMenu(button1);
                voiceFXMenu.Position = voiceFXMenuLocation;

                AddChild(voiceFXMenu, 10);
            }
        }

        void TurnVoiceFXOn(object sender)
        {
            GameData.SharedData.AreVoiceFXMuted = false;

            IsVoiceFXMenuActive = true;
        }

        void TurnVoiceFXOff(object sender)
        {
            GameData.SharedData.AreVoiceFXMuted = true;

            IsVoiceFXMenuActive = false;
        }

        #endregion

        #region Sound FX

        bool IsSoundFXMenuItemActive
        {
            set
            {
                RemoveChild(soundFXMenu, true);

                CCMenuItemLabel button1;

                CCLabelTTF label;

                if (!value)
                {
                    label = new CCLabelTTF(soundButtonNameDim, "MarkerFelt", 18);
                    label.Color = new CCColor3B(Color.Gray);
                    button1 = new CCMenuItemLabel(label, TurnSoundFXOn);
                }
                else
                {
                    label = new CCLabelTTF(soundButtonName, "MarkerFelt", 18);
                    label.Color = new CCColor3B(Color.Yellow);
                    button1 = new CCMenuItemLabel(label, TurnSoundFXOff);
                }

                soundFXMenu = new CCMenu(button1);
                soundFXMenu.Position = soundFXMenuLocation;

                AddChild(soundFXMenu, 10);
            }
        }

        void TurnSoundFXOn(object sender)
        {
            GameData.SharedData.AreSoundFXMuted = false;

            IsSoundFXMenuItemActive = true;
        }

        void TurnSoundFXOff(object sender)
        {
            GameData.SharedData.AreSoundFXMuted = true;

            IsSoundFXMenuItemActive = false;
        }

        #endregion

        #region Ambient FX

        bool IsAmbientFXMenuActive
        {
            set
            {
                RemoveChild(ambientFXMenu, true);

                CCMenuItemLabel button1;

                CCLabelTTF label;

                if (!value)
                {
                    label = new CCLabelTTF(ambientButtonName, "MarkerFelt", 18);
                    label.Color = new CCColor3B(Color.Gray);
                    button1 = new CCMenuItemLabel(label, TurnAmbientFXOn);
                }
                else
                {
                    label = new CCLabelTTF(ambientButtonNameDim, "MarkerFelt", 18);
                    label.Color = new CCColor3B(Color.Yellow);
                    button1 = new CCMenuItemLabel(label, TurnAmbientFXOff);
                }

                ambientFXMenu = new CCMenu(button1);
                ambientFXMenu.Position = ambientFXMenuLocation;

                AddChild(ambientFXMenu, 10);
            }
        }

        void TurnAmbientFXOn(object sender)
        {
            GameData.SharedData.AreAmbientFXMuted = true;

            IsAmbientFXMenuActive = true;
        }

        void TurnAmbientFXOff(object sender)
        {
            GameData.SharedData.AreAmbientFXMuted = false;

            IsAmbientFXMenuActive = false;
        }

        bool left = false;

        public void Update(GameTime gameTime)
        {
            backLabel.Text = $"Back {time.Elapsed.TotalSeconds.ToString("N0")}";
            backLabel.Color = new CCColor3B(Color.Red);

            var h = ContentSize.Height;
            var w = ContentSize.Width;
            var reverse = false;

            if (left)
            {
                backMenu.PositionX += -2;

                if (backMenu.PositionX < 0)
                {
                    backMenu.PositionX = 0;
                    reverse = true;
                }
            }

            if (!left)
            {
                backMenu.PositionX += 2;

                if (backMenu.PositionX > w)
                {
                    reverse = true;
                }
            }

            if (reverse)
            {
                left = !left;
            }
        }

        #endregion

        #endregion
    }
}

