/*
 * Program ID: DKimFinalProject - Ladder.cs
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
    /// Ladder to be used to go up to the upper ground
    /// </summary>
    public class Ladder : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Rectangle rectangle;
        private int marginTop = 3;
        private const int WIDTH = 24;
        private const int HEIGHT = 96;
        private const int ALLOWANCE_SIDE = 11;

        public Rectangle Rectangle { get => rectangle; set => rectangle = value; }
        public int MarginTop { get => marginTop; set => marginTop = value; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game object</param>
        /// <param name="spriteBatch">SpriteBatch for drawing</param>
        /// <param name="left">X Position from the left</param>=
        /// <param name="level">Ground level to be put</param>
        public Ladder(Game game,
            SpriteBatch spriteBatch,
            int left, int level) : base(game)
        {
            this.spriteBatch = spriteBatch;
            tex = game.Content.Load<Texture2D>("Images/ladder");
            position = new Vector2(Shared.LimitLeft + left, 
                Ground.GetY(level) - HEIGHT);
            rectangle = new Rectangle((int)position.X + ALLOWANCE_SIDE, 
                (int)position.Y - marginTop,
                WIDTH - (ALLOWANCE_SIDE * 2), HEIGHT + marginTop);
        }

        /// <summary>
        /// Update data
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
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
