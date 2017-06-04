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
    public class Enemy
    {
        public Texture2D WalkTexture, currentTexture, oldTexture;
        public Texture2D ShootTexture;
        public Texture2D IdleTexture;
        public Texture2D DeadTexture;
        public Texture2D AttackTexture;
        Player player;
        private Rectangle playerRectangle;
        Game1 game1;
        private int Rows { get; set; }
        public int Columns { get; set; }
        public int currentFrame, totalFrames;
        public int health = 30;

        //public Texture2D texturaFixe;
        // Novos erros no github

        private Vector2 position = new Vector2(64, 384);
        private Vector2 velocity;
        private Rectangle rectangle, sourceRectangle;
        Point frameSize;
        int frames = 0;
        int row, colum;
        bool flip = false;
        public bool isAttacking = false;
        public bool isPlayerAttack = false;
        public int xPosEnemy = 6700;
        bool isDead = false;
        public int xPosEnemy2 = 1000;
        public bool isLevel1;
        //int counter = 1;
       // int limit = 3;
        float countDuration = 0.7f; //every  2s.
        float currentTime = 0f;

        public Vector2 Position
        {
            get { return position; }
        }
        public Enemy(Player player)
        {
            //frameSize = new Point(95, 195);
            frameSize = new Point(50, 90);
            currentFrame = 0;
            totalFrames = Rows * Columns;
            this.player = player;
        }

        public void Load(ContentManager Content)
        {
            //position = new Vector2(6700, 15);
            position = new Vector2(xPosEnemy, 15);
            WalkTexture = Content.Load<Texture2D>("WalkSpritSheetEnemy");
            ShootTexture = Content.Load<Texture2D>("ShootSpriteSheetEnemy");
            IdleTexture = Content.Load<Texture2D>("IdleSpriteSheetEnemy");
            AttackTexture = Content.Load<Texture2D>("MeeleAttackSpriteSheetEnemy");
            DeadTexture = Content.Load<Texture2D>("DeadSpriteSheetEnemy");
            currentTexture = IdleTexture;
            oldTexture = currentTexture;
        }

        public void Update(GameTime gameTime)
        {
            position += velocity;

            //texturaFixe = Texture;
            //width = Texture.Width / Columns;
            //height = Texture.Height / Rows;
            float columnsWidth = currentTexture.Width;
            row = (currentTexture.Height / 2) * 0;

            //Rectangle sourceRectangle = new Rectangle(width * colum, height * row, width, height);
            //Rectangle destiantionRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
            if (isDead == false)
            {
                if (isLevel1 == true)
                {
                    if (player.Position.X > 5900)
                    //  if (Vector2.Distance(player.Position, position) > 500)
                    {
                        velocity.X = 0;

                        for (int i = 0; i < 4; i++)
                        {
                            if (player.Position.X - position.X > 170 && player.Position.X - position.X < 2000)
                            {
                                velocity.X += 1;
                                flip = false;
                            }
                            else if (player.Position.X - position.X + 1 < -170 && player.Position.X - position.X + 1 > -2000)
                            {
                                velocity.X -= 1;
                                flip = true;
                            }
                            else break;

                        }
                    }
                }

                if (isLevel1 == false)
                {
                    if (player.Position.X > 0)
                    //  if (Vector2.Distance(player.Position, position) > 500)
                    {

                            velocity.X = 0;
                        
                        for (int i = 0; i < 4; i++)
                        {
                            if (player.Position.X - position.X > 170 && player.Position.X - position.X < 2000)
                            {
                                velocity.X += 1;
                                flip = false;
                            }
                            else if (player.Position.X - position.X + 1 < -170 && player.Position.X - position.X + 1 > -2000)
                            {
                                velocity.X -= 1;
                                flip = true;
                            }
                            else break;

                        }
                    }
                }
                if (Vector2.Distance(player.Position, position) < 200 && isAttacking == false)
                {
                    currentTexture = AttackTexture;
                    player.health -= 15;
                    isAttacking = true;

                }


                if (Vector2.Distance(player.Position, position) > 200 && Vector2.Distance(player.Position, position) < 2000)
                {
                    currentTexture = WalkTexture;
                    isAttacking = false;
                }

                if (Vector2.Distance(player.Position, position) > 2000)
                {
                    currentTexture = IdleTexture;
                }

            }

            //Ataque do player
            if (Vector2.Distance(player.Position, position) < 220)
            {
                if (player.currentTexture == player.AttackTexture && isPlayerAttack == false)
                {
                    health -= 30;
                    isPlayerAttack = true;
                }

            }

            if (Vector2.Distance(player.Position, position) > 200)
            {
                if (player.currentTexture == player.WalkTexture)
                {
                    isPlayerAttack = false;
                }

            }



            if (health <= 0 && isDead == false)
            {
                health = 0;
                player.points += 30;
                currentTexture = DeadTexture;
                isDead = true;
                return;
                
            }


            //rectangle = new Rectangle((int)position.X, (int)position.Y, Texture.Width, Texture.Height);
            //rectangle = new Rectangle((int)position.X, (int)position.Y, frameSize.X, frameSize.Y);

            if (currentTexture != oldTexture)
            {
                oldTexture = currentTexture;
                currentFrame = 0;
                frames = 0;
            }

            frames++;

            if (frames > 5)
            {
                currentFrame++;
                if (currentTexture == WalkTexture || currentTexture == AttackTexture)
                {
                    if (currentFrame > 7)
                        currentFrame = 0;
                }
                else
                {
                    if (currentFrame > 9)
                        currentFrame = 0;
                }
                frames = 0;
                if (currentTexture == WalkTexture || currentTexture == AttackTexture)
                {
                    colum = (int)((columnsWidth / 8) * currentFrame);
                }
                else
                {
                    colum = (int)((columnsWidth / 10) * currentFrame);
                }

            }


            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
            }

            if (currentTexture == DeadTexture)
            {
                currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds; //Time passed since last Update() 

                if (currentTime >= countDuration)
                {
                    frames = 0;

                }

            }
            //if (currentTexture == WalkTexture)
            //{
            //    sourceRectangle = new Rectangle(colum, row, currentTexture.Width / 11, currentTexture.Height);
            //}
            //else
            //{
            sourceRectangle = new Rectangle(colum, row, currentTexture.Width / 10, currentTexture.Height);
            //}

            if (currentTexture == IdleTexture)
            {
                rectangle = new Rectangle((int)position.X, (int)position.Y, currentTexture.Width / 10, currentTexture.Height);

            }

            if (currentTexture == WalkTexture)
            {
                
                sourceRectangle = new Rectangle(colum, row, currentTexture.Width / 10, currentTexture.Height);


            }

            if (currentTexture == AttackTexture)
            {
                sourceRectangle = new Rectangle(colum, row, currentTexture.Width/10, currentTexture.Height);

            }

            if (currentTexture == DeadTexture)
            {
                sourceRectangle = new Rectangle(colum, row, currentTexture.Width / 10, currentTexture.Height);


            }

            else
            {
                rectangle = new Rectangle((int)position.X, (int)position.Y, currentTexture.Width / 10, currentTexture.Height);
            }

            //currentFrame++;

            if (velocity.Y < 10)
            {
                velocity.Y += 0.4f;
            }


        }



        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                // rectangle.Y = newRectangle.Y - rectangle.Height;
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
            }
            if (rectangle.TouchLeftOf(newRectangle))
            {
                //2 é arrendondado , depende do tamanho da sprite. Ajusta-se o valor conforme a necessidade
                //position.X = newRectangle.X - rectangle.Width -2;
                rectangle.X -= rectangle.Right - newRectangle.Left;
                if (velocity.X > 0)
                    velocity.X = 0;
            }

            Rectangle idleRectangle = rectangle;
            idleRectangle.Width /= 2;

            if (idleRectangle.TouchRightOf(newRectangle))
            {
                //position.X = newRectangle.X + newRectangle.Width + 2;
                rectangle.X += newRectangle.Right - rectangle.Left;
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

            
            if (flip)
                spriteBatch.Draw(currentTexture, rectangle, sourceRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
            else
                spriteBatch.Draw(currentTexture, rectangle, sourceRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0f);


        }
    }
}
