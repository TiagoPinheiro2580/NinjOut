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
        public Player player;
        Scrolling scrolling1;
        Scrolling scrolling2;
        Scrolling scrolling3;
        Scrolling scrolling4;
        Scrolling scrolling5;
        Scrolling scrolling6;
        Scrolling scrolling7;
        Scrolling scrolling8;
        Scrolling scrolling9;
        Scrolling scrolling10;

        Enemy[] enemyArray = new Enemy[5];
        bool didGameStart = false;
        SpriteFont font;
        bool gameOver = false;
        Song backgroundMusic;
        int levelToLoad = 1;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = 1920;
            this.graphics.PreferredBackBufferHeight = 1080;

            //this.graphics.PreferredBackBufferWidth = 2200;
            //this.graphics.PreferredBackBufferHeight = 1600;
            this.graphics.IsFullScreen = false;
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
            font = Content.Load<SpriteFont>("DefaultFont");
            MediaPlayer.Play(Content.Load<Song>("backgroundMusic"));

            for (int i = 0; i < enemyArray.Length; i++)
            {
                enemyArray[i] = new Enemy(player);
                enemyArray[i].Load(Content);
            }

            scrolling1 = new Scrolling(Content.Load<Texture2D>("Backgrounds/Background1"), new Rectangle(0, 0, 2200, 1600));
            scrolling2 = new Scrolling(Content.Load<Texture2D>("Backgrounds/Background2"), new Rectangle(0, 0, 2200, 1600));
            scrolling3 = new Scrolling(Content.Load<Texture2D>("Backgrounds/Background1"), new Rectangle(0, 0, 2200, 1600));
            scrolling4 = new Scrolling(Content.Load<Texture2D>("Backgrounds/Background2"), new Rectangle(0, 0, 2200, 1600));
            scrolling5 = new Scrolling(Content.Load<Texture2D>("Backgrounds/Background1"), new Rectangle(0, 0, 2200, 1600));
            scrolling6 = new Scrolling(Content.Load<Texture2D>("Backgrounds/Background2"), new Rectangle(0, 0, 2200, 1600));
            scrolling7 = new Scrolling(Content.Load<Texture2D>("Backgrounds/Background1"), new Rectangle(0, 0, 2200, 1600));
            scrolling8 = new Scrolling(Content.Load<Texture2D>("Backgrounds/Background2"), new Rectangle(0, 0, 2200, 1600));
            scrolling9 = new Scrolling(Content.Load<Texture2D>("Backgrounds/Background1"), new Rectangle(0, 0, 2200, 1600));
            scrolling10 = new Scrolling(Content.Load<Texture2D>("Backgrounds/Background2"), new Rectangle(0, 0, 2200, 1600));


            scrolling2.rectangle.X = scrolling1.rectangle.X + scrolling1.rectangle.Width;
            scrolling3.rectangle.X = scrolling2.rectangle.X + scrolling2.rectangle.Width;
            scrolling4.rectangle.X = scrolling3.rectangle.X + scrolling3.rectangle.Width;
            scrolling5.rectangle.X = scrolling4.rectangle.X + scrolling4.rectangle.Width;
            scrolling6.rectangle.X = scrolling5.rectangle.X + scrolling5.rectangle.Width;
            scrolling7.rectangle.X = scrolling6.rectangle.X + scrolling6.rectangle.Width;
            scrolling8.rectangle.X = scrolling7.rectangle.X + scrolling7.rectangle.Width;
            scrolling9.rectangle.X = scrolling8.rectangle.X + scrolling8.rectangle.Width;
            scrolling10.rectangle.X = scrolling9.rectangle.X + scrolling9.rectangle.Width;

            Tile.Content = Content;
            LoadMap();
            
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

            if (didGameStart)
            {


                player.Update(gameTime);

                if (player.gameOver)
                {
                    didGameStart = false;
                    gameOver = true;
                    Restart();
                }

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
                }

                //if (player.Position.X >= (scrolling1.rectangle.X + scrolling1.rectangle.Width / 2) && scrolling2.rectangle.X < scrolling1.rectangle.X)
                //{
                //    scrolling2.rectangle.X = scrolling1.rectangle.X + scrolling1.rectangle.Width;
                //}
                //if (player.Position.X <= scrolling1.rectangle.Width / 2)
                //{
                //    scrolling2.rectangle.X = scrolling1.rectangle.X - scrolling1.texture.Width;
                //}
                //if (player.Position.X >= (scrolling2.rectangle.X + scrolling2.rectangle.Width / 2) && scrolling1.rectangle.X < scrolling2.rectangle.X)
                //{
                //    scrolling1.rectangle.X = scrolling2.rectangle.X + scrolling2.rectangle.Width;
                //}
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
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    gameOver = false;
                    didGameStart = true;
                }
            }
            camera.Update(player.Position, map.Width, map.Height);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        
        void LoadMap()
        {
            if (levelToLoad == 1)
            {
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
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 4, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 7, 0, 0, 1, 5, 4, 6, 5, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 4, 3, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 1, 4, 3, 3, 3, 8, 0, 0, 2, 3, 3, 3, 3, 6, 7, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 7, 0, 0, 1, 5, 5, 5, 5, 5, 5, 4, 3, 3, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 1, 4, 3, 3, 3, 3, 8, 0, 0, 2, 3, 3, 3, 3, 3, 6, 7, 0, 0, 0, 1, 4, 3, 3, 3, 3, 3, 6, 5, 5, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5},
            { 0, 0, 0, 0, 1, 4, 3, 3, 3, 3, 3, 8, 0, 0, 2, 3, 3, 3, 3, 3, 3, 8, 0, 0, 1, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 ,3 ,3 ,3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
            }, 128);
            }
            else
            {
                map.Generate(new int[,]
            {

            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 4, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 1, 0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 7, 0, 0, 1, 5, 4, 6, 5, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 4, 3, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 1, 0, 0, 0, 0, 0, 1, 4, 3, 3, 3, 8, 0, 0, 2, 3, 3, 3, 3, 6, 7, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 7, 0, 0, 1, 5, 5, 5, 5, 5, 5, 4, 3, 3, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 1, 0, 0, 0, 0, 1, 4, 3, 3, 3, 3, 8, 0, 0, 2, 3, 3, 3, 3, 3, 6, 7, 0, 0, 0, 1, 4, 3, 3, 3, 3, 3, 6, 5, 5, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5},
            { 1, 0, 0, 0, 1, 4, 3, 3, 3, 3, 3, 8, 0, 0, 2, 3, 3, 3, 3, 3, 3, 8, 0, 0, 1, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 ,3 ,3 ,3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
            }, 128);
            }
        }

        void Restart()
        {
            camera = new Camera(GraphicsDevice.Viewport);
            //AnimatedPlayerWalking = Content.Load<Texture2D>("ArmySprite");
            player = new Player();
            font = Content.Load<SpriteFont>("DefaultFont");
            MediaPlayer.Play(Content.Load<Song>("backgroundMusic"));

            for (int i = 0; i < enemyArray.Length; i++)
            {
                enemyArray[i] = new Enemy(player);
                enemyArray[i].Load(Content);
            }
            player.Load(Content);
            levelToLoad = 2;
            LoadMap();
        }

        protected override void Draw(GameTime gameTime)
        {
            if (!didGameStart)
                GraphicsDevice.Clear(Color.Black);
            else
                GraphicsDevice.Clear(Color.CornflowerBlue);


            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);

            if (!didGameStart)
            {
                if (gameOver)
                    spriteBatch.DrawString(font, "                Game Over\nPress Enter to play, Esc to quit", new Vector2(150, 540), Color.White);
                else
                    spriteBatch.DrawString(font, "Press Enter to play, Esc to quit", new Vector2(150, 540), Color.White);

            }
            else
            {
                scrolling1.Draw(spriteBatch);
                scrolling2.Draw(spriteBatch);
                scrolling3.Draw(spriteBatch);
                scrolling4.Draw(spriteBatch);
                scrolling5.Draw(spriteBatch);
                scrolling6.Draw(spriteBatch);
                scrolling7.Draw(spriteBatch);
                scrolling8.Draw(spriteBatch);
                scrolling9.Draw(spriteBatch);
                scrolling10.Draw(spriteBatch);
                map.Draw(spriteBatch);

                //player.Draw(spriteBatch, new Vector2(400, 200));
                player.Draw(spriteBatch);

                for (int i = 0; i < enemyArray.Length; i++)
                {
                    enemyArray[i].Draw(spriteBatch);
                }


                // TODO: Add your drawing code here
            }

            spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
