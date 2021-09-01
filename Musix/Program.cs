using Musix.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Musix
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           
            // Check that FFMPEG is available
            if(!FileExists("ffmpeg.exe") || !FileExists("ffprobe.exe"))
            {
                var downloader = new FFMPEGDownloader();
                Application.Run(downloader);
            }
            else
            {
                Debug.WriteLine("FFMPEG Found.");
                Application.Run(new MainWindow());
            }
        }

        public static bool FileExists(string fileName)
        {
            if (File.Exists(fileName)) return true;
            var pathBuilder = new StringBuilder(fileName, 260);
            return PathFindOnPath(pathBuilder, null);
        }

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, SetLastError = false)]
        private static extern bool PathFindOnPath([In, Out] StringBuilder pszFile, [In] string[] ppszOtherDirs);
    }
}