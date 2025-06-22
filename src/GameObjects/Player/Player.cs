using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using minijam.Scenes;
using minijam.src.Interfaces.GameObject;
using minijam.src.Manager;

namespace minijam.src.GameObjects.Player
{
    public class Player : AGameObject
    {
        private int speed = 300;
        Vector2 position = new(200, 200);
        Texture2D sprite;

        public Player(Texture2D sprite, Scene scene) : base(scene)
        {
            this.sprite = sprite;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite,
                new Vector2(position.X - sprite.Width / 2f, position.Y - sprite.Height / 2f),
                Color.White
            );
        }

        public override void Update(GameTime gameTime)
        {
            CameraManager.UpdateCameraPosition(position);
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (kState.IsKeyDown(Keys.Right))
            {
                position.X += speed * dt;
            }

            if (kState.IsKeyDown(Keys.Left))
            {
                position.X -= speed * dt;
            }

            if (kState.IsKeyDown(Keys.Up))
            {
                position.Y -= speed * dt;
            }

            if (kState.IsKeyDown(Keys.Down))
            {
                position.Y += speed * dt;
            }
        }
    }
}