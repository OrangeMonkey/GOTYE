using OpenTKTools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace GOTYE
{
    using Colour4 = OpenTK.Graphics.Color4;
    class SpaceShip: SpaceJunk
    {
        static BitmapTexture2D texture;
        static BitmapTexture2D Texture
        {
            get
            {
                if (texture == null)
                {
                    texture = new BitmapTexture2D((Bitmap)Bitmap.FromFile("..\\..\\res\\gotyeship.png"));
                }
                return texture;
            }
        }
        
        Vector2 velocity;
        protected override Vector2 Velocity
        {
            get { return velocity; }
        }

        public override float Depth
        {
            get { return 0; }
        }

        public SpaceShip(Vector2 startpos)
            : this(startpos, Colour4.Pink)
        {

        }

        public SpaceShip(Vector2 startpos, Colour4 colour)
            : base(startpos.X, startpos.Y, Texture, 0.5f)
        {

        }

        public override void Update()
        {
            if (Program.MouseDevice[OpenTK.Input.MouseButton.Left])
            {
                Scene.AddJunk(new SpaceFlare(Sprite.Position, Sprite.Rotation, Colour4.Red));
            }

            velocity.X = (Program.MouseDevice.X - Sprite.X) * 0.1f;
            velocity.Y = (Program.MouseDevice.Y - Sprite.Y) * 0.1f;
            Sprite.Rotation = (float)Math.Atan2(velocity.Y, Star.BaseSpeed);
            base.Update();
        }

        public override bool ShouldRemove(Rectangle bounds)
        {
            return false;
        }

    }
}
