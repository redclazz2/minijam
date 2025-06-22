using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using minijam.Manager;
using minijam.Scenes;
using minijam.src.GameObjects.Player;
using minijam.src.GameObjects.UI;

namespace minijam.src.Scenes.GameScenes
{
    public class EatScreen : Scene
    {
        public EatScreen(SceneManager sceneManager) : base(sceneManager)
        {
            var villageSprite = AssetManager.Load<Texture2D>("Sprites/Village");
            gameObjects.Add(new SpriteRenderer(new Vector2(1280 / 2, 720 / 2), villageSprite, this));
            var playerSprite = AssetManager.Load<Texture2D>("Sprites/Wolf");
            
            gameObjects.Add(new Player(playerSprite, this));
        }

        public override void Initialize()
        {
            //throw new NotImplementedException();
        }

        public override void Notify(string sender)
        {
            //throw new NotImplementedException();
        }
    }
}