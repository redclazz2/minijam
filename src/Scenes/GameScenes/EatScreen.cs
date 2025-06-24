using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using minijam.Manager;
using minijam.Scenes;
using minijam.src.GameObjects.NightTimer;
using minijam.src.GameObjects.NPC;
using minijam.src.GameObjects.Player;
using minijam.src.GameObjects.Traps;
using minijam.src.GameObjects.UI;
using minijam.src.Manager;

namespace minijam.src.Scenes.GameScenes
{
    public class EatScreen : Scene
    {
        private List<Trap> traps = [];
        private Player player;
        private NightTimerTextRenderer nightTimerTextRenderer;
        private Song ambience;
        private SoundEffect shotgun;
        private List<SoundEffect> screams = [];
        private PersonController personController;
        private Random random = new Random();
        private List<string> endNightMessages = new()
        {
            "You knew their name.",
            "They were kind to you once.",
            "You saw the fear in their eyes and didn't stop.",
            "Their laughter used to echo through the village.",
            "You told yourself you had no choice. Lier.",
            "You remember their smile as you tore them apart.",
            "They trusted you.",
            "One of them waved at you earlier that day.",
            "You knew their home. You walked past it for years.",
            "You dug the grave with shaking hands.",
            "You see their face when you close your eyes.",
            "They were someone's child. Someone's parent. Someone.",
            "You whispered sorry. It wasn't enough.",
            "The blood won't come off.",
            "You waited until they were alone. You coward.",
            "You thought it was survival. It felt like betrayal.",
            "Their voice cracked when they begged.",
            "You remember when you used to care.",
            "It was supposed to get easier. It hasn't."
        };

        public EatScreen(SceneManager sceneManager) : base(sceneManager)
        {
            //Load village
            ambience = AssetManager.Load<Song>("Sounds/crickets");
            shotgun = AssetManager.Load<SoundEffect>("Sounds/shot");
            var villageSprite = AssetManager.Load<Texture2D>("Sprites/Village");
            gameObjects.Add(new SpriteRenderer(new Vector2(1280 / 2, 720 / 2), villageSprite, this));

            //Spawn the player
            var playerSprite = AssetManager.Load<Texture2D>("Sprites/Wolf");
            player = new Player(playerSprite, this);
            gameObjects.Add(player);

            //Night Timer UI
            var font = AssetManager.Load<SpriteFont>("Fonts/GameFont");
            gameObjects.Add(new NightTimer(25, this));
            nightTimerTextRenderer = new NightTimerTextRenderer("1 AM", 0, 0, font, this);
            gameObjects.Add(nightTimerTextRenderer);

            //Spawn all the traps
            var trapFont = AssetManager.Load<SpriteFont>("Fonts/TrapFont");
            traps.Add(new Trap(new Vector2(1030, 915), trapFont, AssetManager.Load<SoundEffect>("Sounds/shop"), this));
            traps.Add(new Trap(new Vector2(230, 900), trapFont, AssetManager.Load<SoundEffect>("Sounds/farm"), this));
            traps.Add(new Trap(new Vector2(640, 110), trapFont, AssetManager.Load<SoundEffect>("Sounds/fire"), this));
            traps.Add(new Trap(new Vector2(635, -275), trapFont, AssetManager.Load<SoundEffect>("Sounds/churchbell"), this));
            traps.Add(new Trap(new Vector2(80, -80), trapFont, AssetManager.Load<SoundEffect>("Sounds/splash2"), this));

            for (var i = 0; i < traps.Count; i++)
            {
                gameObjects.Add(traps[i]);
            }

            //Person controller
            var personSprite = AssetManager.Load<Texture2D>("Sprites/human");
            var circleSprite = AssetManager.Load<Texture2D>("Sprites/Circle");

            personController = new PersonController(
                personSprite,
                circleSprite,
                [
                    new Vector2(410,-150),
                    new Vector2(1120,-75),
                    new Vector2(915,375),
                    new Vector2(1295,645),
                ],
                this
            );
            gameObjects.Add(personController);

            screams.Add(AssetManager.Load<SoundEffect>("Sounds/Scream/maleScream"));
            screams.Add(AssetManager.Load<SoundEffect>("Sounds/Scream/femaleScream"));
        }

        public override void Initialize()
        {
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(ambience);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.Update(gameTime);
            }

            //Trap check
            foreach (Trap t in traps)
            {
                int sum = t.radius + player.radius;
                if (Vector2.Distance(player.position, t.position) < sum)
                {
                    t.drawStatus = true;

                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && !t.update)
                    {
                        t.soundEffect.Play();
                        t.update = true;
                        personController.TriggerTrap(t.position);
                    }
                }
                else
                {
                    t.drawStatus = false;
                }
            }

            //Person seeking range check
            foreach (Person p in personController.people)
            {
                //float sum = p.detectionRadius;
                if (Vector2.Distance(player.position, p.position) < p.detectionRadius)
                {
                    if (!p.occupied)
                    {
                        MediaPlayer.Stop();
                        shotgun.Play();
                        sceneManager.ChangeScene(
                            new GameOver("You've been killed", sceneManager)
                        );
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.Space)
                        && Vector2.Distance(player.position, p.position) < p.killInteractionRadius
                    )
                    {
                        var scream = screams[random.Next(screams.Count)];
                        scream.Play();
                        p.isDead = true;
                        GameStateManager.nightVictims++;
                    }
                }
            }
        }

        public override void Notify(string sender)
        {
            switch (sender)
            {
                case "ClockHour":
                    if (nightTimerTextRenderer.hour < 5)
                    {
                        nightTimerTextRenderer.hour++;
                    }
                    else
                    {
                        FinishNight();
                    }

                    break;

                case "Finish":
                    FinishNight();
                    break;
            }
        }

        private void FinishNight()
        {
            MediaPlayer.Stop();

            string message =
            GameStateManager.nightVictims > 0 ?
            endNightMessages[random.Next(endNightMessages.Count)] :
            "What a peaceful night ... "
            ;

            sceneManager.ChangeScene(
                new EndNightScreen(message, sceneManager)
            );
        }

    }
}