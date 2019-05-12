using System;

namespace SmartHunter.Core.Helpers
{
    public class PointerTraceLevel
    {
        public ulong Address { get; private set; }
        public ulong ReadResult { get; private set; }
        public long Offset { get; private set; }
        public ulong Result { get; private set; }

        public PointerTraceLevel(ulong address, ulong readResult, long offset, ulong result)
        {
            Address = address;
            ReadResult = readResult;
            Offset = offset;
            Result = result;
        }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                var other = obj as PointerTraceLevel;

                return Address == other.Address
                    && ReadResult == other.ReadResult
                    && Offset == other.Offset
                    && Result == other.Result;
            }
        }

        // Generated with Quick Action
        public override int GetHashCode()
        {
            var hashCode = 1237761341;
            hashCode = hashCode * -1521134295 + Address.GetHashCode();
            hashCode = hashCode * -1521134295 + ReadResult.GetHashCode();
            hashCode = hashCode * -1521134295 + Offset.GetHashCode();
            hashCode = hashCode * -1521134295 + Result.GetHashCode();
            return hashCode;
        }
    }
}