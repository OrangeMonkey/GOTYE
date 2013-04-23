﻿using OpenTKTools;
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

        double nextpewtime;

        Vector2 velocity;
        public override Vector2 Velocity
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

        public override void Update(IEnumerable<SpaceJunk> junkage)
        {
            if (Program.MouseDevice[OpenTK.Input.MouseButton.Left])
            {
                if (Scene.CurrentTime() > nextpewtime)
                {
                    nextpewtime = Scene.CurrentTime() + 0.1;

                    Vector2 t = new Vector2
                    {
                        X = (float)Math.Cos(Sprite.Rotation + Math.PI / 2),
                        Y = (float)Math.Sin(Sprite.Rotation + Math.PI / 2)
                    };

                    Scene.AddJunk(new SpaceFlare(Sprite.Position + 25 * t, Sprite.Rotation, Colour4.Red));
                    Scene.AddJunk(new SpaceFlare(Sprite.Position - 25 * t, Sprite.Rotation, Colour4.Red));
                    //Scene.AddJunk(new SpaceFlare(Sprite.Position, Sprite.Rotation + 25, Colour4.Red));
                    //Scene.AddJunk(new SpaceFlare(Sprite.Position, Sprite.Rotation - 25, Colour4.Red));
                }

            }

            Scene.AddJunk(new Trail(Sprite.Position, Sprite.Rotation, Color.FromArgb(70, Color.RoyalBlue)));

            velocity.X = (Program.MouseDevice.X - Sprite.X) * 0.1f;
            velocity.Y = (Program.MouseDevice.Y - Sprite.Y) * 0.1f;
            Sprite.Rotation = (float)Math.Atan2(velocity.Y, Star.BaseSpeed);
            base.Update(junkage);
        }

        public override bool ShouldRemove(Rectangle bounds)
        {
            return false;
        }

    }
}
