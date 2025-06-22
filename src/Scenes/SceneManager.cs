using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace minijam.Scenes
{
    public class SceneManager
    {
        private Scene currentScene;

        public void ChangeScene(Scene newScene)
        {
            currentScene = newScene;
            currentScene.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            currentScene.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScene.Draw(spriteBatch);
        }
    }
}