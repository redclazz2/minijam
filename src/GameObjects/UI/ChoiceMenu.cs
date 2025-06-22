using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using minijam.Scenes;
using minijam.src.Interfaces.GameObject;

namespace minijam.src.GameObjects.UI
{
    public class ChoiceMenu : AGameObject
    {
        private double selectionTimer = 0;
        private SpriteFont font;
        private string[] options;
        private int selection = 0;
        private bool upKeyReleased = true;
        private bool downKeyReleased = true;

        public ChoiceMenu(SpriteFont font, string[] options, Scene scene) : base(scene)
        {
            this.font = font;
            this.options = options;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < options.Length; i++)
            {
                Vector2 sizeOfText = font.MeasureString(options[i]);
                int hW = 1280 / 2;
                spriteBatch.DrawString(font, options[i], new Vector2(hW - sizeOfText.X / 2, 220 + 80 * i), 
                    selection == i ? Color.Red : Color.Gray);
            }
        }

        public override void Update(GameTime gameTime)
        {
            selectionTimer += gameTime.ElapsedGameTime.TotalSeconds;

            var keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.W) && upKeyReleased)
            {
                upKeyReleased = false;

                if (selection == 0)
                {
                    selection = options.Length - 1;
                }
                else
                {
                    selection--;
                }
            }

            if (keyboard.IsKeyUp(Keys.W))
            {
                upKeyReleased = true;
            }


            if (keyboard.IsKeyDown(Keys.S) && downKeyReleased)
            {
                downKeyReleased = false;

                if (selection >= options.Length - 1)
                {
                    selection = 0;
                }
                else
                {
                    selection++;
                }
            }

            if (keyboard.IsKeyUp(Keys.S))
            {
                downKeyReleased = true;
            }

            if (keyboard.IsKeyDown(Keys.Space) && selectionTimer > 0.5f)
            {
                if (selection == 0)
                {
                    scene.Notify("Eat");
                }
                else
                {
                    scene.Notify("Resist");
                }
            }
        }
    }
}