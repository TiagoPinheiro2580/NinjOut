using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjOut
{
    class Player
    {
        public Texture2D WalkTexture, currentTexture, oldTexture;
        public Texture2D JumpTexture;
        public Texture2D IdleTexture;
        public Texture2D DeadTexture;
        public Texture2D GlideTexture;
        public Texture2D AttackTexture;

        private int Rows { get; set; }
        public int Columns { get; set; }
        public int currentFrame, totalFrames;

        //public Texture2D texturaFixe;
        private Vector2 position = new Vector2(64, 384);
        private Vector2 velocity;
        private Rectangle rectangle, sourceRectangle;
        Point frameSize;
        int frames = 0;
        int row, colum;
        bool flip = false;

        private bool hasJumped = false;

        public Vector2 Position
        {
            get { return position; }
        }

        public Player()
        {
            //frameSize = new Point(95, 195);
            frameSize = new Point(50, 90);
            currentFrame = 0;
            totalFrames = Rows * Columns;


        }

        public void Load(ContentManager Content)
        {
            position = new Vector2(0, 15);
            WalkTexture = Content.Load<Texture2D>("WalkSpriteSheet");
            JumpTexture = Content.Load<Texture2D>("JumpSpriteSheet");
            IdleTexture = Content.Load<Texture2D>("IdleSpriteSheet");
            GlideTexture = Content.Load<Texture2D>("GlideSpriteSheet");
            AttackTexture = Content.Load<Texture2D>("AttackSpriteSheet");
            DeadTexture = Content.Load<Texture2D>("DeadSpriteSheet");
            currentTexture = IdleTexture;
            oldTexture = currentTexture;
        }

        public void Update(GameTime gameTime)
        {

            //texturaFixe = Texture;
            //width = Texture.Width / Columns;
            //height = Texture.Height / Rows;
            float columnsWidth = currentTexture.Width;
            row = (currentTexture.Height / 2) * 0;

            //Rectangle sourceRectangle = new Rectangle(width * colum, height * row, width, height);
            //Rectangle destiantionRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);


            position += velocity;
            //rectangle = new Rectangle((int)position.X, (int)position.Y, Texture.Width, Texture.Height);

            //rectangle = new Rectangle((int)position.X, (int)position.Y, frameSize.X, frameSize.Y);


            if (Keyboard.GetState().IsKeyDown(Keys.D))
                currentTexture = WalkTexture;

            else if (Keyboard.GetState().IsKeyDown(Keys.Space) /*&& hasJumped == false*/)
            {
                currentTexture = JumpTexture;
            }

            else
            {
                currentTexture = IdleTexture;
            }

            if (currentTexture != oldTexture)
            {
                oldTexture = currentTexture;
                currentFrame = 0;
                frames = 0;
            }

          

          

            Input(gameTime);

            frames++;

            if (frames > 5)
            {
                currentFrame++;
                if (currentFrame > 9)
                    currentFrame = 0;

                frames = 0;
                colum = (int)((columnsWidth / 10) * currentFrame);

            }

            if (currentFrame == totalFrames)
            {

                currentFrame = 0;
            }

            if (currentTexture == WalkTexture)
            {
                sourceRectangle = new Rectangle(colum, row, currentTexture.Width / 11, currentTexture.Height);
            }
            else
            {
                sourceRectangle = new Rectangle(colum, row, currentTexture.Width / 10, currentTexture.Height);
            }

            if (currentTexture == IdleTexture)
            {
                rectangle = new Rectangle((int)position.X, (int)position.Y, currentTexture.Width / 9, currentTexture.Height);

            }
            else
            {
                rectangle = new Rectangle((int)position.X, (int)position.Y, currentTexture.Width / 9, currentTexture.Height);

            }


            //currentFrame++;


            //"gravidade"
            if (velocity.Y < 10)
            {
                velocity.Y += 0.4f;
            }


        }

        private void Input(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                flip = false;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {

                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                flip = true;
            }
            else
            {
                velocity.X = 0f;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                position.Y -= 15f;
                velocity.Y = -11f;
                hasJumped = true;

            }

        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                // rectangle.Y = newRectangle.Y - rectangle.Height;
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
                hasJumped = false;
            }
            if (rectangle.TouchLeftOf(newRectangle))
            {
                //2 é arrendondado , depende do tamanho da sprite. Ajusta-se o valor conforme a necessidade
                //position.X = newRectangle.X - rectangle.Width -2;
                rectangle.X -= rectangle.Right-newRectangle.Left;
                if (velocity.X > 0)
                    velocity.X = 0;
            }
            Rectangle idleRectangle = rectangle;
            idleRectangle.Width /= 2;
            if (idleRectangle.TouchRightOf(newRectangle))
            {
                //position.X = newRectangle.X + newRectangle.Width + 2;
                rectangle.X += newRectangle.Right-rectangle.Left;
                if (velocity.X < 0)
                    velocity.X = 0;
            }
            if (idleRectangle.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
            }

            //position.X = rectangle.X;
            //position.Y = rectangle.Y;

            //impedir que o jogador vá para fora do mapa - limites
            if (position.X < 0)
            {
                position.X = 0;
            }
            if (position.X > xOffset - rectangle.Width)
            {
                position.X = xOffset - rectangle.Width;
            }
            if (position.Y < 0)
            {
                velocity.Y = 1f;
            }
            if (position.Y > yOffset - rectangle.Height)
            {
                position.Y = yOffset - rectangle.Height;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            //int width = Texture.Width / Columns;
            //int height = Texture.Height / Rows;
            //int row = (int)((float)currentFrame / (float)Columns);
            //int colum = currentFrame % Columns;

            //Rectangle sourceRectangle = new Rectangle(width * colum, height * row, width, height);
            //Rectangle destiantionRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
            //Rectangle rectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

           //olé
            if (flip)
                spriteBatch.Draw(currentTexture, rectangle, sourceRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
            else
                spriteBatch.Draw(currentTexture, rectangle, sourceRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.None , 0f);


        }
    }
}
