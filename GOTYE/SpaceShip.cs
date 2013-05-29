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
    class SpaceShip: Damagable
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
        double immuneend;
        public int Score = 0;
        float rotspeed;

        public bool IsImmune
        {
            get { return immuneend > Scene.CurrentTime(); }
        }

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
            HP = 100;
        }

        public override void Update(IEnumerable<SpaceJunk> junkage)
        {
            if (!IsAlive)
            {
                Sprite.Rotation += rotspeed;
                velocity.X -= 0.01f;
                velocity.Y *= 0.9f;
            }
            else
            {
                if (Program.MouseDevice[OpenTK.Input.MouseButton.Left] && !IsImmune)
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

                if (!IsImmune)
                {
                    foreach (SpaceJunk junk in junkage)
                    {
                        if (junk is Roid && junk.IsTouching(this))
                        {
                            if (Sprite.Height < junk.Size.Y)
                            {
                                Damage((int)(junk.Scale * 10), junk.Position, junk.Velocity * junk.Size.X * junk.Size.Y);
                                ((Roid)junk).Splode(true);
                            }
                            else
                            {
                                Score = Score + (int)(junk.Scale * 10);
                                junk.Remove();
                            }
                            break;
                        }
                    }
                }


                velocity.X = (Program.MouseDevice.X - Sprite.X) * 0.1f;
                velocity.Y = (Program.MouseDevice.Y - Sprite.Y) * 0.1f;
                Sprite.Rotation = (float)Math.Atan2(velocity.Y, Star.BaseSpeed);
            }
            base.Update(junkage);
        }

        public override void Draw(SpriteShader shader)
        {
            int alpha = IsImmune ? (int)(Pulse(0.25) * 191) : 255;

            Sprite.Colour = Color.FromArgb(alpha, (Color)Sprite.Colour);
            
            base.Draw(shader);
        }

        protected override void OnDamaged(int amount, Vector2 hitpos, Vector2 force)
        {
            base.OnDamaged(amount, hitpos, force);
            immuneend = Scene.CurrentTime() + 2;
        }

        protected override void OnKilled()
        {
            rotspeed = Program.Rand.NextSingle() * 0.5f - 0.25f;
            base.OnKilled();
        }

        public override bool ShouldRemove(Rectangle bounds)
        {
            return false;
        }

    }
}
