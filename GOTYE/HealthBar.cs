using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTKTools;
using OpenTK;
using System.Drawing;

namespace GOTYE
{
    class HealthBar : IRenderable
    {
        private SpaceShip ship;
        public Vector2 Position
        {
            get;
            set;
        }
        private Sprite sprite;
        const int fullbarcount = 10;
        static BitmapTexture2D texture;
        static BitmapTexture2D Texture
        {
            get
            {
                if (texture == null)
                {
                    texture = new BitmapTexture2D((Bitmap)(Bitmap.FromFile("..\\..\\res\\hp0.png")));
                }
                return texture;
            }
        }

        public HealthBar(Vector2 position, SpaceShip ship)
        {
            Position = position;
            this.ship = ship;
            sprite = new Sprite(Texture, 0.75f);
        }

        public void Draw(SpriteShader shader)
        {
            for (int i = 0; i < fullbarcount; ++i)
            {
                int starthp = i * 100 / fullbarcount;
                int endhp = (i + 1) * 100 / fullbarcount;
                if (ship.HP <= starthp)
                {
                    continue;
                }
                sprite.Colour = Color.FromArgb(Math.Min(255,(ship.HP-starthp)*255/(endhp-starthp)), Color.Firebrick);
                sprite.Position = Position + new Vector2(i * sprite.Width, 0);
                sprite.Render(shader);
            }
        }
    }
}
