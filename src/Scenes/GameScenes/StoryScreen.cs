using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using minijam.Manager;
using minijam.Scenes;
using minijam.src.GameObjects.UI;

namespace minijam.src.Scenes.GameScenes
{
    public class StoryScreen : Scene
    {
        public StoryScreen(SceneManager sceneManager) : base(sceneManager){}

        public override void Initialize()
        {
            var font = AssetManager.Load<SpriteFont>("Fonts/GameFont");
            gameObjects.Add(new TextRenderer(
                "You've been cursed.",
                10,
                200,
                Color.Red,
                font,
                this
            ));

            gameObjects.Add(new TextRenderer(
                "Your soul is now two: human, wolf.\nEat them to survive.",
                10,
                300,
                font,
                this
            ));

            gameObjects.Add(new TextRenderer(
                "Press space to continue",
                1,
                460,
                Color.Yellow,
                font,
                this
            ));
        }

        public override void Notify(string sender)
        {
            switch (sender)
            {
                case "Continue":
                    sceneManager.ChangeScene(
                        new ChoiceScreen(sceneManager)
                    );
                    break;
            }
        }
    }
}