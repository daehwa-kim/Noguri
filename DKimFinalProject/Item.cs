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
    /// Item to be eaten by the main character
    /// </summary>
    public class Item : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Rectangle rectangle;
        private int point = 1;
        private const int WIDTH = 36;
        private const int HEIGHT = 48;
        private const int ALLOWANCE_SIDE = 12;
        private const int ALLOWANCE_BOTTOM = 24;

        public Rectangle Rectangle { get => rectangle; set => rectangle = value; }
        public int Point { get => point; set => point = value; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game object</param>
        /// <param name="spriteBatch">SpriteBatch for drawing</param>
        /// <param name="left">X Position from the left</param>
        /// <param name="level">Ground level to be put</param>
        /// <param name="point">Points worth</param>
        public Item(Game game,
            SpriteBatch spriteBatch,
            int left, int level, int point = 1) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.point = point;
            tex = game.Content.Load<Texture2D>("Images/apple");
            position = new Vector2(Shared.LimitLeft + left, 
                Ground.GetY(level) - HEIGHT);
            rectangle = new Rectangle((int)position.X + ALLOWANCE_SIDE, 
                (int)position.Y,
                WIDTH - (ALLOWANCE_SIDE * 2), HEIGHT - ALLOWANCE_BOTTOM);
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
