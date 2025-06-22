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
        private SpriteFont font;
        private string[] options;
        private int selection = 0;
        private bool upKeyReleased = true;

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
                spriteBatch.DrawString(font, options[i], new Vector2(hW - sizeOfText.X / 2, 120 + 80 * i), 
                    selection == i ? Color.Red : Color.Gray);
            }
        }

        public override void Update(GameTime gameTime)
        {
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
        }
    }
}