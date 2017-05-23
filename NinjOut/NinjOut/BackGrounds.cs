using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjOut
{
    class BackGrounds
    {
        public Texture2D texture;
        public Rectangle rectangle;

      

        public void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

    }

    class Scrolling : BackGrounds
    {

        public Scrolling(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle = newRectangle;
        }


        public void Update()
        {

                /*rectangle.X -= 1;*/ //sempre que esta função for chamada a imagem vai mover-se 3 pixeis para a esquerda

        }
    }
}
