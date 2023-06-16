using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace ChessRules
{
    enum Color
    {
        none,
        White,
        Black
    }
    static class ColorMethods
    {
        public static Color FlipColor (this Color color)
        {
            if (color == Color.Black) return Color.White;
            if (color == Color.White) return Color.Black;
            return Color.none;
        }
    }
}
