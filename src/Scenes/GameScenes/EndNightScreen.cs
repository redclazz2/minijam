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
                    if (GameStateManager.nightVictims == 0)
                    {
                        GameStateManager.suspicion -= 10;
                        GameStateManager.hunger -= 20;
                    }
                    else
                    {
                        GameStateManager.hunger = MathHelper.Clamp(GameStateManager.hunger + GameStateManager.nightVictims * 15, 0, 100);     // +15 per victim    
                    }
                    
                    GameStateManager.sanity = MathHelper.Clamp(GameStateManager.sanity - GameStateManager.nightVictims * 10, 0, 100);     // -10 per victim
                    GameStateManager.suspicion = MathHelper.Clamp(GameStateManager.suspicion + GameStateManager.nightVictims * 20, 0, 100); // +20 per victim

                    GameStateManager.victims += GameStateManager.nightVictims;
                    GameStateManager.night++;
                    GameStateManager.nightTimer = 0;
                    GameStateManager.nightVictims = 0;

                    sceneManager.ChangeScene(
                        new ChoiceScreen(sceneManager)
                    );
                    break;
            }
        }
    }
}