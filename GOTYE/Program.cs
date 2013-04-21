using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using OpenTKTools;
using System.Drawing;
using System.Diagnostics;

namespace GOTYE
{
    class Program : GameWindow
    {
        public static Random Rand = new Random();
        List<SpaceJunk> junkage;
        SpriteShader shader;
        int framecount;
        Stopwatch timer;
        Stopwatch starttimer;
        double stagestarttime;
        Stage currentstage;
        int stagenumber;

        public static MouseDevice MouseDevice
        {
            get;
            private set;

        }

        public static KeyboardDevice KeyboardDevice
        {
            get;
            private set;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color4.Black);
            shader = new SpriteShader(Width, Height);
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
            stagestarttime = CurrentTime();
            stagenumber = 0;
            currentstage = Stage.GenerateStage(stagenumber);
            AddJunk(new SpaceShip(new Vector2(Width / 4, Height / 2), Color4.Peru));
        }

        public double CurrentTime()
        {
            return starttimer.Elapsed.TotalSeconds;
        }

        public double StageTime()
        {
            return CurrentTime() - stagestarttime;
        }

        public void AddJunk(SpaceJunk junk)
        {
            junk.Scene = this;
            for (int i = junkage.Count - 1; i >= 0; --i)
            {
                if (junkage[i].Depth > junk.Depth)
                {
                    junkage.Insert(i + 1, junk);
                    return;
                }
            }
            junkage.Insert(0, junk);
        }

        private void GenerateStarField()
        {
            junkage = junkage.Where(x => !(x is Star)).ToList();
            for (int i = 0; i < Star.MaxStarCount; ++i)
            {
                AddJunk(new Star(Rand.Next(Width), 0, Height));
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

            if (timer.Elapsed.TotalSeconds > 1)
            {
                Title = "FPS:" + framecount + (framecount < 60 ? " D:" : " :D");
                timer.Restart();
                framecount = 0;
            }

            MouseDevice = Mouse;
            KeyboardDevice = Keyboard;

            var tospawn = currentstage.ChooseNextObstacle(StageTime(), Width, 0, Height);

            if (tospawn != null)
            {
                AddJunk(tospawn);
            }

            else if (currentstage.HasEnded())
            {
                stagenumber = stagenumber + 1;
                currentstage = Stage.GenerateStage(stagenumber);
                stagestarttime = CurrentTime() + 2;
            }

            junkage.ToList().ForEach(junk =>
            {
                junk.Update(junkage);
                if (junk.ShouldRemove(ClientRectangle))
                {
                    junkage.Remove(junk);
                    if (junk is Star)
                    {
                        AddJunk(new Star(Width, 0, Height));
                    }                 
                }
            });
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            shader.Begin();
            foreach (var junk in junkage)
            {
                junk.Draw(shader);
            }

            shader.End();

            SwapBuffers();

            framecount = framecount + 1;
        }

        Program()
            : base (1280, 720)
        {
            WindowBorder = OpenTK.WindowBorder.Fixed;
            Title = "";
            VSync = VSyncMode.On;
            junkage = new List<SpaceJunk>();
            framecount = 0;
            timer = new Stopwatch();
            timer.Start();
            starttimer = new Stopwatch();
            starttimer.Start();
        }

        static void Main(string[] args)
        {
            var window = new Program();
            window.Run(60.0, 120);
            window.Dispose();
        }
    }
}
