using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace System.IO
{
    public static class StreamExtensions
    {
        public static bool IsNullOrEmpty(
            this Stream stream)
        {
            return stream == null ||
                Equals(stream, Stream.Null);
        }
    }
}
