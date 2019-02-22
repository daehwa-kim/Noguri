/*
 * Program ID: DKimFinalProject - Noguri.cs
 * 
 * Purpose: To make a game using MonoGame
 * 
 * Revision History
 *  November 25, 2018 Dae Hwa Kim: Written
 *  
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DKimFinalProject
{
    /// <summary>
    /// Main character of the game
    /// </summary>
    public class Noguri : DrawableGameComponent
    {
        private GameNoguri game;
        private PlayScene scene;
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 speed;
        private Rectangle rectangle;
        private Rectangle[,] frames;
        private SoundEffect jumpSound;
        private SoundEffect dieSound;
        private SoundEffect itemSound;
        private Song fallSound;
        private int direction = 0;
        private bool isJumping = false;
        private bool isStabbed = false;
        private bool isFalling = false;
        private bool isLadderBehind = false;
        private bool isOnLadder = false;
        private bool fallStarted = false;
        private Ladder ladder = null;
        private int frameRow = 2;
        private int frameIndex = 2; //-1
        private KeyboardState oldKs;
        private int jumpCounter = 0;
        private int openStageCounter = 0;
        private int delayCounter = 0;
        private const int DELAY = 5;
        private const int DELAY_OPENSTAGE = 60;
        private const int JUMPCOUNTER_MAX = 24;
        private const int WIDTH = 48;
        private const int HEIGHT = 48;
        private const int ALLOWANCE_SIDE = 9;
        private const int FRAMEROW_L = 0;
        private const int FRAMEROW_R = 1;
        private const int FRAMEROW_LJUMP = 3;
        private const int FRAMEROW_RJUMP = 4;
        private const int FRAME_TOTAL_ROW = 6;
        private const int FRAME_TOTAL_COL = 4;
        private const int SPEED_X = 3;
        private const int SPEED_JUMP_X = 3;
        private const int SPEED_JUMP_Y = 2;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game object</param>
        /// <param name="scene">PlayScene where game is playing</param>
        /// <param name="spriteBatch">SpriteBatch for drawing</param>
        public Noguri(Game game,
            PlayScene scene,
            SpriteBatch spriteBatch) : base(game)
        {
            this.game = (GameNoguri)game;
            this.scene = scene;
            this.spriteBatch = spriteBatch;
            this.position = new Vector2(Shared.LimitRightLevelZero - 
                WIDTH, Ground.GetY(0) - HEIGHT);
            tex = game.Content.Load<Texture2D>("Images/noguri");
            speed = new Vector2(SPEED_X, 0);
            rectangle = new Rectangle((int)position.X + ALLOWANCE_SIDE,
                (int)position.Y, WIDTH - (ALLOWANCE_SIDE * 2), HEIGHT);

            // Load sounds
            jumpSound = game.Content.Load<SoundEffect>("Sounds/jump");
            dieSound = game.Content.Load<SoundEffect>("Sounds/die");
            itemSound = game.Content.Load<SoundEffect>("Sounds/item");
            fallSound = game.Content.Load<Song>("Sounds/fall");

            // Create image frames
            CreateFrames();
        }

        /// <summary>
        /// Update data
        /// </summary>
        /// <param name="gameTime">GameTime object</param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (isFalling)
            {
                Fall();
            }
            else if (!isOnLadder)
            {
                if (isJumping)
                {
                    Jump(direction == 0 ? FRAMEROW_LJUMP : FRAMEROW_RJUMP);
                }
                else if (isStabbed)
                {
                    frameRow = 5;
                    frameIndex = 2;
                }
                else
                {
                    Move();
                }
            }

            // Set the limit
            if (position.X < Shared.LimitLeft)
            {
                // End of Left
                position.X = Shared.LimitLeft;
            }
            else
            {
                // End of Right

                if (position.Y > Ground.GetY(1))
                {
                    // Level 0 Exception
                    if (position.X + WIDTH > Shared.LimitRightLevelZero)
                    {
                        position.X = Shared.LimitRightLevelZero - WIDTH;
                    }
                }
                else if (position.X + WIDTH > Shared.LimitRight)
                {
                    position.X = Shared.LimitRight - WIDTH;
                }
            }

            // Sync rectangle with position
            rectangle.X = (int)position.X + ALLOWANCE_SIDE;
            rectangle.Y = (int)position.Y;

            // Tacks
            foreach (Tack tack in scene.tacks)
            {
                if (rectangle.Intersects(tack.Rectangle) && !isJumping)
                {
                    HitTack();
                }
            }

            // Ladders
            isLadderBehind = false;
            foreach (Ladder ladder in scene.ladders)
            {
                if (rectangle.Intersects(ladder.Rectangle) && !isFalling)
                {
                    this.ladder = ladder;
                    isLadderBehind = true;
                    Climb();
                }
            }

            // Centipedes
            foreach (Centipede centipede in scene.centipedes)
            {
                if (rectangle.Intersects(centipede.Rectangle))
                {
                    isFalling = true;
                }
            }

            // Grounds
            int intersectsCount = 0;
            foreach (Ground ground in scene.grounds)
            {
                if (rectangle.Intersects(ground.Rectangle))
                {
                    intersectsCount++;
                }
            }

            if (intersectsCount == 0 && !isJumping)
            {
                isFalling = true;
            }

            // Items
            int itemCount = 0;
            foreach (Item item in scene.items)
            {
                if (rectangle.Intersects(item.Rectangle) && !isJumping 
                    && !isFalling && !isOnLadder)
                {
                    if (item.Visible)
                    {
                        itemSound.Play();
                        game.Score += item.Point;
                        item.Enabled = false;
                        item.Visible = false;
                    }
                }
                if (item.Visible)
                {
                    itemCount++;
                }
            }

            if (itemCount == 0)
            {
                if (scene.Stage < Shared.FinalStage)
                {
                    // Go to next stage
                    game.NewPlayScene(scene.Stage + 1, true);
                }
                else
                {
                    // Show clear scene
                    scene.Hide();
                    game.ClearScene.Show();
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draw on screen
        /// </summary>
        /// <param name="gameTime">GameTime object</param>
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

        /// <summary>
        /// Animate jumping
        /// </summary>
        /// <param name="frameRow">Frame Row for animation</param>
        private void Jump(int frameRow)
        {
            if (jumpCounter == 0)
            {
                this.frameRow = frameRow;
                speed = new Vector2(SPEED_JUMP_X *
                    (direction == 0 ? -1 : 1), -SPEED_JUMP_Y);
            }
            if (jumpCounter == (JUMPCOUNTER_MAX / 2))
            {
                speed = new Vector2(SPEED_JUMP_X *
                    (direction == 0 ? -1 : 1), SPEED_JUMP_Y);
            }
            else if (jumpCounter >= JUMPCOUNTER_MAX)
            {
                isJumping = false;
                speed = new Vector2(SPEED_X, 0);
                jumpCounter = 0;
                this.frameRow = direction == 0 ? 
                    FRAMEROW_L : FRAMEROW_R;
                return;
            }
            position += speed;
            jumpCounter++;
            PlayFrames();
        }

        /// <summary>
        /// Animate falling
        /// </summary>
        private void Fall()
        {
            frameRow = 5;
            speed.X = 0;
            speed.Y = 2;
            position += speed;

            if (!fallStarted)
            {
                MediaPlayer.Play(fallSound);
                fallStarted = true;
            }

            if (position.Y + HEIGHT >=
                    Ground.GetY(0))
            {
                position.Y = Ground.GetY(0) - HEIGHT;

                if (openStageCounter == 0)
                {
                    MediaPlayer.Stop();
                    dieSound.Play();
                }

                openStageCounter++;

                if (openStageCounter >= DELAY_OPENSTAGE)
                {
                    if (game.Life > 1)
                    {
                        game.Life--;
                        game.NewPlayScene(scene.Stage); //Restart
                    }
                    else
                    {
                        scene.Hide();
                        game.GameOverScene.Show();
                    }
                }
            }
            else
            {
                PlayFrames();
            }
        }

        /// <summary>
        /// Animate climbing
        /// </summary>
        private void Climb()
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Up) && isLadderBehind 
                && !isJumping)
            {
                isOnLadder = true;
                frameRow = 2;
                //frameIndex = 0;
                speed.X = 0;
                speed.Y = 2;
                position -= speed;
                if (position.Y + HEIGHT <= 
                    ladder.Rectangle.Y + ladder.MarginTop)
                {
                    position.Y = ladder.Rectangle.Y +
                        ladder.MarginTop - HEIGHT;
                    frameIndex = 0;
                    isOnLadder = false;
                }
                else
                {
                    PlayFrames();
                }
            }
            else if (ks.IsKeyDown(Keys.Down) && isLadderBehind 
                && !isJumping)
            {
                isOnLadder = true;
                frameRow = 2;
                //frameIndex = 0;
                speed.X = 0;
                speed.Y = 2;
                position += speed;
                if (position.Y + HEIGHT >=
                    ladder.Rectangle.Y + ladder.Rectangle.Height)
                {
                    position.Y = ladder.Rectangle.Y +
                        ladder.Rectangle.Height - HEIGHT;
                    frameIndex = 0;
                    isOnLadder = false;
                }
                else
                {
                    PlayFrames();
                }
            }
        }

        /// <summary>
        /// Animate moving
        /// </summary>
        private void Move()
        {
            KeyboardState ks = Keyboard.GetState();

            speed.X = SPEED_X;
            speed.Y = 0;

            if (ks.IsKeyDown(Keys.Left))
            {
                direction = 0;
                if (ks.IsKeyDown(Keys.Space))
                {
                    isJumping = true;
                    jumpSound.Play();
                }
                else
                {
                    frameRow = FRAMEROW_L;
                    position -= speed;
                }
                PlayFrames();
            }
            else if (ks.IsKeyDown(Keys.Right))
            {
                direction = 1;
                if (ks.IsKeyDown(Keys.Space))
                {
                    isJumping = true;
                    jumpSound.Play();
                }
                else
                {
                    frameRow = FRAMEROW_R;
                    position += speed;
                }
                PlayFrames();
            }

            if (ks.IsKeyDown(Keys.Space) && oldKs.IsKeyUp(Keys.Space))
            {
                isJumping = true;
                jumpSound.Play();
            }

            oldKs = ks;
        }

        /// <summary>
        /// Hit a tack
        /// </summary>
        private void HitTack()
        {
            isStabbed = true;

            if (openStageCounter == 0)
            {
                dieSound.Play();
            }
            openStageCounter++;

            if (openStageCounter >= DELAY_OPENSTAGE)
            {
                MediaPlayer.Stop();
                if (game.Life > 1)
                {
                    game.Life--;
                    game.NewPlayScene(scene.Stage); //Restart
                }
                else
                {
                    scene.Hide();
                    game.GameOverScene.Show();
                }
            }
            
        }

        /// <summary>
        /// Create image frames
        /// </summary>
        private void CreateFrames()
        {
            frames = new Rectangle[FRAME_TOTAL_ROW, FRAME_TOTAL_COL];
            for (int i = 0; i < frames.GetLength(0); i++)
            {
                for (int j = 0; j < frames.GetLength(1); j++)
                {
                    int x = j * WIDTH;
                    int y = i * HEIGHT;
                    Rectangle r = new Rectangle(x, y, WIDTH,
                        HEIGHT);
                    frames[i, j] = r;
                }
            }
        }

        /// <summary>
        /// Play image frames
        /// </summary>
        private void PlayFrames()
        {
            delayCounter++;
            if (delayCounter > DELAY)
            {
                frameIndex++;
                if (frameIndex >= 2 && isOnLadder)
                {
                    frameIndex = 0;
                }
                else if (frameIndex >= frames.GetLength(1))
                {
                    frameIndex = 0;
                }
                delayCounter = 0;
            }
        }
    }
}
