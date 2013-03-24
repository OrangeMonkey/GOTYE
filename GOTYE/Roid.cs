﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTKTools;
using OpenTK.Graphics;
using System.Drawing;

namespace GOTYE
{
    class Roid: SpaceJunk
    {
        static String[] texturenames = new[]
        {
            "roid1",
            "roid2",
            "roid3"
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

        float rotspeed;

        protected override Vector2 Velocity
        {
            get
            {
                return new Vector2
                {
                    X = -16,
                    Y = 0
                };
            }
        }
        
        public Roid(float x, float miny, float maxy)
            : base(x, miny, maxy, Textures[Program.Rand.Next(Textures.Length)], Program.Rand.NextSingle() * 1.75f + 0.25f) 
        {
            Sprite.X = Sprite.X + Sprite.Width;
            rotspeed = Program.Rand.NextSingle() * MathHelper.Pi / 10 - MathHelper.Pi / 20;
            Sprite.Colour = Color4.SlateGray;
        }

        public override void Update()
        {
            base.Update();
            Sprite.Rotation = Sprite.Rotation + rotspeed;
        }
    }
}
