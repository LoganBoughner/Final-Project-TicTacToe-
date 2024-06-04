using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Threading;

namespace Final_Project__TicTacToe_
{ 
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        enum Screen
        {
            intro,
            instuctions,
            game,
            end

        }
        KeyboardState keyboardState;
        MouseState mouseState;     
        Texture2D boardTexture;
        Rectangle boardRect;
        Texture2D xTexture; 
        Texture2D oTexture;
        Texture2D emptyTexture;
        Texture2D startTexture;
        Rectangle startRect;
        Texture2D instructionTexture;
        Rectangle instructionRect;
        List<Rectangle> tileRects;
        List<string> tileOwners;
        
        string playerTurn;
        private MouseState oldState;
        private MouseState newState;
        private Screen screen;
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 300;
            _graphics.PreferredBackBufferHeight = 300;
            _graphics.ApplyChanges();
            boardRect = new Rectangle(0, 0, 300, 300);
            playerTurn = "X";
            instructionRect = new Rectangle(0, 75, 200, 75);
            startRect = new Rectangle(0,0, 100,75);

            screen = Screen.intro;

            tileRects = new List<Rectangle>();

            tileRects.Add(new Rectangle(7, 7, 85, 85));
            tileRects.Add(new Rectangle(107, 7, 85, 85));
            tileRects.Add(new Rectangle(208, 7, 85, 85));
            tileRects.Add(new Rectangle(7, 108, 85, 85));
            tileRects.Add(new Rectangle(107, 108, 85, 85));
            tileRects.Add(new Rectangle(208, 108, 85, 85));
            tileRects.Add(new Rectangle(7, 208, 85, 85));
            tileRects.Add(new Rectangle(107, 208, 85, 85));
            tileRects.Add(new Rectangle(208, 208, 85, 85));
           
            tileOwners =
            [
                "", 
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
            ];




            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            boardTexture = Content.Load<Texture2D>("board");
            xTexture = Content.Load<Texture2D>("X");
            oTexture = Content.Load<Texture2D>("O");
            emptyTexture = Content.Load<Texture2D>("Empty");
            startTexture = Content.Load<Texture2D>("Startbtn");
            instructionTexture = Content.Load<Texture2D>("instructionbtn");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            oldState = mouseState;
            mouseState = Mouse.GetState();
           
           
            if (screen == Screen.game)
            if (mouseState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                for (int i = 0; i < tileRects.Count; i++ )
                    if (tileRects[i].Contains(mouseState.Position))
                    {
                       
                        if (tileOwners[i] == "")
                        {
                            if (playerTurn == "X")
                            {
                                tileOwners[i] = "X";
                                if (CheckWin())
                                {
                                    this.Window.Title = "X WIN"; // not perm
                                    //display winner
                                    // add cheering when win is true
                                }
                                else
                                    playerTurn = "O";
                            }
                            else if (playerTurn == "O") 
                            {
                                tileOwners[i] = "O";

                                if (CheckWin())
                                {
                                    this.Window.Title = "O WIN"; // not perm
                                    //display winner
                                    // add cheering when win is true
                                }
                                else
                                    playerTurn = "X";
                            }
                            // Check for a win
                            // Horizontal
                            



                        }
                    }
                    
                
                
                //Check if someone won
                //Figure out why the X/O appear on the board
            }

            

            oldState = newState;
           
            // TODO: Add your update logic here
           
            base.Update(gameTime);
        }

        public bool CheckWin()
        {
            if (tileOwners[0] == tileOwners[1] && tileOwners[0] == tileOwners[2] && tileOwners[0] != "")
            {
                return true;
            }
            else if (tileOwners[3] == tileOwners[4] && tileOwners[3] == tileOwners[5] && tileOwners[3] != "")
            {
                return true;
            }
            else if (tileOwners[6] == tileOwners[7] && tileOwners[6] == tileOwners[8] && tileOwners[6] != "")
            {
                return true;
            }
            else if (tileOwners[0] == tileOwners[4] && tileOwners[0] == tileOwners[8] && tileOwners[0] != "")
            {
                return true;
            }
            else if (tileOwners[2] == tileOwners[4] && tileOwners[2] == tileOwners[6] && tileOwners[0] != "")
            {
                return true;
            }
            else if (tileOwners[0] == tileOwners[3] && tileOwners[0] == tileOwners[6] && tileOwners[0] != "")
            {
                return true;
            }
            else if (tileOwners[1] == tileOwners[4] && tileOwners[1] == tileOwners[7] && tileOwners[1] != "")
            {
                return true;
            }
            else if (tileOwners[2] == tileOwners[5] && tileOwners[2] == tileOwners[8] && tileOwners[2] != "")
            {
                return true;
            }
            
            // all wins are 0,3,6  1,4,7  2,5,8

            return false;
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (screen == Screen.intro)
            {
                _spriteBatch.Draw(startTexture, startRect, Color.White);
                _spriteBatch.Draw(instructionTexture, instructionRect, Color.White);
            }
            
            if (screen == Screen.game)
            {
                _spriteBatch.Draw(boardTexture, boardRect, Color.White);

                for (int i = 0; i < tileRects.Count; i++)
                {
                    if (tileOwners[i] == "X")
                        _spriteBatch.Draw(xTexture, tileRects[i], Color.White);
                    else if (tileOwners[i] == "O")
                        _spriteBatch.Draw(oTexture, tileRects[i], Color.White);
                    else
                        _spriteBatch.Draw(emptyTexture, tileRects[i], Color.Red);
                }
            }
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
