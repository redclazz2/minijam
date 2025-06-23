using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using minijam.Scenes;

namespace minijam.src.GameObjects.NPC
{
    public class WanderingPerson : Person
    {
        private int wanderCycles = 0;
        private int maxWanderCycles;
        private float wanderTimer = 0f;
        private float wanderInterval = 4.5f;
        private Random rng = new();

        private bool goingHome = false;

        public WanderingPerson(Vector2 startPos, Texture2D circleSprite, Scene scene)
            : base(startPos, startPos, circleSprite, scene)
        {
            position = startPos;
            targetPosition = GetRandomNearbyPosition();
            maxWanderCycles = rng.Next(4, 13);

            color = Color.Yellow;
        }

        private Vector2 GetRandomNearbyPosition()
        {
            float dx = rng.Next(-380, 380);
            float dy = rng.Next(-380, 380);
            return homePosition + new Vector2(dx, dy);
        }

        public override void Update(GameTime gameTime)
        {
            wanderTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if ((targetPosition - position).Length() < 1f || wanderTimer > wanderInterval)
            {
                if (goingHome)
                {
                    isDone = true;
                    return;
                }

                wanderCycles++;

                if (wanderCycles >= maxWanderCycles)
                {
                    targetPosition = homePosition;
                    goingHome = true;
                }
                else
                {
                    targetPosition = GetRandomNearbyPosition();
                }

                wanderTimer = 0f;
            }

            Vector2 dir = targetPosition - position;
            if (dir.Length() > 1f)
            {
                dir.Normalize();
                position += dir * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}