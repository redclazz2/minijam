using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using minijam.Scenes;
using minijam.src.Interfaces.GameObject;

namespace minijam.src.GameObjects.NightTimer
{
    public class NightTimer : AGameObject
    {
        private int timerMax;
        private double currentTimer;

        public NightTimer(int countdown, Scene scene) : base(scene)
        {
            this.timerMax = countdown;
        }

        public override void Draw(SpriteBatch spriteBatch){}

        public override void Update(GameTime gameTime)
        {
            currentTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (currentTimer >= timerMax)
            {
                currentTimer = 0f;

                scene.Notify(
                    "ClockHour"
                );
            } 
        }
    }
}