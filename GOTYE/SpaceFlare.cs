﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTKTools;
using System.Drawing;

namespace GOTYE
{
    using Colour4 = OpenTK.Graphics.Color4;
    class SpaceFlare : SpaceJunk
    {
        const float pewspeed = Star.BaseSpeed;
        static BitmapTexture2D texture;
        static BitmapTexture2D Texture
        {
            get
            {
                if (texture == null)
                {
                    texture = new BitmapTexture2D((Bitmap)Bitmap.FromFile("..\\..\\res\\pew1.png"));
                }
                return texture;
            }
        }
        Vector2 velocity;

        bool hashit;


        protected override Vector2 Velocity
        {
            get { return velocity; }
        }

        public override float Depth
        {
            get { return 0.5f; }
        }

        public SpaceFlare(Vector2 startpos, float angle, Colour4 colour)
            : base(startpos.X, startpos.Y, Texture, 0.75f)
        {
            Sprite.Colour = colour;
            Sprite.Rotation = angle;
            velocity = new Vector2
            {
                X = (float)Math.Cos(angle) * pewspeed,
                Y = (float)Math.Sin(angle) * pewspeed
            };
            hashit = false;
        }

        public override bool ShouldRemove(Rectangle bounds)
        {
            return hashit || base.ShouldRemove(bounds);
        }

        public override void Update(IEnumerable<SpaceJunk> junkage)
        {
            base.Update(junkage);
            foreach (SpaceJunk junk in junkage)
            {
                if (junk is Roid && junk.IsHit(Sprite.Position))
                {
                    ((Damagable)junk).Damage(25, velocity / 16);
                    hashit = true;
                    return;
                }
            }
        }
    }
}
