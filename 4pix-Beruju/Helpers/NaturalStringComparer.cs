using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4pix_Beruju.Helpers
{
 

    public class NaturalStringComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            int ix = 0, iy = 0;

            while (ix < x.Length && iy < y.Length)
            {
                if (char.IsDigit(x[ix]) && char.IsDigit(y[iy]))
                {
                    long numX = 0;
                    while (ix < x.Length && char.IsDigit(x[ix]))
                    {
                        numX = numX * 10 + (x[ix] - '0');
                        ix++;
                    }

                    long numY = 0;
                    while (iy < y.Length && char.IsDigit(y[iy]))
                    {
                        numY = numY * 10 + (y[iy] - '0');
                        iy++;
                    }

                    int numberCompare = numX.CompareTo(numY);
                    if (numberCompare != 0)
                        return numberCompare;
                }
                else
                {
                    char cx = char.ToUpperInvariant(x[ix]);
                    char cy = char.ToUpperInvariant(y[iy]);

                    int charCompare = cx.CompareTo(cy);
                    if (charCompare != 0)
                        return charCompare;

                    ix++;
                    iy++;
                }
            }

            return x.Length.CompareTo(y.Length);
        }
    }
}