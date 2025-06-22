using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using minijam.Manager;
using minijam.src.GameObjects.UI;
using minijam.src.Scenes.GameScenes;

namespace minijam.Scenes.GameScenes
{
    public class TitleScreen : Scene
    {
        public TitleScreen(SceneManager sceneManager):base(sceneManager){}

        public override void Initialize()
        {
            var font = AssetManager.Load<SpriteFont>("Fonts/GameFont");
            //gameObjects.Add(new UISpriteRenderer(new Vector2(1280 / 2, 720 / 2), AssetManager.Load<Texture2D>("Sprites/Background/Bgm"),this));
            gameObjects.Add(new TextRenderer("A Tale of Flesh & Fur", 0, 250,font, this));
            gameObjects.Add(new TextRenderer("Press space to begin", 0, 350, Color.Red, font, this));
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