using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using minijam.Scenes;
using minijam.src.GameObjects.UI;
using minijam.src.Manager;

namespace minijam.src.GameObjects.NightTimer
{
    public class NightTimerTextRenderer : TextRenderer
    {
        public int hour = 1;
        public NightTimerTextRenderer(string text, int awaitTime, double y, SpriteFont gameFont, Scene scene) : base(text, awaitTime, y, gameFont, scene)
        {
        }

        public override void Update(GameTime gameTime)
        {
            Text = $"{hour} AM";
            x = CameraManager.camera.Position.X;
            y = CameraManager.camera.Position.Y + 300;
        }
    }
}