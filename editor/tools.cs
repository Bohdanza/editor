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
    public class Pencil:Tool
    {
        public Pencil()
        {

        }

        public override void UseAt(Image image, int x, int y, Color color)
        {
            image.ChangeColor(x, y, color);
        }
    }

    public class Filler:Tool
    {
        public Filler()
        {

        }

        public override void UseAt(Image image, int x, int y, Microsoft.Xna.Framework.Color color)
        {
            Color ToFill = image.ColorSet[x, y];

            if(color==ToFill)
                return;

            List<Tuple<int, int>> current, next;

            current = new List<Tuple<int, int>>();
           
            current.Add(new Tuple<int, int>(x, y));

            while (current.Count > 0)
            {
                next = new List<Tuple<int, int>>();

                foreach (var ccolor in current)
                {
                    image.ChangeColor(ccolor.Item1, ccolor.Item2, color);

                    if (ccolor.Item1<image.Width-1&& image.ColorSet[ccolor.Item1 + 1, ccolor.Item2] == ToFill)
                        next.Add(new Tuple<int, int>(ccolor.Item1 + 1, ccolor.Item2));

                    if (ccolor.Item1 >0 && image.ColorSet[ccolor.Item1 - 1, ccolor.Item2] == ToFill)
                        next.Add(new Tuple<int, int>(ccolor.Item1 - 1, ccolor.Item2));

                    if (ccolor.Item2 < image.Height - 1 && image.ColorSet[ccolor.Item1, ccolor.Item2+1] == ToFill)
                        next.Add(new Tuple<int, int>(ccolor.Item1, ccolor.Item2+1));

                    if (ccolor.Item2 >0 && image.ColorSet[ccolor.Item1, ccolor.Item2-1] == ToFill)
                        next.Add(new Tuple<int, int>(ccolor.Item1, ccolor.Item2-1));
                }

                current = next;
            }

            return;
        }
    }
}