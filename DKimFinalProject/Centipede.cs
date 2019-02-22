using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DKimFinalProject
{
    public class Centipede : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 dimension;
        private Vector2 position;
        private Vector2 speed;
        private Rectangle rectangle;
        private Rectangle[,] frames;
        private int direction = 0;
        private int frameRow = 0;
        private int frameIndex = 0;
        private int delayCounter;
        private const int DELAY = 5;
        private const int WIDTH = 48;
        private const int HEIGHT = 48;
        private const int ALLOWANCE_SIDE = 9;
        private const int ALLOWANCE_BOTTOM = 3;

        public Rectangle Rectangle { get => rectangle; set => rectangle = value; }

        public Centipede(Game game,
            SpriteBatch spriteBatch,
            int left, int level, int speedX) : base(game)
        {
            this.spriteBatch = spriteBatch;
            position = new Vector2(Shared.LimitLeft + left, 
                Ground.GetY(level) - HEIGHT);
            tex = game.Content.Load<Texture2D>("Images/centipede");
            dimension = new Vector2(WIDTH, HEIGHT);
            speed = new Vector2(speedX, 0);
            rectangle = new Rectangle((int)position.X + ALLOWANCE_SIDE,
                (int)position.Y, WIDTH - (ALLOWANCE_SIDE * 2), 
                HEIGHT - ALLOWANCE_BOTTOM);

            // Create frames
            CreateFrames();
        }

        public override void Update(GameTime gameTime)
        {
            if (direction == 0)
            {
                frameRow = 1;
                position -= speed;
            }
            else if (direction == 1)
            {
                frameRow = 0;
                position += speed;
            }

            // Set the limit
            if (position.X < Shared.LimitLeft)
            {
                // End of Left
                position.X = Shared.LimitLeft;
                direction = 1;
            }
            else if (position.X + WIDTH > Shared.LimitRight)
            {
                // End of Right
                position.X = Shared.LimitRight - WIDTH;
                direction = 0;
            }

            rectangle.X = (int)position.X + ALLOWANCE_SIDE;
            rectangle.Y = (int)position.Y;

            PlayFrames();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (frameIndex >= 0)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(tex, position,
                    frames[frameRow, frameIndex], Color.White);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        private void CreateFrames()
        {
            frames = new Rectangle[2, 2];
            for (int i = 0; i < frames.GetLength(0); i++)
            {
                for (int j = 0; j < frames.GetLength(1); j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X,
                        (int)dimension.Y);
                    frames[i, j] = r;
                }
            }
        }

        private void PlayFrames()
        {
            delayCounter++;
            if (delayCounter > DELAY)
            {
                frameIndex++;
                if (frameIndex >= frames.GetLength(1))
                {
                    frameIndex = 0;
                }
                delayCounter = 0;
            }
        }
    }
}
