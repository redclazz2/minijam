using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using minijam.Scenes;

namespace minijam.src.GameObjects.NPC
{
    public class WanderingPerson : Person
    {
        private enum State { GoingToTrap, Investigating, GoingHome }

        private State currentState = State.GoingToTrap;
        private int wanderCycles = 0;
        private int maxWanderCycles;
        private float wanderTimer = 0f;
        private float wanderInterval = 3f;
        private Random rng = new();

        public WanderingPerson(Vector2 startPos, Vector2 baseTrapPosition, Texture2D circleSprite, Scene scene)
            : base(startPos, baseTrapPosition, circleSprite, scene)
        {
            detectionRadius = 70f;
            killInteractionRadius = 42f;

            speed = 105f;
            position = startPos;

            maxWanderCycles = rng.Next(4, 6);
            targetPosition = baseTrapPosition;

            color = Color.DarkRed;
            detectionCircleColor = color;
        }

        private Vector2 GetRandomNearbyPosition(Vector2 center, float maxRadius = 180f)
        {
            double angle = rng.NextDouble() * MathHelper.TwoPi;
            double radius = rng.NextDouble() * maxRadius;

            float dx = (float)(Math.Cos(angle) * radius);
            float dy = (float)(Math.Sin(angle) * radius);

            return center + new Vector2(dx, dy);
        }

        public override void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 dir = targetPosition - position;

            if (dir.Length() > 1f)
            {
                dir.Normalize();
                position += dir * speed * delta;
            }
            else
            {
                switch (currentState)
                {
                    case State.GoingToTrap:
                        currentState = State.Investigating;
                        wanderCycles = 0;
                        targetPosition = GetRandomNearbyPosition(targetPosition);
                        wanderTimer = 0f;
                        break;

                    case State.Investigating:
                        wanderTimer += delta;
                        if (wanderTimer > wanderInterval)
                        {
                            wanderCycles++;
                            wanderTimer = 0f;

                            if (wanderCycles >= maxWanderCycles)
                            {
                                currentState = State.GoingHome;
                                targetPosition = homePosition;
                            }
                            else
                            {
                                targetPosition = GetRandomNearbyPosition(targetPosition);
                            }
                        }
                        break;

                    case State.GoingHome:
                        isDone = true;
                        break;
                }
            }
        }
    }
}