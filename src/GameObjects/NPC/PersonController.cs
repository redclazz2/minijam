using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using minijam.Scenes;
using minijam.src.GameObjects.NPC.Factory;
using minijam.src.Interfaces.GameObject;
using minijam.src.Manager;

namespace minijam.src.GameObjects.NPC
{
    public class PersonController : AGameObject
    {
        private Random random = new();
        public List<Person> people = new();
        private Texture2D personSprite;
        private Texture2D circleSprite;
        private List<Vector2> spawnPositions;

        public PersonController(Texture2D personSprite, Texture2D circleSprite, List<Vector2> spawnPositions, Scene scene) : base(scene)
        {
            this.circleSprite = circleSprite;
            this.personSprite = personSprite;
            this.spawnPositions = spawnPositions;
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = people.Count - 1; i >= 0; i--)
            {
                var person = people[i];
                person.Update(gameTime);

                if (person.isDone || person.isDead)
                {
                    people.RemoveAt(i);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var person in people)
            {
                person.Draw(spriteBatch);
            }
        }

        public void TriggerTrap(Vector2 trapPosition)
        {
            float baseChance = 0.6f;
            float modifiedChance = MathHelper.Clamp(baseChance - GameStateManager.victims * 0.03f, 0.4f, 1f);

            if (random.NextDouble() < modifiedChance)
            {
                int howMany = random.Next(1, 4);
                for (int i = 0; i < howMany; i++)
                {
                    var home = spawnPositions[random.Next(spawnPositions.Count)];

                    string behavior = random.NextDouble() < 0.8 ? "Regular" : "Wandering";

                    var person = PersonFactory.CreatePerson(behavior, home, trapPosition, circleSprite, scene);
                    person.sprite = personSprite;

                    people.Add(person);
                }
            }
        }

    }
}