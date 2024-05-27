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
            xs = new List<Rectangle>();
            if (player1 == true)
            {
                xs.Add(new Rectangle(100, 100, 100, 100));
            }
            os = new List<Rectangle>();
            if (player2 == true)
            {
                os.Add(new Rectangle(200, 200, 100, 100));
            }
            boardRect = new Rectangle(300,300, 0,0);
            player1 = true; player2 = false;
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
