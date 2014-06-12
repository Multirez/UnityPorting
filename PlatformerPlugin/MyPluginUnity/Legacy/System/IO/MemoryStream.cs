using System;
using System.IO;


namespace LegacySystem.IO
{
    
    public class MemoryStream : System.IO.MemoryStream
    {
        public MemoryStream() : base() { }
        public MemoryStream(byte[] buffer) : base(buffer) { }
        
        public void Close()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}


