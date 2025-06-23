using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using minijam.Scenes;
using minijam.src.Interfaces.GameObject;
using minijam.src.Manager;

namespace minijam.src.GameObjects.NPC
{
    public class Person : AGameObject
    {
        public Texture2D sprite;
        public Texture2D circleSprite;
        public Color color;
        public Vector2 position;
        public Vector2 homePosition;
        public Vector2 targetPosition;
        public bool occupied = false;
        public float detectionRadius = 62f;
        public float killInteractionRadius = 40;
        private bool atTrap = false;
        private float waitTime;
        private float waitTimer;
        public bool isDone = false;
        public bool isDead = false;
        public SoundEffect screamSound;
        protected float speed = 60f;
        private bool isFlashing = false;
        private float flashTimer = 0f;
        private float flashDuration = 0.85f;
        private float colorSwapTimer = 0f;
        private float colorSwapInterval = 0.08f;

        public Person(
            Vector2 homePosition,
            Vector2 baseTrapPosition,
            Texture2D circleSprite,
            Scene scene) : base(scene)
        {
            color = Color.OrangeRed;

            this.circleSprite = circleSprite;
            this.homePosition = homePosition;
            position = homePosition;

            var offset = new Vector2(
                Random.Shared.Next(-25, 25),
                Random.Shared.Next(-25, 25)
            );

            detectionRadius += Random.Shared.Next(15, 40);

            if (GameStateManager.suspicion >= 75)
            {
                detectionRadius += Random.Shared.Next(40, 70);
            }
            else if (GameStateManager.suspicion >= 50)
            {
                detectionRadius += Random.Shared.Next(25, 50);
            }
            else if (GameStateManager.suspicion >= 25)
            {
                detectionRadius += Random.Shared.Next(10, 30);
            }

            speed += Random.Shared.Next(15, 40);
            targetPosition = baseTrapPosition + offset;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite,
                new Vector2(position.X - sprite.Width / 2f, position.Y - sprite.Height / 2f),
                color
            );

            if (!occupied)
            {
                float scale = detectionRadius * 2 / circleSprite.Width;

                spriteBatch.Draw(
                    circleSprite,
                    new Vector2(position.X - detectionRadius, position.Y - detectionRadius),
                    null,
                    Color.Red,
                    0f,
                    Vector2.Zero,
                    scale,
                    SpriteEffects.None,
                    0f
                );
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (atTrap)
            {
                color = Color.Blue;
                waitTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (!isFlashing && waitTimer >= waitTime)
                {
                    isFlashing = true;
                    flashTimer = 0f;
                    colorSwapTimer = 0f;
                }

                if (isFlashing)
                {
                    flashTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    colorSwapTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (colorSwapTimer >= colorSwapInterval)
                    {
                        color = (color == Color.Blue ? Color.Red : Color.Blue);
                        colorSwapTimer = 0f;
                    }

                    if (flashTimer >= flashDuration)
                    {
                        isFlashing = false;
                        atTrap = false;
                        occupied = false;
                        targetPosition = homePosition;
                        color = Color.OrangeRed; // Reset to default
                    }

                    return;
                }

                return;
            }

            Vector2 direction = targetPosition - position;
            if (direction.Length() < 1f)
            {
                position = targetPosition;

                if (targetPosition == homePosition)
                {
                    isDone = true;
                }
                else
                {
                    atTrap = true;
                    occupied = true;
                    waitTime = 2f + (float)new Random().NextDouble() * 2.0f;
                    waitTimer = 0f;
                }
            }
            else
            {
                direction.Normalize();
                position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}