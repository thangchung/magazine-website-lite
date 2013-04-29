using System;

namespace Cik.MagazineWeb.Utilities.Extensions
{
    public static class ConvertExtensions
    {
         public static int ConvertToInteger(this string source)
         {
             return Convert.ToInt32(source);
         }
    }
}