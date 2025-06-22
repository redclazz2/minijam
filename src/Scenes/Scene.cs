using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using minijam.Interfaces.GameObject;
using minijam.Interfaces.Mediator;
using minijam.Manager;

namespace minijam.Scenes
{
    public abstract class Scene : IMediator
    {
        protected SceneManager sceneManager;
        protected List<IGameObject> gameObjects = [];

        public Scene(SceneManager sceneManager)
        {
            this.sceneManager = sceneManager;
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.Draw(spriteBatch);
            }
        }

        public abstract void Initialize();

        public abstract void Notify(string sender);
    }
}