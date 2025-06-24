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
        private enum State { GoingToTrap, Waiting, Flashing, GoingHome, Done }

        public Texture2D sprite;
        public Texture2D circleSprite;
        public Color color;
        protected Color detectionCircleColor;

        public Vector2 position;
        public Vector2 homePosition;
        public Vector2 targetPosition;

        public bool occupied = false;
        public bool isDead = false;
        public bool isDone = false;

        public float detectionRadius = 60f;
        public float killInteractionRadius = 35f;

        public SoundEffect screamSound;

        protected float speed = 60f;

        private State currentState = State.GoingToTrap;

        private float waitTime = 0f;
        private float waitTimer = 0f;

        private float flashTimer = 0f;
        private float flashDuration = 0.85f;
        private float colorSwapTimer = 0f;
        private float colorSwapInterval = 0.08f;

        public Person(Vector2 homePosition, Vector2 baseTrapPosition, Texture2D circleSprite, Scene scene)
            : base(scene)
        {
            color = Color.OrangeRed;
            detectionCircleColor = color;
            this.circleSprite = circleSprite;
            this.homePosition = homePosition;
            position = homePosition;

            var offset = new Vector2(Random.Shared.Next(-40, 40), Random.Shared.Next(-25, 25));
            targetPosition = baseTrapPosition + offset;

            detectionRadius += Random.Shared.Next(15, 40);
            if (GameStateManager.suspicion >= 75) detectionRadius += Random.Shared.Next(40, 70);
            else if (GameStateManager.suspicion >= 50) detectionRadius += Random.Shared.Next(25, 50);
            else if (GameStateManager.suspicion >= 25) detectionRadius += Random.Shared.Next(10, 30);

            speed += Random.Shared.Next(15, 25);
        }

        public override void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (currentState)
            {
                case State.GoingToTrap:
                case State.GoingHome:
                    MoveToTarget(delta);
                    break;

                case State.Waiting:
                    waitTimer += delta;
                    if (waitTimer >= waitTime)
                    {
                        currentState = State.Flashing;
                        flashTimer = 0f;
                        colorSwapTimer = 0f;
                    }
                    break;

                case State.Flashing:
                    flashTimer += delta;
                    colorSwapTimer += delta;

                    if (colorSwapTimer >= colorSwapInterval)
                    {
                        color = (color == Color.Blue ? Color.OrangeRed : Color.Blue);
                        colorSwapTimer = 0f;
                    }

                    if (flashTimer >= flashDuration)
                    {
                        currentState = State.GoingHome;
                        color = Color.OrangeRed;
                        occupied = false;
                        targetPosition = homePosition;
                    }
                    break;

                case State.Done:
                    isDone = true;
                    break;
            }
        }

        private void MoveToTarget(float delta)
        {
            Vector2 dir = targetPosition - position;
            if (dir.Length() < 1f)
            {
                position = targetPosition;

                if (targetPosition == homePosition)
                {
                    currentState = State.Done;
                }
                else
                {
                    currentState = State.Waiting;
                    waitTime = 2f + (float)System.Random.Shared.NextDouble() * 2f;
                    waitTimer = 0f;
                    color = Color.Blue;
                    occupied = true;
                }
            }
            else
            {
                dir.Normalize();
                position += dir * speed * delta;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                sprite,
                new Vector2(position.X - sprite.Width / 2f, position.Y - sprite.Height / 2f),
                color
            );
            var radiusForCircle = !occupied ? detectionRadius : killInteractionRadius;
            float scale =
            radiusForCircle
             * 2 / circleSprite.Width;
            spriteBatch.Draw(
                circleSprite,
                new Vector2(position.X - radiusForCircle, position.Y - radiusForCircle),
                null,
                !occupied ? detectionCircleColor : Color.Blue,
                0f,
                Vector2.Zero,
                scale,
                SpriteEffects.None,
                0f
            );

        }
    }
}