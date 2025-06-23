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
        public int radius = 45;
        private int speed = 210;
        public Vector2 position = new(200, 200);
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

            Vector2 direction = Vector2.Zero;

            if (kState.IsKeyDown(Keys.D)) direction.X += 1;
            if (kState.IsKeyDown(Keys.A)) direction.X -= 1;
            if (kState.IsKeyDown(Keys.W)) direction.Y -= 1;
            if (kState.IsKeyDown(Keys.S)) direction.Y += 1;

            if (direction != Vector2.Zero)
            {
                direction.Normalize();
                Vector2 movement = direction * speed * dt;

                Vector2 nextPos = position + movement;

                if (nextPos.X >= -140 && nextPos.X <= 1450)
                    position.X = nextPos.X;

                if (nextPos.Y >= -370 && nextPos.Y <= 1110)
                    position.Y = nextPos.Y;
            }
        }
    }
}