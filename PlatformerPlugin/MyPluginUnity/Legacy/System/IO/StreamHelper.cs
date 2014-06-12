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

        public static void Close(this StreamReader reader)
        {
            reader.Dispose();
            GC.SuppressFinalize(reader);
        }

        public static void Close(this StreamWriter writer)
        {
            writer.Dispose();
            GC.SuppressFinalize(writer);
        }
    }
}
