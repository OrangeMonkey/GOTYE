using OpenTKTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOTYE
{
    static class Tools
    {
        public static float FindTextWidth(this Text text)
        {
            return text.Font.CharWidth * text.Scale.X * text.String.Length;
        }
    }
}
