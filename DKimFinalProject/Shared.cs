/*
 * Program ID: DKimFinalProject - Shared.cs
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
    /// Pre-defined gameplay data
    /// </summary>
    public class Shared
    {
        private static int limitLeft = 48;
        private static int limitRight = 720;
        private static int limitRightLevelZero = 792;
        private static int finalStage = 3;
        public static int LimitLeft { get => limitLeft; set => limitLeft = value; }
        public static int LimitRight { get => limitRight; set => limitRight = value; }
        public static int LimitRightLevelZero { get => limitRightLevelZero; set => limitRightLevelZero = value; }
        public static int FinalStage { get => finalStage; set => finalStage = value; }

        /// <summary>
        /// Load pre-defined stage data into lists
        /// </summary>
        /// <param name="game">Game object</param>
        /// <param name="scene">PlayScene where game is playing</param>
        /// <param name="stage">Stage number to load data for</param>
        /// <param name="grounds">Ground list</param>
        /// <param name="ladders">Ladder list</param>
        /// <param name="tacks">Tack list</param>
        /// <param name="items">Item list</param>
        /// <param name="centipedes">Centipede list</param>
        public static void SetStage(Game game, SpriteBatch spriteBatch, 
            int stage, List<Ground> grounds, List<Ladder> ladders, 
            List<Tack> tacks, List<Item> items, 
            List<Centipede> centipedes)
        {
            switch (stage)
            {
                case 1:
                    // Grounds
                    grounds.Clear();
                    grounds.Add(new Ground(game, spriteBatch, 0, 744, 0));
                    grounds.Add(new Ground(game, spriteBatch, 0, 192, 1));
                    grounds.Add(new Ground(game, spriteBatch, 216, 456, 1));
                    grounds.Add(new Ground(game, spriteBatch, 0, 456, 2));
                    grounds.Add(new Ground(game, spriteBatch, 480, 192, 2));
                    grounds.Add(new Ground(game, spriteBatch, 0, 192, 3));
                    grounds.Add(new Ground(game, spriteBatch, 240, 432, 3));
                    grounds.Add(new Ground(game, spriteBatch, 0, 48, 4));
                    grounds.Add(new Ground(game, spriteBatch, 72, 96, 4));
                    grounds.Add(new Ground(game, spriteBatch, 192, 192, 4));
                    grounds.Add(new Ground(game, spriteBatch, 408, 264, 4));

                    // Ladders
                    ladders.Clear();
                    ladders.Add(new Ladder(game, spriteBatch, 360, 0));
                    ladders.Add(new Ladder(game, spriteBatch, 504, 1));
                    ladders.Add(new Ladder(game, spriteBatch, 144, 2));
                    ladders.Add(new Ladder(game, spriteBatch, 336, 3));
                    ladders.Add(new Ladder(game, spriteBatch, 600, 3));

                    // Items
                    items.Clear();
                    items.Add(new Item(game, spriteBatch, 198, 0, 1));
                    items.Add(new Item(game, spriteBatch, 30, 1, 3));
                    items.Add(new Item(game, spriteBatch, 320, 1, 3));
                    items.Add(new Item(game, spriteBatch, 582, 1, 3));
                    items.Add(new Item(game, spriteBatch, 6, 3, 7));
                    items.Add(new Item(game, spriteBatch, 246, 3, 7));
                    items.Add(new Item(game, spriteBatch, 510, 3, 7));
                    items.Add(new Item(game, spriteBatch, 216, 4, 9));
                    items.Add(new Item(game, spriteBatch, 318, 4, 9));
                    items.Add(new Item(game, spriteBatch, 462, 4, 9));

                    // Centipede
                    centipedes.Clear();
                    centipedes.Add(new Centipede(game, spriteBatch, 20, 2, 2));
                    centipedes.Add(new Centipede(game, spriteBatch, 20, 4, 4));

                    break;
                case 2:
                    // Grounds
                    grounds.Clear();
                    grounds.Add(new Ground(game, spriteBatch, 0, 744, 0));
                    grounds.Add(new Ground(game, spriteBatch, 0, 300, 1));
                    grounds.Add(new Ground(game, spriteBatch, 408, 336, 1));
                    grounds.Add(new Ground(game, spriteBatch, 0, 390, 2));
                    grounds.Add(new Ground(game, spriteBatch, 420, 150, 2));
                    grounds.Add(new Ground(game, spriteBatch, 0, 48, 3));
                    grounds.Add(new Ground(game, spriteBatch, 72, 144, 3));
                    grounds.Add(new Ground(game, spriteBatch, 240, 192, 3));
                    grounds.Add(new Ground(game, spriteBatch, 456, 216, 3));
                    grounds.Add(new Ground(game, spriteBatch, 0, 192, 4));
                    grounds.Add(new Ground(game, spriteBatch, 240, 432, 4));

                    // Tacks
                    tacks.Clear();
                    tacks.Add(new Tack(game, spriteBatch, 540, 0));
                    tacks.Add(new Tack(game, spriteBatch, 120, 0));
                    tacks.Add(new Tack(game, spriteBatch, 192, 0));
                    tacks.Add(new Tack(game, spriteBatch, 240, 0));
                    tacks.Add(new Tack(game, spriteBatch, 480, 3));

                    // Ladders
                    ladders.Clear();
                    ladders.Add(new Ladder(game, spriteBatch, 75, 0));
                    ladders.Add(new Ladder(game, spriteBatch, 240, 1));
                    ladders.Add(new Ladder(game, spriteBatch, 504, 1));
                    ladders.Add(new Ladder(game, spriteBatch, 120, 2));
                    ladders.Add(new Ladder(game, spriteBatch, 285, 3));

                    // Items
                    items.Clear();
                    items.Add(new Item(game, spriteBatch, 15, 1, 5));
                    items.Add(new Item(game, spriteBatch, 582, 1, 5));
                    items.Add(new Item(game, spriteBatch, 6, 3, 9));
                    items.Add(new Item(game, spriteBatch, 246, 3, 9));
                    items.Add(new Item(game, spriteBatch, 510, 3, 9));
                    items.Add(new Item(game, spriteBatch, 150, 4, 11));
                    items.Add(new Item(game, spriteBatch, 330, 4, 11));
                    items.Add(new Item(game, spriteBatch, 510, 4, 11));

                    // Centipede
                    centipedes.Clear();
                    centipedes.Add(new Centipede(game, spriteBatch, 240, 1, 4));
                    centipedes.Add(new Centipede(game, spriteBatch, 20, 2, 2));
                    centipedes.Add(new Centipede(game, spriteBatch, 420, 4, 3));

                    break;
                case 3:
                    // Grounds
                    grounds.Clear();
                    grounds.Add(new Ground(game, spriteBatch, 0, 744, 0));
                    grounds.Add(new Ground(game, spriteBatch, 0, 240, 1));
                    grounds.Add(new Ground(game, spriteBatch, 264, 408, 1));
                    grounds.Add(new Ground(game, spriteBatch, 0, 96, 2));
                    grounds.Add(new Ground(game, spriteBatch, 120, 48, 2));
                    grounds.Add(new Ground(game, spriteBatch, 192, 48, 2));
                    grounds.Add(new Ground(game, spriteBatch, 264, 48, 2));
                    grounds.Add(new Ground(game, spriteBatch, 360, 48, 2));
                    grounds.Add(new Ground(game, spriteBatch, 432, 48, 2));
                    grounds.Add(new Ground(game, spriteBatch, 504, 48, 2));
                    grounds.Add(new Ground(game, spriteBatch, 576, 96, 2));
                    grounds.Add(new Ground(game, spriteBatch, 0, 540, 3));
                    grounds.Add(new Ground(game, spriteBatch, 120, 120, 4));
                    grounds.Add(new Ground(game, spriteBatch, 288, 384, 4));

                    // Tacks
                    tacks.Clear();
                    tacks.Add(new Tack(game, spriteBatch, 132, 0));
                    tacks.Add(new Tack(game, spriteBatch, 180, 0));
                    tacks.Add(new Tack(game, spriteBatch, 300, 0));
                    tacks.Add(new Tack(game, spriteBatch, 330, 0));
                    tacks.Add(new Tack(game, spriteBatch, 360, 0));
                    tacks.Add(new Tack(game, spriteBatch, 480, 0));
                    tacks.Add(new Tack(game, spriteBatch, 156, 4));

                    // Ladders
                    ladders.Clear();
                    ladders.Add(new Ladder(game, spriteBatch, 75, 0));
                    ladders.Add(new Ladder(game, spriteBatch, 393, 0));
                    ladders.Add(new Ladder(game, spriteBatch, 516, 1));
                    ladders.Add(new Ladder(game, spriteBatch, 120, 2));
                    ladders.Add(new Ladder(game, spriteBatch, 330, 3));

                    // Items
                    items.Clear();
                    items.Add(new Item(game, spriteBatch, 210, 0, 5));
                    items.Add(new Item(game, spriteBatch, 15, 1, 7));
                    items.Add(new Item(game, spriteBatch, 300, 1, 7));
                    items.Add(new Item(game, spriteBatch, 582, 1, 7));
                    items.Add(new Item(game, spriteBatch, 270, 2, 9));
                    items.Add(new Item(game, spriteBatch, 30, 2, 9));
                    items.Add(new Item(game, spriteBatch, 510, 2, 9));
                    items.Add(new Item(game, spriteBatch, 630, 2, 9));
                    items.Add(new Item(game, spriteBatch, 6, 3, 11));
                    items.Add(new Item(game, spriteBatch, 369, 3, 11));
                    items.Add(new Item(game, spriteBatch, 120, 4, 13));
                    items.Add(new Item(game, spriteBatch, 540, 4, 13));

                    // Centipede
                    centipedes.Clear();
                    centipedes.Add(new Centipede(game, spriteBatch, 120, 0, 3));
                    centipedes.Add(new Centipede(game, spriteBatch, 20, 2, 1));
                    centipedes.Add(new Centipede(game, spriteBatch, 420, 3, 4));
                    centipedes.Add(new Centipede(game, spriteBatch, 300, 3, 2));
                    centipedes.Add(new Centipede(game, spriteBatch, 540, 4, 1));

                    break;
            }
        }
    }
}
