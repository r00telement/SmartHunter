using System;
using System.Collections.Generic;
using System.Globalization;

namespace SmartHunter.Core
{
    public class BytePattern
    {
        public string String { get; private set; }
        public byte?[] Bytes { get; private set; }
        public List<ulong> Addresses { get; private set; }

        public BytePattern(string byteString)
        {
            String = byteString;
            Bytes = BytesFromString(byteString);
            Addresses = new List<ulong>();
        }

        public static byte?[] BytesFromString(string byteString)
        {
            List<byte?> byteList = new List<byte?>();

            var singleByteStrings = byteString.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var singleByteString in singleByteStrings)
            {
                byte parsedByte = 0;
                if (byte.TryParse(singleByteString, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out parsedByte))
                {
                    byteList.Add(parsedByte);
                }
                else
                {
                    byteList.Add(null);
                }
            }

            return byteList.ToArray();
        }

        public static List<BytePattern> PatternsFromString(string patternString)
        {
            List<BytePattern> patterns = new List<BytePattern>();

            string[] byteStrings = patternString.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var byteString in byteStrings)
            {
                patterns.Add(new BytePattern(byteString));
            }

            return patterns;
        }
    }
}