using System;
using System.IO;

namespace LegacySystem.IO
{
    public static class StreamHelper
    {
        public static void Close(this Stream stream)
        {
            stream.Dispose();
            GC.SuppressFinalize(stream);
        }
    }
}
