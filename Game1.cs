using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
            singleplayer,
            xWin,
            oWin,
            tie,
            singleplayerxwin,
            singleplayerowin,
            singleplayertie

        }
        SoundEffect cheer;
        KeyboardState keyboardState;
        MouseState mouseState;     
        Texture2D boardTexture;
        Rectangle boardRect;
        Texture2D cursorTexture;
        Rectangle cursorRect;
        Texture2D xTexture; 
        Texture2D oTexture;
        Texture2D emptyTexture;
        Texture2D startTexture;
        Rectangle startRect;
        Texture2D aiButtonTexture;
        Rectangle aiButtonRect;
        Texture2D instructionTexture;
        Rectangle instructionRect;
        Texture2D backbtnTexture;
        Rectangle backbtnRect;
        List<Rectangle> tileRects;
        List<string> tileOwners;
        SpriteFont TextFont;
        SpriteFont WinFont;
        string playerTurn;
        private MouseState oldState;
        private MouseState newState;
        private Screen screen;
        int xWins;
        int oWins;
        int ties;
        int randNum;
        Random random;
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
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
            startRect = new Rectangle(0, 0, 100, 75);
            aiButtonRect = new Rectangle(200, 12, 100, 50);
            backbtnRect = new Rectangle(200, 200, 100, 100);
            
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
            cursorTexture = xTexture;
            cursorRect = new Rectangle(0,0, 20,20);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            boardTexture = Content.Load<Texture2D>("board");
            xTexture = Content.Load<Texture2D>("X");
            oTexture = Content.Load<Texture2D>("O");
            emptyTexture = Content.Load<Texture2D>("Empty");
            startTexture = Content.Load<Texture2D>("Startbtn");
            aiButtonTexture = Content.Load<Texture2D>("aibutton");
            instructionTexture = Content.Load<Texture2D>("instructionbtn");
            backbtnTexture = Content.Load<Texture2D>("backbtn");
            TextFont = Content.Load<SpriteFont>("instructions");
            WinFont = Content.Load<SpriteFont>("wins");
            cheer = Content.Load<SoundEffect>("Cheering");
            xWins = 0;
            oWins = 0;
            ties = 0;
            random = new Random();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            oldState = mouseState;
            mouseState = Mouse.GetState();
            cursorRect.Location = mouseState.Position;
            if (screen == Screen.intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
                {
                    if (startRect.Contains(cursorRect))
                    {
                        screen = Screen.game;
                    }
                    else if (instructionRect.Contains(mouseState.Position))
                    {
                        screen = Screen.instuctions;
                    }
                    else if (aiButtonRect.Contains(cursorRect))
                        screen = Screen.singleplayer;
                }
            }
            else if (screen == Screen.instuctions)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
                {
                    if (backbtnRect.Contains(cursorRect))
                    {
                        screen = Screen.intro;
                    }
                }
            }
            

            else if (screen == Screen.game)
            {
                
                if (mouseState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
                {
                    
                    for (int i = 0; i < tileRects.Count; i++)
                        if (tileRects[i].Contains(cursorRect))
                        {

                            if (tileOwners[i] == "")
                            {
                                if (playerTurn == "X")
                                {
                                    tileOwners[i] = "X";
                                    if (CheckWin())
                                    {
                                        screen = Screen.xWin;
                                        playerTurn = "X";
                                        xWins += 1;
                                        cheer.Play();
                                    }
                                    else if (tileOwners[0] != "" && tileOwners[1] != "" && tileOwners[2] != "" && tileOwners[3] != "" && tileOwners[4] != "" && tileOwners[5] != "" && tileOwners[6] != "" && tileOwners[7] != "" && tileOwners[8] != "")
                                    {
                                        screen = Screen.tie;
                                        ties += 1;
                                    }
                                    else
                                    {
                                        playerTurn = "O";
                                        cursorTexture = oTexture;
                                    }
                                }
                                else if (playerTurn == "O")
                                {
                                    tileOwners[i] = "O";

                                    if (CheckWin())
                                    {
                                        screen = Screen.oWin;
                                        playerTurn = "X";
                                        oWins += 1;
                                        cursorTexture = xTexture;
                                        cheer.Play();
                                    }
                                    else if (tileOwners[0] != "" && tileOwners[1] != "" && tileOwners[2] != "" && tileOwners[3] != "" && tileOwners[4] != "" && tileOwners[5] != "" && tileOwners[6] != "" && tileOwners[7] != "" && tileOwners[8] != "")
                                    {
                                        ties += 1;
                                        screen = Screen.tie;
                                    }
                                    else
                                    {
                                        playerTurn = "X";
                                        cursorTexture = xTexture;
                                    }
                                }                               
                            }
                        }
                    
                }
            }
            else if (screen == Screen.singleplayer)
            { 
                if (mouseState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
                {
                    
                    for (int i = 0; i < tileRects.Count; i++)
                        if (tileRects[i].Contains(cursorRect))
                        {

                            if (tileOwners[i] == "")
                            {
                                if (playerTurn == "X")
                                {
                                    tileOwners[i] = "X";
                                    if (CheckWin())
                                    {
                                        screen = Screen.singleplayerxwin;
                                        playerTurn = "X";
                                        xWins += 1;
                                        cheer.Play();
                                    }
                                    else if (tileOwners[0] != "" && tileOwners[1] != "" && tileOwners[2] != "" && tileOwners[3] != "" && tileOwners[4] != "" && tileOwners[5] != "" && tileOwners[6] != "" && tileOwners[7] != "" && tileOwners[8] != "")
                                    {
                                        screen = Screen.singleplayertie;
                                        ties += 1;
                                    }
                                    else
                                    {
                                        playerTurn = "O";

                                        // Computer goes
                                        tileOwners[ComputerTurn()] = "O";
                                        if (CheckWin() && playerTurn == "O")
                                        {
                                            screen = Screen.singleplayerowin;
                                            playerTurn = "X";
                                            oWins += 1;
                                        }
                                        else playerTurn = "X";



                                    }
                                }
                                
                            }
                        }

                    
                }
            }
            
            else if (screen == Screen.xWin || screen == Screen.oWin || screen == Screen.tie)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
                {
                    if (backbtnRect.Contains(cursorRect))
                    {
                        screen = Screen.game;
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
                    }
                }
            }
            else if (screen == Screen.singleplayerxwin || screen == Screen.singleplayerowin || screen == Screen.singleplayertie)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
                {
                    if (backbtnRect.Contains(cursorRect))
                    {
                        screen = Screen.singleplayer;
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
                    }
                }
            }



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
            else if (tileOwners[2] == tileOwners[4] && tileOwners[2] == tileOwners[6] && tileOwners[2] != "")
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
                _spriteBatch.Draw(aiButtonTexture, aiButtonRect, Color.White);
            }
            else if (screen == Screen.instuctions)
            {
                _spriteBatch.Draw(backbtnTexture, backbtnRect, Color.White);
                _spriteBatch.DrawString(TextFont, "Click on the square you want to place your \nX/O in. \nAfter someone wins press the back button \n(same one as in this tab) to go back \nto the game screen." , new Vector2(0, 0), Color.Black);

            }
            
            else if (screen == Screen.game || screen == Screen.singleplayer)
            {
                _spriteBatch.Draw(boardTexture, boardRect, Color.White);

                for (int i = 0; i < tileRects.Count; i++)
                {
                    if (tileOwners[i] == "X")
                        _spriteBatch.Draw(xTexture, tileRects[i], Color.White);
                    else if (tileOwners[i] == "O")
                        _spriteBatch.Draw(oTexture, tileRects[i], Color.White);
                    else
                        _spriteBatch.Draw(emptyTexture, tileRects[i], Color.White);
                }
            }
            else if (screen == Screen.xWin)
            {
                _spriteBatch.Draw(backbtnTexture, backbtnRect, Color.White);
                _spriteBatch.DrawString(WinFont, "X has won this round!\n\nX has won " + (xWins) + " times, \nO has won " + (oWins) + " times,\nthere has been " + (ties) + " ties.", new Vector2(0, 0), Color.Black);
            }
            else if (screen == Screen.oWin)
            {
                _spriteBatch.Draw(backbtnTexture, backbtnRect, Color.White);
                _spriteBatch.DrawString(WinFont, "O has won this round!\n\nX has won " + (xWins) + " times, \nO has won " + (oWins) + " times,\nthere has been " + (ties) + " ties.", new Vector2(0, 0), Color.Black);
            }
            else if (screen == Screen.tie)
            {
                _spriteBatch.Draw(backbtnTexture, backbtnRect, Color.White);
                _spriteBatch.DrawString(WinFont, "this round was a tie!\n\nX has won " + (xWins) + " times, \nO has won " + (oWins) + " times,\nthere has been " + (ties) + " ties.", new Vector2(0, 0), Color.Black);
            }
            else if (screen ==Screen.singleplayerxwin)
            {
                _spriteBatch.Draw(backbtnTexture, backbtnRect, Color.White);
                _spriteBatch.DrawString(WinFont, "X has won this round!\n\nX has won " + (xWins) + " times, \nO has won " + (oWins) + " times,\nthere has been " + (ties) + " ties.", new Vector2(0, 0), Color.Black);
            }
            else if (screen == Screen.singleplayerowin)
            {
                _spriteBatch.Draw(backbtnTexture, backbtnRect, Color.White);
                _spriteBatch.DrawString(WinFont, "O has won this round!\n\nX has won " + (xWins) + " times, \nO has won " + (oWins) + " times,\nthere has been " + (ties) + " ties.", new Vector2(0, 0), Color.Black);
            }
            else if (screen == Screen.singleplayertie)
            {
                _spriteBatch.Draw(backbtnTexture, backbtnRect, Color.White);
                _spriteBatch.DrawString(WinFont, "this round was a tie!\n\nX has won " + (xWins) + " times, \nO has won " + (oWins) + " times,\nthere has been " + (ties) + " ties.", new Vector2(0, 0), Color.Black);
            }
            _spriteBatch.Draw(cursorTexture, cursorRect, Color.White);

            _spriteBatch.End();
            
            base.Draw(gameTime);
        }

        public int ComputerTurn()
        {
            bool done = false;
            int turn = 0;
            while (!done) 
            {
                turn = random.Next(9);
                if (tileOwners[turn] == "")
                    done = true;
                //else if (tileOwners[0] != "" && tileOwners[1] != "" && tileOwners[2] != "" && tileOwners[3] != "" && tileOwners[4] != "" && tileOwners[5] != "" && tileOwners[6] != "" && tileOwners[7] != "" && tileOwners[8] != "")
                    //done = true;

            }

            return turn;
        }
    }
}
