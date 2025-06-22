using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using minijam.Scenes;
using minijam.src.Interfaces.GameObject;

namespace minijam.src.GameObjects.UI
{
    public class SpriteRenderer : AGameObject
    {
        Vector2 position;
        Texture2D sprite;
        public SpriteRenderer(Vector2 position, Texture2D sprite, Scene scene) : base(scene)
        {
            this.position = position;
            this.sprite = sprite;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw( sprite,
                new Vector2(position.X - sprite.Width / 2f, position.Y - sprite.Height / 2f),
                Color.White
            );
        }

        public override void Update(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }
    }
}