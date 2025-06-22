using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using minijam.Scenes;
using minijam.src.Interfaces.GameObject;

namespace minijam.src.GameObjects.UI
{
    public class UISpriteRenderer : AGameObject
    {
        Vector2 position;
        Texture2D sprite;
        public UISpriteRenderer(Vector2 position, Texture2D sprite, Scene scene) : base(scene)
        {
            this.position = position;
            this.sprite = sprite;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }
    }
}