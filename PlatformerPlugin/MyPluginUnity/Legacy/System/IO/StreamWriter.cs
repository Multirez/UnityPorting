using System;
using System.IO;

namespace LegacySystem.IO
{
    public class StreamWriter : System.IO.StreamWriter
    {
        public StreamWriter(Stream stream)
            : base(stream)
        {

        }
        
        public void Close()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }
    }
}