using System;
using System.IO;

namespace LegacySystem.IO
{
    public class StreamReader : System.IO.StreamReader
    {
        public StreamReader(Stream stream) 
            :base(stream)
        { 
        }

        public void Close()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }

    }
}