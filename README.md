# Winlocker
Winlocker on C# WPF

# What is this Winlocker?
Window with a virtual keyboard that has its display style set to "on top" and performs the following tasks:
1. Blocking keyboard input events;
2. Blocking the launch of the task manager;
3. Blocking the launch of the registry editor;
4. Blocking the right mouse button;
5. Adding to autoload;
6. Hide the taskbar.

# How to unlock Windows?
To unlock the system, you must enter the correct password by pressing the virtual keys. After entering the correct password, the changes in the Windows registry are rolled back and the program is closed.

# How change password?
You have to open App.xaml.cs and there can change "Password" property

```
public partial class App : Application
{
    public const string Password = "enter_your_password";
}
```
