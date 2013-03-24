using OpenTKTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.Drawing;

namespace GOTYE
{
    abstract class SpaceJunk
    {
        protected Sprite Sprite;
        protected abstract Vector2 Velocity
        {
            get;
        }

        public abstract float Depth
        {
            get;
        }

        public SpaceJunk(float x, float miny, float maxy, BitmapTexture2D texture, float scale)
        {            
            Sprite = new Sprite(texture, scale);
            Sprite.UseCentreAsOrigin = true;
            Sprite.X = x;
            Sprite.Y = Program.Rand.NextSingle() * (maxy - miny) + miny;
        }

        public SpaceJunk(float x, float y, BitmapTexture2D texture, float scale)
        {
            Sprite = new Sprite(texture, scale);
            Sprite.UseCentreAsOrigin = true;
            Sprite.X = x;
            Sprite.Y = y;
        }

        public virtual void Draw(SpriteShader shader)
        {
            Sprite.Render(shader);
        }

        public virtual void Update()
        {
            Sprite.X = Sprite.X + Velocity.X;
            Sprite.Y = Sprite.Y + Velocity.Y;
        }

        public virtual bool ShouldRemove(Rectangle bounds)
        {
            if (Velocity.X >= 0)
            {
                return Sprite.X - Sprite.Width / 2 > bounds.Right;
            }
            else
            {
                return Sprite.X + Sprite.Width / 2 < bounds.Left;
            }
        }
    }
}
