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
    public class Image
    {
        public Color[,] ColorSet { get; protected set; }
        public int Scale { get; protected set; }
        
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        
        public int DelayX { get; protected set; }
        public int DelayY { get; protected set; }

        public Image(ContentManager contentManager, int width, int height, int x, int y)
        {
            Scale = 1;

            Width = width;
            Height = height;

            DelayX = x;
            DelayY = y;

            ColorSet = new Color[Width, Height];

            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                {
                    ColorSet[i, j] = new Color(255, 255, 255, 255);
                }
        }

        public void Update(ContentManager contentManager)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    spriteBatch.Draw(Game1.pixelTexture, new Vector2(i + DelayX, j + DelayY), ColorSet[i, j]);
                }
            }
        }
    }
}