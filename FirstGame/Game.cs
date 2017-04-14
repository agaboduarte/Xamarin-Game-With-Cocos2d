using Cocos2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirstGame
{
    public class MyGame : Game
    {
        private readonly GraphicsDeviceManager graphics;

        public MyGame()
            : base()
        {
            Content.RootDirectory = "Content";

            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;

            Components.Add(new AppDelegate(this, graphics));
        }

        private void ProcessBackClick()
        {
            if (CCDirector.SharedDirector.CanPopScene)
            {
                CCDirector.SharedDirector.PopScene();
            }
            else
            {
                Exit();
            }
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                ProcessBackClick();
            }

            if (CCDirector.SharedDirector.RunningScene != null && CCDirector.SharedDirector.RunningScene.Children != null)
            {
                foreach (var item in CCDirector.SharedDirector.RunningScene.Children)
                {
                    if (item is IGameLoop gameLoop)
                    {
                        gameLoop.Update(gameTime);
                    }
                }
            }

            base.Update(gameTime);
        }
    }
}
