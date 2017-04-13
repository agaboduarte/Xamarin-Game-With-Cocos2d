using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstGame
{
    public interface IGameLoop
    {
        void Update(GameTime gameTime);
    }
}
