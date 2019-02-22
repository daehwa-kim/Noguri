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
    /// Menu to open a scene
    /// </summary>
    public class MenuComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont menuFont, menuFocusedFont;
        private List<string> menuItems;
        private const int POSITION_X = 70;
        private const int POSITION_Y = 380;

        public int SelectedIndex { get; set; } = 0;
        private Vector2 position;
        private Color menuColor = Color.Black;
        private Color menuFocusedColor = Color.Red;

        private KeyboardState oldState;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game object</param>
        /// <param name="spriteBatch">SpriteBatch for drawing</param>
        /// <param name="menuFont">Font for menu items</param>
        /// <param name="menuFocusedFont">Font for selected menu item</param>
        /// <param name="menus">Menu array</param>
        public MenuComponent(Game game,
            SpriteBatch spriteBatch,
            SpriteFont menuFont,
            SpriteFont menuFocusedFont,
            string[] menus) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.menuFont = menuFont;
            this.menuFocusedFont = menuFocusedFont;
            this.menuItems = menus.ToList<string>();
            this.position = new Vector2(POSITION_X,
                POSITION_Y);
        }

        /// <summary>
        /// Update data
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            //will not work!
            if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                SelectedIndex++;
                if (SelectedIndex >= menuItems.Count)
                {
                    SelectedIndex = 0;
                }
            }
            if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                SelectedIndex--;
                if (SelectedIndex < 0)
                {
                    SelectedIndex = menuItems.Count - 1;
                }
            }

            oldState = ks;

            base.Update(gameTime);
        }

        /// <summary>
        /// Draw on screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPos = position;
            spriteBatch.Begin();
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (SelectedIndex == i)
                {
                    spriteBatch.DrawString(menuFocusedFont,
                        menuItems[i], tempPos, menuFocusedColor);
                    tempPos.Y += menuFocusedFont.LineSpacing;
                }
                else
                {
                    spriteBatch.DrawString(menuFont,
                        menuItems[i], tempPos, menuColor);
                    tempPos.Y += menuFont.LineSpacing;
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
