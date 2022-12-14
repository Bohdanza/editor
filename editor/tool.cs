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
    public abstract class Tool
    {
        public virtual Texture2D Icon { get; protected set; }

        public virtual void Update(ContentManager contentManager)
        { 
        
        }

        public virtual void DrawInterface(SpriteBatch spriteBatch, int x, int y)
        {

        }

        public virtual void DrawIcon(SpriteBatch spriteBatch, int x, int y)
        {

        }

        public virtual void UseAt(Image image, int x, int y, Color color)
        {
            
        }
    }
}