using System;
using System.IO;
using Microsoft.Win32;
using System.Reflection;
using IWshRuntimeLibrary;

namespace Winlocker.Models
{
    public class Autorun
    {
        public static void Set()
        {
            string ShortCutPath = "C:\\Windows\\Steam.lnk"; //Shortcut name
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(ShortCutPath);
            shortcut.Description = "Steam desktop client"; //Shortcut description

            var image = Properties.Resources.Steam;
            using (FileStream f = new FileStream("C:\\Windows\\Steam.ico", FileMode.OpenOrCreate))
            {
                image.Save(f);
            }

            shortcut.TargetPath = Assembly.GetExecutingAssembly().Location; //Target path
            shortcut.WorkingDirectory = Environment.CurrentDirectory;
            shortcut.IconLocation = "C:\\Windows\\Steam.ico";
            shortcut.Save();

            RegistryKey reg;
            string sub = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\\";
            reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(sub, true);
            reg.SetValue("Steam", ShortCutPath);
            reg.Close();
        }

        public static void Unset()
        {
            if (System.IO.File.Exists("C:\\Windows\\Steam.lnk")) System.IO.File.Delete("C:\\Windows\\Steam.lnk");
            if (System.IO.File.Exists("C:\\Windows\\Steam.ico")) System.IO.File.Delete("C:\\Windows\\Steam.ico");

            RegistryKey reg;
            string sub = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\\";
            reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(sub, true);
            reg.DeleteValue("Steam");
            reg.Close();
        }
    }
}
