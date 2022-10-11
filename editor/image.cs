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
        private Color[,] ColorSet;
        public Color[,] GetColorSet() { return ColorSet; }

        public int Scale { get; protected set; }
        
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        
        public int DelayX { get; set; }
        public int DelayY { get; set; }

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
            var ms = Mouse.GetState();

            int dif = Game1.MousePrev.ScrollWheelValue - ms.ScrollWheelValue;

            Scale -= dif / 50;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    spriteBatch.Draw(Game1.pixelTexture, new Vector2(i*Scale + DelayX, j*Scale + DelayY), null,
                        ColorSet[i, j], 0f, new Vector2(0, 0), Scale, SpriteEffects.None, 0f);
                }
            }
        }

        public Vector2 GetScreenPos(int x, int y)
        {
            return new Vector2((float)(x-DelayX)/Scale, (float)(y - DelayY) / Scale);
        }

        public void ChangeColor(int x, int y, Color color)
        {
            if(x>=0&&y>=0&&x<Width&&y<Height)
                ColorSet[x, y] = color;
        }
    }
}