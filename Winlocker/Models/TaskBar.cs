using System.Runtime.InteropServices;

namespace Winlocker.Models
{
    public static class TaskBar
    {
        [DllImport("user32.dll")]
        private static extern int FindWindow(string className, string windowText);

        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int command);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;

        private static int Handle => FindWindow("Shell_TrayWnd", "");
        private static int StartHandle => FindWindow("Button", "Пуск");

        public static void Lock()
        {
            ShowWindow(Handle, SW_HIDE);
            ShowWindow(StartHandle, SW_HIDE);
        }
        public static void Unlock()
        {
            ShowWindow(Handle, SW_SHOW);
            ShowWindow(StartHandle, SW_SHOW);
        }
    }
}
