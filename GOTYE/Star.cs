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
    class Star
    {
        static String[] texturenames = new[]
        {
            "star0",
            "star1"
        };
            
        static BitmapTexture2D[] textures;
        const float minrange = 1.5f;
        const float maxrange = 8f;
        const float basespeed = 16f;
        public const int MaxStarCount = 256;
        
        Sprite sprite;
        float depth;
       
        float speed
        {
            get
            {
                return basespeed / (depth + 1);
            }
        }
       
        public Star(float x, float miny, float maxy)
        {

            if (textures == null)
            {
                textures = texturenames.Select(name => new BitmapTexture2D((Bitmap)Bitmap.FromFile("..\\..\\res\\" + name + ".png"))).ToArray();
            }

            depth = Program.Rand.NextSingle() * (maxrange - minrange) + minrange;
            sprite = new Sprite(textures[Program.Rand.Next(textures.Length)], 1/(depth + 1));
            sprite.UseCentreAsOrigin = true;
            sprite.X = x;
            sprite.Y = Program.Rand.NextSingle() * (maxy - miny) + miny;
        }
       
        public void Draw(SpriteShader shader)
        {
            sprite.Render(shader);
        }
       
        public void Update()
        {
            sprite.X = sprite.X - speed;
        }

        public bool ShouldRemove(float cutoff)
        {
            return sprite.X < cutoff;
        }
    }
}
