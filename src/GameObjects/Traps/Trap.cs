using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using minijam.Scenes;
using minijam.src.Interfaces.GameObject;

namespace minijam.src.GameObjects.Traps
{
    public class Trap : AGameObject
    {
        public Vector2 position;
        private SpriteFont spriteFont;
        public int radius = 50;
        public bool update = false;
        private double cooldown = 16;
        public bool drawStatus = false;
        public SoundEffect soundEffect;

        public Trap(Vector2 position, SpriteFont spriteFont, SoundEffect soundEffect, Scene scene) : base(scene)
        {
            this.soundEffect = soundEffect;
            this.position = position;
            this.spriteFont = spriteFont;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (drawStatus)
            {
                if (cooldown == 16)
                {
                    var message = "Press Space";
                    Vector2 sizeOfText = spriteFont.MeasureString(message);
                    spriteBatch.DrawString(
                        spriteFont,
                        message,
                        new Vector2(position.X - sizeOfText.X / 2, position.Y - 130),
                        Color.White
                    );
                }
                else
                {
                    var message = $"{(int)cooldown}";
                    Vector2 sizeOfText = spriteFont.MeasureString(message);
                    spriteBatch.DrawString(
                        spriteFont,
                        message,
                        new Vector2(position.X - sizeOfText.X / 2, position.Y - 130),
                        Color.White
                    );
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (update)
            {
                cooldown -= gameTime.ElapsedGameTime.TotalSeconds;

                if (cooldown <= 0)
                {
                    cooldown = 16;
                    update = false;
                }
            }
        }
    }
}