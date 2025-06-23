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
        int victims;
        int sanityVariator;
        int suspicionVariator;
        int hungerVariator;

        public EndNightScreen(string message, SceneManager sceneManager) : base(sceneManager)
        {
            CameraManager.UpdateCameraPosition(new Vector2(1280 / 2, 720 / 2));
            this.message = message;
        }

        public override void Initialize()
        {

            victims = GameStateManager.nightVictims;

            sanityVariator = MathHelper.Clamp(GameStateManager.sanity + (victims == 0 ? 12 : -victims * 2), 0, 100);
            suspicionVariator = MathHelper.Clamp(GameStateManager.suspicion + (victims == 0 ? -16 : victims * 8), 0, 100);
            hungerVariator = MathHelper.Clamp(GameStateManager.hunger + (victims == 0 ? -30 : victims * 4), 0, 100);

            int sanityDelta = sanityVariator - GameStateManager.sanity;
            int suspicionDelta = suspicionVariator - GameStateManager.suspicion;
            int hungerDelta = hungerVariator - GameStateManager.hunger;

            string FormatDelta(int delta) => delta >= 0 ? $"+{delta}" : $"{delta}";

            CameraManager.UpdateCameraPosition(new Vector2(1280 / 2, 720 / 2));

            var font = AssetManager.Load<SpriteFont>("Fonts/GameFont");
            gameObjects.Add(new TextRenderer($"End of Night {GameStateManager.night}", 60, 60, Color.Red, font, this));
            gameObjects.Add(new TextRenderer(message, 60, 100, Color.OrangeRed, font, this));

            gameObjects.Add(new TextRenderer($"Kills: {victims}", 60, 180, Color.White, font, this));
            gameObjects.Add(new TextRenderer($"Sanity: {FormatDelta(sanityDelta)}", 60, 260, Color.LightGray, font, this));
            gameObjects.Add(new TextRenderer($"Suspicion: {FormatDelta(suspicionDelta)}", 60, 340, Color.LightGray, font, this));
            gameObjects.Add(new TextRenderer($"Hunger: {FormatDelta(hungerDelta)}", 60, 420, Color.LightGray, font, this));

            gameObjects.Add(new TextRenderer("Press space to continue", 1, 500, Color.Yellow, font, this));
        }

        public override void Notify(string sender)
        {
            switch (sender)
            {
                case "Continue":
                    GameStateManager.sanity = sanityVariator;
                    GameStateManager.suspicion = suspicionVariator;
                    GameStateManager.hunger = hungerVariator;


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