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
using System.Drawing;
using System.Drawing.Imaging;

namespace editor
{
    public class Image
    {
        public Microsoft.Xna.Framework.Color[,] ColorSet { get; private set; }

        public int Scale { get; protected set; }
        
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        
        public int DelayX { get; set; }
        public int DelayY { get; set; }

        private int TimeSinceLastSave=0;

        public Image(ContentManager contentManager, int width, int height, int x, int y)
        {
            Scale = 1;

            Width = width;
            Height = height;

            DelayX = x;
            DelayY = y;

            ColorSet = new Microsoft.Xna.Framework.Color[Width, Height];

            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                {
                    ColorSet[i, j] = new Microsoft.Xna.Framework.Color(255, 255, 255, 255);
                }
        }

        public void Update(ContentManager contentManager)
        {
            var ms = Mouse.GetState();

            int dif = Game1.MousePrev.ScrollWheelValue - ms.ScrollWheelValue;

            Scale -= dif / 50;

            TimeSinceLastSave++;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int w = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int h = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            int pidraw = 0;

            for (int i = 0; i < Width&&pidraw <= w; i++)
            {
                int pjdraw = 0;

                for (int j = 0; j < Height&&pjdraw<=h; j++)
                {
                    spriteBatch.Draw(Game1.pixelTexture, new Vector2(i*Scale + DelayX, j*Scale + DelayY), null,
                        ColorSet[i, j], 0f, new Vector2(0, 0), Scale, SpriteEffects.None, 0f);

                    pjdraw = j * Scale + DelayY;
                }

                pidraw = i * Scale + DelayX;
            }
        }

        public Vector2 GetScreenPos(int x, int y)
        {
            return new Vector2((float)(x-DelayX)/Scale, (float)(y - DelayY) / Scale);
        }

        public void ChangeColor(int x, int y, Microsoft.Xna.Framework.Color color)
        {
            if(x>=0&&y>=0&&x<Width&&y<Height)
                ColorSet[x, y] = color;
        }

        public void PlaceRectangle(int x, int y, int width, int height)
        {
            for(int i=x; i<x+width; i++)
            {


            }
        }

        public Bitmap GetBitmap()
        {
            Bitmap bmp = new Bitmap(Width, Height);

            for(int i=0; i<Width; i++)
                for (int j = 0; j < Height; j++)
                {
                    bmp.SetPixel(i, j, System.Drawing.Color.FromArgb(ColorSet[i, j].R, ColorSet[i, j].G, ColorSet[i, j].B));
                }

            return bmp;
        }

        public void Save(string path)
        {
            if (TimeSinceLastSave < 10)
                return;

            TimeSinceLastSave = 0;

            Bitmap myBitmap;
            ImageCodecInfo myImageCodecInfo;
            Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;

            myBitmap = GetBitmap();

            myImageCodecInfo = GetEncoderInfo("image/png");

            myEncoder = Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);

            myEncoderParameter = new EncoderParameter(myEncoder, 75L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            myBitmap.Save(path+".png", myImageCodecInfo, myEncoderParameters);
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
    }
}