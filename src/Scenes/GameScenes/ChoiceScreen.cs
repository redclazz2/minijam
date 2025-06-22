using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using minijam.Manager;
using minijam.Scenes;
using minijam.src.GameObjects.UI;
using minijam.src.Manager;

namespace minijam.src.Scenes.GameScenes
{
    public class ChoiceScreen : Scene
    {
        public ChoiceScreen(SceneManager sceneManager): base(sceneManager){}

        public override void Initialize()
        {
            CameraManager.UpdateCameraPosition(new Vector2(1280/2,720/2));

            var font = AssetManager.Load<SpriteFont>("Fonts/GameFont");
            gameObjects.Add(new TextRenderer($"Night {GameStateManager.night}", 600, 40, font, this));
            gameObjects.Add(new TextRenderer("What will it be?", 600, 100, font, this));
            gameObjects.Add(new ChoiceMenu(font, ["Eat", "Resist"], this));

            if (GameStateManager.night == 1)
                gameObjects.Add(new TextRenderer($"Press W/A to move and space to select", 600, 140, Color.Gray, font, this));

            //Sanity icon
            var sanityIcon = AssetManager.Load<Texture2D>("Sprites/Icons/Brain");
            gameObjects.Add(new TextRenderer("Sanity", 600, 350f, 400, Color.White, font, this));
            gameObjects.Add(new SpriteRenderer(new Vector2(350, 550), sanityIcon, this));
            gameObjects.Add(new TextRenderer($"{GameStateManager.sanity}%", 600, 350f, 650, Color.White, font, this));

            //Suspicion Icon
            var suspicionIcon = AssetManager.Load<Texture2D>("Sprites/Icons/Suspicion");
            gameObjects.Add(new TextRenderer("Suspicion", 600, 650f, 400, Color.White, font, this));
            gameObjects.Add(new SpriteRenderer(new Vector2(650, 550), suspicionIcon, this));
            gameObjects.Add(new TextRenderer($"{GameStateManager.suspicion}%", 600, 650f, 650, Color.White, font, this));

            //Suspicion Icon
            var hungerIcon = AssetManager.Load<Texture2D>("Sprites/Icons/Hunger");
            gameObjects.Add(new TextRenderer("Hunger", 600, 950f, 400, Color.White, font, this));
            gameObjects.Add(new SpriteRenderer(new Vector2(950, 550), hungerIcon, this));
            gameObjects.Add(new TextRenderer($"{GameStateManager.hunger}%", 600, 950f, 650, Color.White, font, this));
        }

        public override void Notify(string sender)
        {
            switch (sender)
            {
                case "Eat":
                    sceneManager.ChangeScene(
                        new EatScreen(sceneManager)
                    );
                    break;

                case "Resist":
                    break;
            }
        }
    }
}