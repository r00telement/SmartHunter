using System;

namespace SmartHunter.StatLog
{
    class Utils
    {
        private static DateTime refTime = new DateTime(1970, 1, 1);
        public static long GetUnixTimeStamp()
        {
            long unixTimestamp = (long)DateTime.UtcNow.Subtract(refTime).TotalSeconds;
            return unixTimestamp;
        }

        public static DateTime GetTimeFromUnixTime(long unixTimeStamp)
        {
            return refTime.AddSeconds(unixTimeStamp);
        }
    }
}
