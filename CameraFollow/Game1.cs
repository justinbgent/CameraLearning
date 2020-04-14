using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CameraFollow
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D player;
        Vector2 playerPosition;
        float speed = 300f;

        Texture2D box;
        Vector2 boxPosition;

        Camera2d camera;
        Viewport viewport;

        KeyboardState keyState;
        Keys[] previousKeys;
        MouseState mouseState;
        Vector2 previousMousePos;
        int previousScroll;

        Texture2D grass;
        GameGrid grid;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            mouseState = Mouse.GetState();
            previousScroll = mouseState.ScrollWheelValue;
            keyState = Keyboard.GetState();
            previousKeys = keyState.GetPressedKeys();
            previousMousePos = new Vector2(mouseState.X, mouseState.Y);
            IsMouseVisible = true;

            camera = new Camera2d(this.viewport, 5120, 2880, 1f);
            Content.RootDirectory = "Content";
            IsFixedTimeStep = false;

            grid = new GameGrid(5120, 2880, 480);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            playerPosition = new Vector2(graphics.PreferredBackBufferWidth / 4, graphics.PreferredBackBufferHeight / 2);
            boxPosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = Content.Load<Texture2D>("circle");
            box = Content.Load<Texture2D>("box");
            grass = Content.Load<Texture2D>("grass");
            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            #region Player Movement

            if (keyState.IsKeyDown(Keys.W))
            {
                playerPosition.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                playerPosition.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                playerPosition.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                playerPosition.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            #endregion

            #region Camera Movement

            // Adjust zoom if the mouse wheel has moved
            if (mouseState.ScrollWheelValue > previousScroll)
            {
                camera.Zoom += .05f; //zoomIncrement;
            }
            else if (mouseState.ScrollWheelValue < previousScroll)
            {
                camera.Zoom -= .05f;
            }

            previousScroll = mouseState.ScrollWheelValue;

            // Move the camera when the arrow keys are pressed
            Vector2 movement = Vector2.Zero;
            Viewport vp = GraphicsDevice.Viewport;

            if (keyState.IsKeyDown(Keys.Up))
            {
                movement.Y--;
            }
            if (keyState.IsKeyDown(Keys.Down))
            {
                movement.Y++;
            }
            if (keyState.IsKeyDown(Keys.Left))
            {
                movement.X--;
            }
            if (keyState.IsKeyDown(Keys.Right))
            {
                movement.X++;
            }

            //camera.Pos += movement * 20;

            camera.Pos = new Vector2(playerPosition.X - 640 + player.Width/2, playerPosition.Y - 360 + player.Height/2);

            // Transform mouse input from view to world position
            Matrix inverse = Matrix.Invert(camera.GetTransformation());
            Vector2 mousePos = Vector2.Transform(new Vector2(mouseState.X, mouseState.Y), inverse);

            float top = boxPosition.Y;
            float bottom = boxPosition.Y + box.Height;
            float left = boxPosition.X;
            float right = boxPosition.X + box.Width;
            if (mousePos.X > left && mousePos.X < right && mousePos.Y > top && mousePos.Y < bottom)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    boxPosition.X -= (previousMousePos.X - mousePos.X);
                    boxPosition.Y -= (previousMousePos.Y - mousePos.Y);
                }
            }

            previousMousePos = mousePos;

            #endregion

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, camera.GetTransformation());

            // Note to self, about .1f at the end of draw there, the higher the float the more depth into the background
            // 1f is max. 0f means it is on the layer closest to us
            foreach (Vector2 position in grid.Grid)
            {
                spriteBatch.Draw(grass, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, .1f);
            }
        
            spriteBatch.Draw(player, playerPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            
            spriteBatch.Draw(box, new Vector2(800, 800), Color.White);

            spriteBatch.Draw(box, boxPosition, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
