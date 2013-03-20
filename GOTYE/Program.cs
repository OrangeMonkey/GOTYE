using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTKTools;
using System.Drawing;

namespace GOTYE
{
    class Program : GameWindow
    {
        public static Random Rand = new Random();
        List<Star> stars;
        List<Roid> roids;
        BitmapTexture2D jamesking;
        SpriteShader shader;
        Sprite jamessprite;
        Sprite player2;
        int playerx;
        int playery;
        int playerspeed = 10;
        
        

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color4.Black);
            shader = new SpriteShader(Width, Height);
            jamesking = new BitmapTexture2D((Bitmap)Bitmap.FromFile("..\\..\\res\\gotyeship.png"));
            jamessprite = new Sprite(jamesking, 0.5f);
            player2 = new Sprite(jamesking, 0.5f);
            jamessprite.UseCentreAsOrigin = true;
            player2.UseCentreAsOrigin = true;
            jamessprite.Colour = Color.FromArgb(Rand.Next(100, 255), Rand.Next(100, 255), Rand.Next(100, 255));
            player2.Colour = Color4.Crimson;
            CursorVisible = false;   
            Keyboard.KeyDown += (sender, ke) => 
            {
                if (ke.Key == OpenTK.Input.Key.Enter && Keyboard[OpenTK.Input.Key.AltLeft])
                {
                    if (WindowState == OpenTK.WindowState.Fullscreen)
                    {
                        WindowState = OpenTK.WindowState.Normal;
                    }
                    else
                    {
                        WindowState = OpenTK.WindowState.Fullscreen;
                    }
                }

                if (ke.Key == OpenTK.Input.Key.Escape)
                {
                    Close();
                }
            };
            GenerateStarField();
            roids.Add(new Roid(Width, 0, Height));
            roids.Add(new Roid(Width, 0, Height));
            roids.Add(new Roid(Width, 0, Height));
            roids.Add(new Roid(Width, 0, Height));
        }
        private void GenerateStarField()
        {
            stars.Clear();
            for (int i = 0; i < Star.MaxStarCount; ++i)
            {
                stars.Add(new Star(Rand.Next(Width), 0, Height));
            }
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(ClientRectangle);
            shader.SetScreenSize(Width, Height);

            GenerateStarField();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Keyboard[OpenTK.Input.Key.Up])
            {
                playery = playery - playerspeed;
            }
            if (Keyboard[OpenTK.Input.Key.Down])
            {
                playery = playery + playerspeed;
            }
            if (Keyboard[OpenTK.Input.Key.Right])
            {
                playerx = playerx + playerspeed;
            }
            if (Keyboard[OpenTK.Input.Key.Left])
            {
                playerx = playerx - playerspeed;
            }

            for (int i = stars.Count - 1; i >= 0; --i )
            {
                var star = stars[i];
                star.Update();
                if (star.ShouldRemove(0))
                {
                    stars[i] = new Star(Width, 0, Height);
                }
            }

            for (int i = roids.Count - 1; i >= 0; --i)
            {
                var roid = roids[i];
                roid.Update();
                if (roid.ShouldRemove(0))
                {
                    roids[i] = new Roid(Width, 0, Height);
                }
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            shader.Begin();
            foreach (var star in stars)
            {
                star.Draw(shader);
            }
            foreach (var roid in roids)
            {
                roid.Draw(shader);
            }
            jamessprite.X = Mouse.X;
            jamessprite.Y = Mouse.Y;
            player2.X = playerx;
            player2.Y = playery;
            jamessprite.Render(shader);
            player2.Render(shader);
            shader.End();

            SwapBuffers();
        }

        Program()
        {
            WindowBorder = OpenTK.WindowBorder.Hidden;
            Width = 1280;
            Height = 720;
            Title = "James in Orange III";
            stars = new List<Star>();
            roids = new List<Roid>();
        }

        static void Main(string[] args)
        {
            var window = new Program();
            window.Run(60.0, 120);
            window.Dispose();
        }
    }
}
