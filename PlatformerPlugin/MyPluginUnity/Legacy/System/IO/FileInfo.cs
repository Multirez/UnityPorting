using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
#if NETFX_CORE
using Windows.Storage;
using System.Threading.Tasks;
using Windows.Storage.Streams;
#endif
using System.Reflection;

namespace LegacySystem.IO
{
    public class FileInfo
    {
        #if NETFX_CORE  
            private StorageFile storFile;
        #endif
        private string path;

        public string FullName
        {
            get
            {
                return path;
            }
        }
        public bool Exists
        {
            get
            {
                return File.Exists(path);
            }
        }

        public FileInfo(string path)
        {
            this.path = path;
        }

        public string ReadAllText(){
            return File.ReadAllText(path);
        }
        public void WriteAllText(string data)
        {
            File.WriteAllText(path, data);
        }
    }
}
