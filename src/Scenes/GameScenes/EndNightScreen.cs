using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using minijam.Manager;
using minijam.Scenes;
using minijam.src.GameObjects.UI;
using minijam.src.Manager;

namespace minijam.src.Scenes.GameScenes
{
    public class EndNightScreen : Scene
    {
        string message;
        public EndNightScreen(string message, SceneManager sceneManager) : base(sceneManager)
        {
            CameraManager.UpdateCameraPosition(new Vector2(1280 / 2, 720 / 2));
            this.message = message;
        }

        public override void Initialize()
        {
            CameraManager.UpdateCameraPosition(new Vector2(1280 / 2, 720 / 2));

            var font = AssetManager.Load<SpriteFont>("Fonts/GameFont");
            gameObjects.Add(new TextRenderer($"End of Night {GameStateManager.night}", 60, 100, Color.OrangeRed, font, this));
            gameObjects.Add(new TextRenderer(message, 60, 200, Color.White, font, this));

            gameObjects.Add(new TextRenderer("Press space to continue", 1, 400, Color.White, font, this));
        }

        public override void Notify(string sender)
        {
            switch (sender)
            {
                case "Continue":
                    int victims = GameStateManager.nightVictims;

                    if (victims == 0)
                    {
                        GameStateManager.sanity = MathHelper.Clamp(GameStateManager.sanity + 12, 0, 100);
                        GameStateManager.suspicion = MathHelper.Clamp(GameStateManager.suspicion - 16, 0, 100);
                        GameStateManager.hunger = MathHelper.Clamp(GameStateManager.hunger - 30, 0, 100);
                    }
                    else
                    {
                        GameStateManager.sanity = MathHelper.Clamp(GameStateManager.sanity - victims * 5, 0, 100);
                        GameStateManager.suspicion = MathHelper.Clamp(GameStateManager.suspicion + victims * 8, 0, 100);
                        GameStateManager.hunger = MathHelper.Clamp(GameStateManager.hunger + victims * 5, 0, 100);
                    }

                    GameStateManager.victims += victims;
                    GameStateManager.night++;
                    GameStateManager.nightTimer = 0;
                    GameStateManager.nightVictims = 0;

                    if (GameStateManager.sanity <= 0)
                    {
                        sceneManager.ChangeScene(
                            new GameOver("You're crazy. They all died.", sceneManager)
                        );
                    }
                    else if (GameStateManager.hunger <= 0)
                    {
                        sceneManager.ChangeScene(
                            new GameOver("You starved to death.", sceneManager)
                        );
                    }
                    else
                    {
                        sceneManager.ChangeScene(
                            new ChoiceScreen(sceneManager)
                        );
                    }

                    break;
            }
        }

    }
}