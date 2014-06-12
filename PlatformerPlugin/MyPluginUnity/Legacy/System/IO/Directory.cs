using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if NETFX_CORE
using System.Threading.Tasks;
using Windows.Storage;
using Windows.ApplicationModel;
using System.IO;
#endif

namespace LegacySystem.IO
{
    public class Directory
    {

        public static string[] GetFiles(string path)
        {
#if NETFX_CORE
            var t = GetFilesAsync(path.Replace('/', '\\'));
            t.Wait();
            return t.Result;
#else
            throw new NotImplementedException();
#endif
        }

        public static string[] GetDirectories(string path)
        {
#if NETFX_CORE
            var t = GetFoldersAsync(path.Replace('/', '\\'));
            t.Wait();
            return t.Result;
#else
            throw new NotImplementedException();
#endif
        }

        public static bool Exists(string path)
        {
#if NETFX_CORE
            var t = ExistsAsync(path.Replace('/', '\\'));
            t.Wait();
            if (t.IsCompleted)
                return t.Result;
            else
                return false;
#else
            throw new NotImplementedException();
#endif
        }

        public static bool CreateDirectory(string path)
        {
#if NETFX_CORE
            var t = CreateDirectoryAsync(path);
            t.Wait();
            if (t.IsCompleted)
                return t.Result;
            else
            {
                return false;
            }
#else
            throw new NotImplementedException();
#endif
        }
        
        public static bool Delete(string path, bool isRecursive)
        {
            //default recursive delete supports only
            return Delete(path);
        }
        public static bool Delete(string path)
        {
#if NETFX_CORE
            var t = DeleteDirectoryAsync(path);
            t.Wait();
            if (t.IsCompleted)
                return t.Result;
            else
                return false;
#else
            throw new NotImplementedException();
#endif
        }

        public static string GetCurrentDirectory()
        {
#if NETFX_CORE
            return Package.Current.InstalledLocation.Path;
#else
            throw new NotImplementedException();
#endif
        }

#if NETFX_CORE

        /// <summary>
        /// Creates a folder using an absolute path
        /// </summary>
        private static async Task<bool> CreateDirectoryAsync(string folderName)
        {
            //fix folder path
            folderName = File.FixPath(folderName);
            try
            {
                if (folderName[folderName.Length - 1] == '\\')//remove slash in the end if exist
                    folderName = folderName.Substring(0, folderName.Length-1);

                int lastSlashIndex=folderName.LastIndexOf('\\');
                string parentPath = folderName.Substring(0, lastSlashIndex);
                StorageFolder parentFolder = await StorageFolder.GetFolderFromPathAsync(parentPath);
                await parentFolder.CreateFolderAsync(folderName.Substring(lastSlashIndex+1), CreationCollisionOption.ReplaceExisting);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Path", folderName);
                throw new Exception("Error! Can't create directory: "+folderName, ex);
            } 
            return true;   
        }
        private static async Task<bool> DeleteDirectoryAsync(string folderName)
        {

            try
            {
                var folder = await StorageFolder.GetFolderFromPathAsync(folderName);
                await folder.DeleteAsync(StorageDeleteOption.PermanentDelete);
                return true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Path", folderName);
                throw new Exception("Error! Can't delete directory: "+folderName, ex);
            }
        }


        private static async Task<bool> ExistsAsync(string path)
        {
            try
            {
                var folder = await StorageFolder.GetFolderFromPathAsync(path);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static async Task<string[]> GetFilesAsync(string path)
        {
            try
            {
                var folder = await StorageFolder.GetFolderFromPathAsync(path);
                var files = await folder.GetFilesAsync();
                var result = new string[files.Count];

                for (int i = 0; i < files.Count; i++)
                    result[i] = Path.Combine(path, files[i].Name);
                return result;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Path", path);
                throw new Exception("Error! Can't return files: "+path, ex);
            }
        }

        private static async Task<string[]> GetFoldersAsync(string path)
        {
            try
            {
                var folder = await StorageFolder.GetFolderFromPathAsync(path);
                var folders = await folder.GetFoldersAsync();
                var result = new string[folders.Count];

                for (int i = 0; i < folders.Count; i++)
                    result[i] = Path.Combine(path, folders[i].Name);
                return result;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Path", path);
                throw new Exception("Error! Can't return directories: "+path, ex);
            }
        }

#endif

    }
}