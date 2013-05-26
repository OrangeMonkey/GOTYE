using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTKTools;

namespace GOTYE
{
    interface IRenderable
    {
        void Draw(SpriteShader shader);
    }
}
