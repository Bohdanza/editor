using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace editor
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        public static Texture2D pixelTexture;
        public static MouseState MousePrev;

        private Palette palette;
        private List<Image> images;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Window.AllowUserResizing = true;

            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            MousePrev = Mouse.GetState();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            pixelTexture = Content.Load<Texture2D>("pixel");
            palette = new Palette(Content, 10, 10);

            images = new List<Image>();
            images.Add(new Image(Content, 512, 512, 448, 28));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            palette.Update(Content);
            images[0].Update(Content);

            var ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Up))
                images[0].DelayY -= 5;

            if (ks.IsKeyDown(Keys.Down))
                images[0].DelayY += 5;

            if (ks.IsKeyDown(Keys.Right))
                images[0].DelayX += 5;

            if (ks.IsKeyDown(Keys.Left))
                images[0].DelayX -= 5;

            if (MousePrev.LeftButton==ButtonState.Pressed)
            {
                Vector2 vct = images[0].GetScreenPos(MousePrev.X, MousePrev.Y);

                images[0].ChangeColor((int)vct.X, (int)vct.Y, new Color(palette.R, palette.G, palette.B, palette.A));
            }

            MousePrev = Mouse.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(100, 100, 100));

            _spriteBatch.Begin();

            images[0].Draw(_spriteBatch);

            palette.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
