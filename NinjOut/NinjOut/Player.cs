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
        private Texture2D Texture { get; set; }
        private int Rows { get; set; }
        public int Columns { get; set; }
        public int currentFrame, totalFrames;
  
        private Vector2 position = new Vector2(64, 384);
        private Vector2 velocity;
        private Rectangle rectangle;
        Point frameSize;
        int frames = 0;

        private bool hasJumped = false;

        public Vector2 Position
        {
            get { return position; }
        }

        public Player(Texture2D texture, int rows, int columns)
        {
            //frameSize = new Point(95, 195);
            frameSize = new Point(50, 90);
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            


        }
        public void Load(ContentManager Content)
        {
            position = new Vector2(0, 15);
            Texture = Content.Load<Texture2D>("ArmySprite");
        }

        public void Update(GameTime gameTime)
        {
            position += velocity;
            //rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            //rectangle = new Rectangle((int)position.X, (int)position.Y, frameSize.X, frameSize.Y);

            Input(gameTime);

            frames++;
            
            if (frames > 5)
            {
                currentFrame++;
                frames = 0;
            }

            //currentFrame++;
            if (currentFrame == totalFrames)
            {

                currentFrame = 0;
            }

            //"gravidade"
            if (velocity.Y <10)
            {
                velocity.Y += 0.4f;
            }

        }

        private void Input(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            }
            else if(Keyboard.GetState().IsKeyDown(Keys.A))
            {
                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            }
            else
            {
                velocity.X = 0f;
            }


            if(Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped==false)
            {
                position.Y -= 15f;
                velocity.Y = -11f;
                hasJumped = true;
            }

        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if(rectangle.TouchTopOf(newRectangle))
            {
                // rectangle.Y = newRectangle.Y - rectangle.Height;
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
                hasJumped = false;
            }
            if(rectangle.TouchLeftOf(newRectangle))
            {
                //2 é arrendondado , depende do tamanho da sprite. Ajusta-se o valor conforme a necessidade
                //position.X = newRectangle.X - rectangle.Width -2;
               rectangle.X = newRectangle.X - rectangle.Width-2;
            }
            if(rectangle.TouchRightOf(newRectangle))
            {
                //position.X = newRectangle.X + newRectangle.Width + 2;
                rectangle.X = newRectangle.X + rectangle.Width +2;
            }
            if(rectangle.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
            }

            //position.X = rectangle.X;
            //position.Y = rectangle.Y;

            //impedir que o jogador vá para fora do mapa - limites
            if(position.X <0)
            {
                position.X = 0;
            }
            if(position.X> xOffset- rectangle.Width)
            {
                position.X = xOffset - rectangle.Width;
            }
            if(position.Y<0)
            {
                velocity.Y = 1f;
            }
            if(position.Y >yOffset -rectangle.Height)
            {
                position.Y = yOffset - rectangle.Height;
            }
        }
 
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int colum = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * colum, height * row, width, height);
            Rectangle destiantionRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            //spriteBatch.Begin();
            spriteBatch.Draw(Texture, destiantionRectangle, sourceRectangle, Color.White);
            //spriteBatch.End();

        }

    }
}
