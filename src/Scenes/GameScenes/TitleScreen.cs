using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using minijam.Manager;
using minijam.src.GameObjects.UI;
using minijam.src.Manager;
using minijam.src.Scenes.GameScenes;

namespace minijam.Scenes.GameScenes
{
    public class TitleScreen : Scene
    {
        public TitleScreen(SceneManager sceneManager) : base(sceneManager)
        {
            GameStateManager.Reset();
        }

        public override void Initialize()
        {
            CameraManager.UpdateCameraPosition(new Vector2(1280 / 2, 720 / 2));

            var font = AssetManager.Load<SpriteFont>("Fonts/GameFont");
            gameObjects.Add(new TextRenderer("A Tale of Flesh & Fur", 100, 250, font, this));
            gameObjects.Add(new TextRenderer("Press space to begin", 1, 350, Color.Red, font, this));
            
            gameObjects.Add(new TextRenderer("V.1.1.0", 1, 650, Color.Gray, font, this));
        }

        public override void Notify(string sender)
        {
            switch (sender)
            {
                case "Continue":
                    sceneManager.ChangeScene(
                        new StoryScreen(sceneManager)
                    );
                    break;
            }
        }
    }
}