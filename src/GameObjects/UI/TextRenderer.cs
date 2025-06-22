using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using minijam.Scenes;
using minijam.src.Interfaces.GameObject;

namespace minijam.src.GameObjects.UI
{
    public class TextRenderer : AGameObject
    {
        SpriteFont gameFont;
        double keyTimer = 0;
        int awaitTime = 0;
        string text;
        Color color = Color.White;
        protected double y;

        protected float x = 1280 / 2;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }


        public TextRenderer(string text, int awaitTime, double y, SpriteFont gameFont, Scene scene) : base(scene)
        {
            this.gameFont = gameFont;
            this.text = text;
            this.awaitTime = awaitTime;
            this.y = y;
        }

        public TextRenderer(string text, int awaitTime, double y, Color color, SpriteFont gameFont, Scene scene) : base(scene)
        {
            this.gameFont = gameFont;
            this.text = text;
            this.awaitTime = awaitTime;
            this.color = color;
            this.y = y;
        }

        public TextRenderer(string text, int awaitTime, float x, double y, Color color, SpriteFont gameFont, Scene scene) : base(scene)
        {
            this.gameFont = gameFont;
            this.text = text;
            this.awaitTime = awaitTime;
            this.color = color;
            this.x = x;
            this.y = y;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 sizeOfText = gameFont.MeasureString(text);
            spriteBatch.DrawString(gameFont, text, new Vector2(x - sizeOfText.X / 2, (float)y), color);
        }

        public override void Update(GameTime gameTime)
        {
            keyTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && keyTimer >= awaitTime)
            {
                scene.Notify("Continue");
            }
        }
    }
}