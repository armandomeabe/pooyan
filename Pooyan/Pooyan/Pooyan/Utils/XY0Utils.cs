using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pooyan.Utils
{
    public static class XY0Utils
    {
        public static bool EstaRelativamenteCerca(Vector2 A, Vector2 B, int tolerancia)
        {
            return
                (
                    Math.Abs(A.X - B.X) <= tolerancia
                    &&
                    Math.Abs(A.Y - B.Y) <= tolerancia
                );
        }
    }
}
