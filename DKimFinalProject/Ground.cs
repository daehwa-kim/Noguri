/*
 * Program ID: DKimFinalProject - Ground.cs
 * 
 * Purpose: To make a game using MonoGame
 * 
 * Revision History
 *  November 25, 2018 Dae Hwa Kim: Written
 *  
 */

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
    /// <summary>
    /// Ground on which the main character stands
    /// </summary>
    public class Ground : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Texture2D texLeft;
        private Texture2D texRight;
        private Vector2 position;
        private Rectangle rectLeft;
        private Rectangle rectMiddle;
        private Rectangle rectRight;
        private Rectangle rectangle;
        private int marginTop = 72;
        private const int HEIGHT = 24;
        private const int ALLOWANCE_SIDE = 18;
        private const int WIDTH_SIDE = 24;
        private const int LEVEL_MAX = 4;
        private const int LEVEL_GAP = 96;
        private const int LEVEL_MARGIN = 168;

        public Rectangle Rectangle { get => rectangle; set => rectangle = value; }
        public int MarginTop { get => marginTop; set => marginTop = value; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game object</param>
        /// <param name="spriteBatch">SpriteBatch for drawing</param>
        /// <param name="left">X Position from the left</param>
        /// <param name="width">Width of the object</param>
        /// <param name="level">Ground level to be put</param>
        public Ground(Game game,
            SpriteBatch spriteBatch,
            int left, int width, 
            int level) : base(game)
        {
            this.spriteBatch = spriteBatch;
            tex = 
                game.Content.Load<Texture2D>("Images/ground");
            texLeft = 
                game.Content.Load<Texture2D>("Images/ground_left");
            texRight = 
                game.Content.Load<Texture2D>("Images/ground_right");
            position = 
                new Vector2(Shared.LimitLeft + left, GetY(level));
            rectLeft = new Rectangle((int)position.X, (int)position.Y,
                WIDTH_SIDE, HEIGHT);
            rectMiddle = new Rectangle((int)position.X + WIDTH_SIDE, 
                (int)position.Y, width - (WIDTH_SIDE * 2), HEIGHT);
            rectRight = new Rectangle
                ((int)position.X + width - WIDTH_SIDE, 
                (int)position.Y, WIDTH_SIDE, HEIGHT);
            rectangle = new Rectangle
                ((int)position.X + ALLOWANCE_SIDE,
                (int)position.Y - marginTop,
                width - (ALLOWANCE_SIDE * 2), HEIGHT + marginTop);
        }

        /// <summary>
        /// Update Data
        /// </summary>
        /// <param name="gameTime">GameTime object</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// Draw on screen
        /// </summary>
        /// <param name="gameTime">GameTime object</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texLeft, rectLeft, Color.White);
            spriteBatch.Draw(tex, rectMiddle, Color.White);
            spriteBatch.Draw(texRight, rectRight, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Calculate the Y position by the specified level
        /// </summary>
        /// <param name="level">Ground level to get the Y position</param>
        /// <returns></returns>
        public static int GetY(int level)
        {
            return (LEVEL_MAX - level) * LEVEL_GAP + LEVEL_MARGIN;
        }
    }
}
