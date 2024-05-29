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
        enum State
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
        List<Rectangle> tileRects;
        List<string> tileOwners;
        
        string playerTurn;
        private MouseState oldState;
        private MouseState newState;
        
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
            

            tileRects = new List<Rectangle>();

            tileRects.Add(new Rectangle(5, 5, 90, 90));
            tileRects.Add(new Rectangle(105, 5, 90, 90));
            tileRects.Add(new Rectangle(205, 5, 90, 90));
            tileRects.Add(new Rectangle(5, 105, 90, 90));
            tileRects.Add(new Rectangle(105, 105, 90, 90));
            tileRects.Add(new Rectangle(205, 105, 90, 90));
            tileRects.Add(new Rectangle(5, 205, 90, 90));
            tileRects.Add(new Rectangle(105, 205, 90, 90));
            tileRects.Add(new Rectangle(205, 205, 90, 90));
           
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
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mouseState = Mouse.GetState();
           
           
            if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                for (int i = 0; i < tileRects.Count; i++ )
                    if (tileRects[i].Contains(mouseState.Position))
                    {
                        if (tileOwners[i] == "")
                        {
                            if (playerTurn == "X")
                            {
                                tileOwners[i] = "X";
                                playerTurn = "O";
                            }
                            else if (playerTurn == "O") 
                            {
                                tileOwners[i] = "O";
                                playerTurn = "X";
                            }
                        }
                    }
                    
                
                
                //Check if someone won
            }

            

            oldState = newState;
           
            // TODO: Add your update logic here
           
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            
            _spriteBatch.Draw(boardTexture, boardRect, Color.White);
            
            for (int i = 0; i < tileRects.Count; i++)
            {
                if (tileOwners[i] == "X")
                    _spriteBatch.Draw(xTexture, tileRects[i], Color.White);
                else if (tileOwners[i] == "O")
                    _spriteBatch.Draw(oTexture, tileRects[i], Color.White);
                else
                    _spriteBatch.Draw(, tileRects[i], Color.White);
            }
               
            

            
           
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
