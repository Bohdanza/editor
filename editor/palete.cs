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
    public class Palette
    {
        public byte R { get; protected set; }
        public byte G { get; protected set; }
        public byte B { get; protected set; }
        public byte A { get; protected set; }

        public int X { get; protected set; }
        public int Y { get; protected set; }

        private Texture2D background;

        private Slider sliderR, sliderG, sliderB, sliderA;

        public Palette(ContentManager contentManager, int x, int y)
        {
            X = x;
            Y = y;

            background = contentManager.Load<Texture2D>("palete_back");

            R = 255;
            G = 255;
            B = 255;
            A = 0;

            Texture2D slider = contentManager.Load<Texture2D>("slider");

            sliderR = new Slider(X + 3, Y + 196, 255, 20, 255, 0, slider);
            sliderR.LockY = true;

            sliderG = new Slider(X + 3, Y + 226, 255, 20, 255, 0, slider);
            sliderG.LockY = true;

            sliderB = new Slider(X + 3, Y + 256, 255, 20, 255, 0, slider);
            sliderB.LockY = true;

            sliderA = new Slider(X + 3, Y + 168, 255, 20, 0, 0, slider);
            sliderA.LockY = true;
        }

        public void Update(ContentManager contentManager)
        {
            sliderR.Update();
            sliderG.Update();
            sliderB.Update();
            sliderA.Update();

            R = (byte)sliderR.SliderX;
            G = (byte)sliderG.SliderX;
            B = (byte)sliderB.SliderX;
            A = (byte)sliderA.SliderX;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Vector2(X, Y), Color.White);

            spriteBatch.Draw(Game1.pixelTexture, new Vector2(X + 14, Y + 14), null,
                new Color(R, G, B)*((float)(255-A)/255), 0f, new Vector2(0, 0),
                111f, SpriteEffects.None, 0f);

            sliderA.Draw(spriteBatch);
            sliderR.Draw(spriteBatch);
            sliderG.Draw(spriteBatch);
            sliderB.Draw(spriteBatch);
        }

        public void ChangeCoords(int x, int y)
        {
            X = x;
            Y = y;

            sliderA.X = X + 3;
            sliderA.Y = Y + 168;

            sliderR.X = X + 3;
            sliderR.Y = Y + 196;

            sliderR.X = X + 3;
            sliderR.Y = Y + 226;

            sliderR.X = X + 3;
            sliderR.Y = Y + 256;
        }
    }
}