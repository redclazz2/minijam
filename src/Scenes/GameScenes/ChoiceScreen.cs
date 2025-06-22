using System;
using Microsoft.Xna.Framework.Graphics;
using minijam.Manager;
using minijam.Scenes;
using minijam.src.GameObjects.UI;

namespace minijam.src.Scenes.GameScenes
{
    public class ChoiceScreen : Scene
    {
        public ChoiceScreen(SceneManager sceneManager): base(sceneManager){}

        public override void Initialize()
        {
            var font = AssetManager.Load<SpriteFont>("Fonts/GameFont");
            gameObjects.Add(new TextRenderer("What will it be tonight?", 10, 40, font, this));
            gameObjects.Add(new ChoiceMenu(font, ["Eat", "Resist"], this));
            
        }

        public override void Notify(string sender)
        {
            //throw new NotImplementedException();
        }
    }
}