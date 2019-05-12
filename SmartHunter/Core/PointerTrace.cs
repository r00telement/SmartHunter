using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartHunter.Core.Helpers
{
    public class PointerTrace
    {
        public List<PointerTraceLevel> Levels { get; private set; }

        public PointerTrace()
        {
            Levels = new List<PointerTraceLevel>();
        }

        public override string ToString()
        {
            string result = "";

            if (!Levels.Any())
            {
                return result;
            }

            // Apply padding to the output so it looks neat and tidy
            var largestAddressLength = Levels.Select(traceLevel => $"{traceLevel.Address:X}".Length).OrderByDescending(length => length).First();
            var largestReadResultLength = Levels.Select(traceLevel => $"{traceLevel.ReadResult:X}".Length).OrderByDescending(length => length).First();
            var largestOffsetLength = Levels.Select(traceLevel => $"{traceLevel.Offset:X}".Length).OrderByDescending(length => length).First();
            var largestResultLength = Levels.Select(traceLevel => $"{traceLevel.Result:X}".Length).OrderByDescending(length => length).First();

            foreach (var traceLevel in Levels)
            {
                string addressString = $"{traceLevel.Address:X}".PadLeft(largestAddressLength);
                string readResultString = $"{traceLevel.ReadResult:X}".PadLeft(largestReadResultLength);
                string offsetString = $"{traceLevel.Offset:X}".PadLeft(largestOffsetLength);
                string resultString = $"{traceLevel.Result:X}".PadLeft(largestResultLength);

                result += $"{addressString} -> {readResultString} + {offsetString} = {resultString}";

                if (traceLevel != Levels.Last())
                {
                    result += "\r\n";
                }
            }

            return result;
        }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                var other = obj as PointerTrace;

                if (Levels.Count != other.Levels.Count)
                {
                    return false;
                }

                for (int index = 0; index < Levels.Count; ++index)
                {
                    if (!Levels[index].Equals(other.Levels[index]))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        // Generated with Quick Action
        public override int GetHashCode()
        {
            return -653240011 + EqualityComparer<List<PointerTraceLevel>>.Default.GetHashCode(Levels);
        }
    }
}