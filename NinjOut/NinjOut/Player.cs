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
        private Texture2D texture;
        private Vector2 position = new Vector2(64, 384);
        private Vector2 velocity;
        private Rectangle rectangle;
        Point frameSize;


        private bool hasJumped = false;

        public Vector2 Position
        {
            get { return position; }
        }

        public Player()
        {
            //frameSize = new Point(95, 195);
            frameSize = new Point(50, 90);

        }
        public void Load(ContentManager Content)
        {
            position = new Vector2(0, 15);
            texture = Content.Load<Texture2D>("Player");
        }

        public void Update(GameTime gameTime)
        {
            position += velocity;
            //rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            rectangle = new Rectangle((int)position.X, (int)position.Y, frameSize.X, frameSize.Y);

            Input(gameTime);


            //"gravidade"
            if(velocity.Y <10)
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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

    }
}
