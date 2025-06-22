using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using minijam.Manager;
using minijam.Scenes;
using minijam.src.GameObjects.NightTimer;
using minijam.src.GameObjects.Player;
using minijam.src.GameObjects.UI;

namespace minijam.src.Scenes.GameScenes
{
    public class EatScreen : Scene
    {
        private NightTimerTextRenderer nightTimerTextRenderer;
        public EatScreen(SceneManager sceneManager) : base(sceneManager)
        {
            var villageSprite = AssetManager.Load<Texture2D>("Sprites/Village");
            gameObjects.Add(new SpriteRenderer(new Vector2(1280 / 2, 720 / 2), villageSprite, this));
            var playerSprite = AssetManager.Load<Texture2D>("Sprites/Wolf");

            gameObjects.Add(new Player(playerSprite, this));

            //Night Timer UI
            var font = AssetManager.Load<SpriteFont>("Fonts/GameFont");
            gameObjects.Add(new NightTimer(40, this));
            nightTimerTextRenderer = new NightTimerTextRenderer("1 AM", 0, 0, font, this); 
            gameObjects.Add(nightTimerTextRenderer);
        }

        public override void Initialize()
        {
            //throw new NotImplementedException();
        }

        public override void Notify(string sender)
        {
            switch (sender)
            {
                case "ClockHour":
                    nightTimerTextRenderer.hour++;
                    break;
            }
        }
    }
}