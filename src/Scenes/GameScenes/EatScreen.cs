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

namespace minijam.src.Scenes.GameScenes
{
    public class EatScreen : Scene
    {
        private List<Trap> traps = [];
        private Player player;
        private NightTimerTextRenderer nightTimerTextRenderer;
        private Song ambience;
        private SoundEffect shotgun;

        private PersonController personController;

        public EatScreen(SceneManager sceneManager) : base(sceneManager)
        {
            //Load village
            ambience = AssetManager.Load<Song>("Sounds/crickets");
            shotgun = AssetManager.Load<SoundEffect>("Sounds/shot");
            var villageSprite = AssetManager.Load<Texture2D>("Sprites/Village");
            gameObjects.Add(new SpriteRenderer(new Vector2(1280 / 2, 720 / 2), villageSprite, this));

            //Night Timer UI
            var font = AssetManager.Load<SpriteFont>("Fonts/GameFont");
            gameObjects.Add(new NightTimer(35, this));
            nightTimerTextRenderer = new NightTimerTextRenderer("1 AM", 0, 0, font, this);
            gameObjects.Add(nightTimerTextRenderer);

            //Spawn all the traps
            var trapFont = AssetManager.Load<SpriteFont>("Fonts/TrapFont");
            traps.Add(new Trap(new Vector2(1030, 915), trapFont, AssetManager.Load<SoundEffect>("Sounds/shop"), this));
            traps.Add(new Trap(new Vector2(245, 855), trapFont, AssetManager.Load<SoundEffect>("Sounds/farm"), this));
            traps.Add(new Trap(new Vector2(540, 19), trapFont, AssetManager.Load<SoundEffect>("Sounds/fire"), this));
            traps.Add(new Trap(new Vector2(635, -275), trapFont, AssetManager.Load<SoundEffect>("Sounds/churchbell"), this));
            traps.Add(new Trap(new Vector2(64, -110), trapFont, AssetManager.Load<SoundEffect>("Sounds/splash2"), this));

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

            //Spawn the player
            var playerSprite = AssetManager.Load<Texture2D>("Sprites/Wolf");
            player = new Player(playerSprite, this);
            gameObjects.Add(player);
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
                if (Vector2.Distance(player.position, p.position) < p.detectionRadius && !p.occupied)
                {
                    MediaPlayer.Stop();
                    shotgun.Play();
                    sceneManager.ChangeScene(
                        new GameOver("You've been killed", sceneManager)
                    );
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

        private void FinishNight() {
            //System.Console.WriteLine("FINISHED!");
            MediaPlayer.Stop();
        }
    }
}