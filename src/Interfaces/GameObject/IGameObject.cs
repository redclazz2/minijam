using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace minijam.Interfaces.GameObject
{
    public interface IGameObject
    {
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch);
    }
}