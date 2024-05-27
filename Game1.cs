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
        List<Rectangle> xs; 
        Texture2D oTexture;
        List<Rectangle> os;
        bool player1;
        bool player2;

        string playerTurn;
        
        bool square1;
        bool square2;
        bool square3;
        bool square4;
        bool square5;
        bool square6;
        bool square7;
        bool square8;
        bool square9;

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
            player1 = true; player2 = false;
            boardRect = new Rectangle(0, 0, 300, 300);
            playerTurn = "X";
            



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
                // Loop through rectangle list and determine which was clicked on
                //was clicked rectanglew not owned, if not assign owner and switch turns
            }

            if (playerTurn == "X" && newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released && mouseState.X >= 300 && mouseState.Y >= 300)
            {
                player2 = true;
                player1 = false;
            }
            if (player2 == true && newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released && mouseState.X >= 300 && mouseState.Y >= 300)
            {
                player2 = false;
                player1 = true;
            }

            oldState = newState;
            xs = new List<Rectangle>();
            
            xs.Add(new Rectangle(105, 105, 90, 90)); // perfect spawn + size
            
            os = new List<Rectangle>();

            os.Add(new Rectangle(205, 205, 90, 90)); // perfect spawn + size
            // TODO: Add your update logic here
           
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            
            _spriteBatch.Draw(boardTexture, boardRect, Color.White);
            
            foreach (Rectangle x in xs)
                _spriteBatch.Draw(xTexture, x, Color.White);

            foreach (Rectangle o in os)
                _spriteBatch.Draw(oTexture, o, Color.White);
           
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
