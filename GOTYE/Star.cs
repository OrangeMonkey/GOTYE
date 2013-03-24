using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTKTools;
using System.Drawing;

namespace GOTYE
{
    class Star: SpaceJunk
    {
        static String[] texturenames = new[]
        {
            "star0",
            "star1"

        };
            
        static BitmapTexture2D[] textures;
        static BitmapTexture2D[] Textures
        {
            get
            {
                if (textures == null)
                {
                    textures = texturenames.Select(name => new BitmapTexture2D((Bitmap)Bitmap.FromFile("..\\..\\res\\" + name + ".png"))).ToArray();
                }
                return textures;
            }
        }
        const float minrange = 1.5f;
        const float maxrange = 64f;
        public const float BaseSpeed = 32f;
        const int colourmin = 200;
        const int colourmax = 256;
        public const int MaxStarCount = 5000;
        
        float depth;

        protected override Vector2 Velocity
        {
            get
            {
                return new Vector2
                {
                    X = - BaseSpeed / (depth + 1),
                    Y = 0
                };
            }
        }
       
        public Star(float x, float miny, float maxy)
            : base(x, miny, maxy, Textures[Program.Rand.Next(Textures.Length)], 1/(Program.Rand.NextSingle() * (maxrange - minrange) + minrange + 1)) 
        {
            depth = 1/Sprite.Scale.X - 1;
            Sprite.Colour = Color.FromArgb(Program.Rand.Next(colourmin, colourmax), Program.Rand.Next(colourmin, colourmax), Program.Rand.Next(colourmin, colourmax));
        }       
    }
}
