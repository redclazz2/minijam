using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using minijam.Manager;
using minijam.Scenes;
using minijam.Scenes.GameScenes;
using minijam.src.GameObjects.UI;
using minijam.src.Manager;

namespace minijam.src.Scenes.GameScenes
{
    public class GameOver : Scene
    {
        private string reason;
        public GameOver(string reason, SceneManager sceneManager) : base(sceneManager)
        {
            this.reason = reason;
        }

        public override void Initialize()
        {
            CameraManager.UpdateCameraPosition(new Vector2(1280/2,720/2));
            var font = AssetManager.Load<SpriteFont>("Fonts/GameFont");
            gameObjects.Add(new TextRenderer("GAME OVER", 60, 200, Color.Red, font, this));
            gameObjects.Add(new TextRenderer(reason, 60, 260, Color.OrangeRed, font, this));
            gameObjects.Add(new TextRenderer("Press space to continue", 1, 400, font, this));
        }

        public override void Notify(string sender)
        {
            switch (sender)
            {
                case "Continue":
                    sceneManager.ChangeScene(
                        new TitleScreen(sceneManager)
                    );
                    break;
            }
        }
    }
}