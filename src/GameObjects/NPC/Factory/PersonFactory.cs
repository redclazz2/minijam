using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using minijam.Scenes;

namespace minijam.src.GameObjects.NPC.Factory
{
    public static class PersonFactory
    {
        public static Person CreatePerson(string behavior, Vector2 startPos, Vector2 trap, Texture2D circleSprite, Scene scene)
        {
            return behavior switch
            {
                "Regular" => new Person(startPos,trap,circleSprite,scene),
                "Wandering" => new WanderingPerson(startPos, trap, circleSprite, scene),
                _ => throw new Exception("Unsupported behavior")
            };
        }
    }
}