using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Core.Util
{
    public class DateTimeUtil
    {
        public static int convertDateTimeInt(DateTime time)
        {
            int intResult = 0;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = int.Parse((time - startTime).TotalSeconds.ToString().Substring(0, 10));
            return intResult;
        }

        public static DateTime convertIntDatetime(int utc)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            startTime = startTime.AddSeconds(utc);
            return startTime;
        }

        public static DateTime getMonthFirstDay()
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            DateTime firstDayOfThisMonth = new DateTime(year, month, 1);//年、月、日
            return firstDayOfThisMonth;
        }
    }
}
