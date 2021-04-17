using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yungching_T1.Extention
{
    public static class StringExtention
    {
        public static int ConvertToInt32(this string str)
        {
            return int.Parse(str);
        }
    }

    public static class DateTimeExtention
    {
        public static DateTime ConvertToTWDateTime(this DateTime dateTime)
        {
            return dateTime.AddYears(-1911);
        }

        public static DateTime ConvertAnnoDominaiToTWDateTime(this DateTime dateTime)
        {
            return dateTime.AddYears(1911);
        }
    }
}
