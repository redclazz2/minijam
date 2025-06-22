using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using minijam.Manager;
using minijam.Scenes;
using minijam.Scenes.GameScenes;

namespace minijam;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SceneManager sceneManager;
    private Effect crtEffect;
    private RenderTarget2D renderTarget;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        AssetManager.Initialize(Content);
        sceneManager = new();
        var scene = new TitleScreen(sceneManager);
        sceneManager.ChangeScene(scene);

        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.ApplyChanges();

        renderTarget = new RenderTarget2D(GraphicsDevice,
GraphicsDevice.PresentationParameters.BackBufferWidth,
GraphicsDevice.PresentationParameters.BackBufferHeight);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        crtEffect = Content.Load<Effect>("Shader/CRT");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        sceneManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        //Draw the game to a render target
        GraphicsDevice.SetRenderTarget(renderTarget);
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();
        sceneManager.Draw(_spriteBatch);
        _spriteBatch.End();

        //Set the target to default and add the crt shader
        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(Color.Black);

        // Update shader parameters
        crtEffect.Parameters["time"]?.SetValue((float)gameTime.TotalGameTime.TotalSeconds);
        crtEffect.Parameters["screenSize"]?.SetValue(new Vector2(renderTarget.Width, renderTarget.Height));

        _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque, null, null, null, crtEffect);
        _spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }

}
