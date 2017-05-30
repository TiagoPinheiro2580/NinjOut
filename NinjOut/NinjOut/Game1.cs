using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace NinjOut
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera;
        Map map;
        Player player;
        Scrolling scrolling1;
        Scrolling scrolling2;
        Enemy[] enemyArray = new Enemy[5];

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = 800;
            this.graphics.PreferredBackBufferHeight = 600;

            //this.graphics.PreferredBackBufferWidth = 2200;
            //this.graphics.PreferredBackBufferHeight = 1600;
            this.graphics.IsFullScreen = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            map = new Map();
            //player = new Player();
            //camera = new Camera();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            camera = new Camera(GraphicsDevice.Viewport);
            //AnimatedPlayerWalking = Content.Load<Texture2D>("ArmySprite");
            player = new Player();

            for (int i = 0; i < enemyArray.Length; i++)
            {
                enemyArray[i] = new Enemy();
                enemyArray[i].Load(Content);
            }

            scrolling1 = new Scrolling(Content.Load<Texture2D>("Backgrounds/Background1"), new Rectangle(0, 0, 2200, 1600));
            scrolling2 = new Scrolling(Content.Load<Texture2D>("Backgrounds/Background2"), new Rectangle(2200, 0, 2200, 1600));

            Tile.Content = Content;
            map.Generate(new int[,] 
            {




                //{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                //{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                //{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                //{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                //{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                //{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                //{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                //{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                //{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 5},
                //{0, 0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 7, 0, 0, 1, 5, 4, 6, 5, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 4, 3},
                //{0, 0, 0, 0, 0, 0, 1, 4, 3, 3, 3, 8, 0, 0, 2, 3, 3, 3, 3, 6, 7, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 7, 0, 0, 1, 5, 5, 5, 5, 5, 5, 4, 3, 3},
                //{0, 0, 0, 0, 0, 1, 4, 3, 3, 3, 3, 8, 0, 0, 2, 3, 3, 3, 3, 3, 6, 7, 0, 0, 0, 1, 4, 3, 3, 3, 3, 3, 6, 5, 5, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                //{0, 0, 0, 0, 1, 4, 3, 3, 3, 3, 3, 8, 0, 0, 2, 3, 3, 3, 3, 3, 3, 8, 0, 0, 1, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                //}, 128);

            //{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 5, 7, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 7, 0, 0, 1, 5, 4, 6, 5, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 4, 3, 8, 0, 0, 1, 0, 0, 0, 0}, 
            { 0, 0, 0, 0, 0, 0, 1, 4, 3, 3, 3, 8, 0, 0, 2, 3, 3, 3, 3, 6, 7, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 7, 0, 0, 1, 5, 5, 5, 5, 5, 5, 4, 3, 3, 6, 5, 5, 4, 0, 0, 0, 0}, 
            { 0, 0, 0, 0, 0, 1, 4, 3, 3, 3, 3, 8, 0, 0, 2, 3, 3, 3, 3, 3, 6, 7, 0, 0, 0, 1, 4, 3, 3, 3, 3, 3, 6, 5, 5, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0, 0},
            { 0, 0, 0, 0, 1, 4, 3, 3, 3, 3, 3, 8, 0, 0, 2, 3, 3, 3, 3, 3, 3, 8, 0, 0, 1, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0, 0},
            }, 128);
            player.Load(Content);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for 
        /// sions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //gitcenas 
            player.Update(gameTime);

            for (int i = 0; i < enemyArray.Length; i++)
            {
                enemyArray[i].Update(gameTime);
            }

            foreach (CollisionTiles tile in map.CollisionTiles)
            {
                player.Collision(tile.Rectangle, map.Width, map.Height);
                for (int i = 0; i < enemyArray.Length; i++)
                {
                    enemyArray[i].Collision(tile.Rectangle, map.Width, map.Height);
                }
                camera.Update(player.Position, map.Width, map.Height);
            }

            if (player.Position.X >= (scrolling1.rectangle.X+scrolling1.rectangle.Width / 2) && scrolling2.rectangle.X<scrolling1.rectangle.X)
            {
                scrolling2.rectangle.X = scrolling1.rectangle.X + scrolling1.rectangle.Width;
            }
            //if (player.Position.X <= scrolling1.rectangle.Width / 2)
            //{
            //    scrolling2.rectangle.X = scrolling1.rectangle.X - scrolling1.texture.Width;
            //}
            if (player.Position.X >= (scrolling2.rectangle.X+scrolling2.rectangle.Width / 2) && scrolling1.rectangle.X < scrolling2.rectangle.X)
            {
                scrolling1.rectangle.X = scrolling2.rectangle.X + scrolling2.rectangle.Width;
            }
            //if (player.Position.X <= scrolling2.rectangle.Width / 2)
            //{
            //    scrolling1.rectangle.X = scrolling2.rectangle.X - scrolling2.texture.Width;
            //}


            ////Scrolling Backgrounds
            //if (scrolling1.rectangle.X + scrolling1.texture.Width <= 0)
            //{
            //    scrolling1.rectangle.X = scrolling2.rectangle.X + scrolling2.texture.Width;
            //}

            //if (scrolling2.rectangle.X + scrolling2.texture.Width <= 0)
            //{
            //    scrolling2.rectangle.X = scrolling1.rectangle.X + scrolling1.texture.Width;
            //}

            scrolling1.Update();
            scrolling2.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
           GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,null, null, null, null, camera.Transform);

            scrolling1.Draw(spriteBatch);
            scrolling2.Draw(spriteBatch);
            map.Draw(spriteBatch);

            //player.Draw(spriteBatch, new Vector2(400, 200));
            player.Draw(spriteBatch);

            for (int i = 0; i < enemyArray.Length; i++)
            {
                enemyArray[i].Draw(spriteBatch);
            }

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
