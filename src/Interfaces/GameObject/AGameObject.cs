using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using minijam.Interfaces.GameObject;
using minijam.Scenes;

namespace minijam.src.Interfaces.GameObject
{
    public abstract class AGameObject : IGameObject
    {
        protected Scene scene;

        public AGameObject(Scene scene) {
            this.scene = scene;    
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);
    }
}