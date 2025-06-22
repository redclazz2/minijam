using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using minijam.Manager;
using minijam.Scenes;
using minijam.src.Interfaces.GameObject;

namespace minijam.src.GameObjects.NightTimer
{
    public class NightTimer : AGameObject
    {
        private int timerMax;
        private double currentTimer;
        private SoundEffect clockSoundEffect;
        
        public NightTimer(int countdown, Scene scene) : base(scene)
        {
            timerMax = countdown;
            clockSoundEffect = AssetManager.Load<SoundEffect>("Sounds/clock-1");
        }

        public override void Draw(SpriteBatch spriteBatch){}

        public override void Update(GameTime gameTime)
        {
            currentTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (currentTimer >= timerMax)
            {
                currentTimer = 0f;
                clockSoundEffect.Play();
                
                scene.Notify(
                    "ClockHour"
                );
            } 
        }
    }
}