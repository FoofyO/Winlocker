using Microsoft.Win32;

namespace Winlocker.Models
{
    public class Attempts
    {
        private int _count;
        public int count
        {
            get => _count;
            set
            {
                if (_count != value)
                {
                    _count = value;
                    OnAttemptChange();
                }
            }
        }

        public Attempts()
        {
            RegistryKey reg;
            string sub = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Steam";
            reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(sub);

            if (reg.GetValue("Count") != null) count = int.Parse(reg.GetValue("Count").ToString());
            else count = 3;
            reg.Close();
        }

        public void OnAttemptChange()
        {
            RegistryKey reg;
            string sub = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Steam";
            reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(sub);
            reg.SetValue("Count", count);
            reg.Close();
        }
    }
}
