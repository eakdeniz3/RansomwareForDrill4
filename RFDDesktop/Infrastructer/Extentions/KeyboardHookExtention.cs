using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RFDDesktop.Infrastructer.Extentions
{

    public class KeyboardHook
    {
        public delegate int DCallback();

        //public keyboardHookProc _keyboardHookProc;

        //public KeyboardHook()
        //{
        //    Initialize(Hook);
        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();
        //    Callback();
        //}
        private static keyboardHookProc callbackDelegate;
        public delegate int keyboardHookProc(int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam);
        [DllImport("user32.dll", EntryPoint = "SetWindowsHookExA", CharSet = CharSet.Ansi)]
        private static extern int SetWindowsHookEx(int idHook, keyboardHookProc lpfn, IntPtr hinstance, int dwThreadId);
        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("user32.dll")]
        private static extern int UnhookWindowsHookEx(int hHook);

        [DllImport("user32.dll", EntryPoint = "CallNextHookEx", CharSet = CharSet.Ansi)]
        private static extern int CallNextHookEx(int hHook, int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam);

        const int WH_KEYBOARD_LL = 13;
        private static int intLLKey;
        public KBDLLHOOKSTRUCT lParam;

        public struct KBDLLHOOKSTRUCT
        {
            public int vkCode;
            int scanCode;
            public int flags;
            int time;
            int dwExtraInfo;
        }

        private static int LowLevelKeyboardProc(int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam)
        {
            bool blnEat = false;
            switch (wParam)
            {
                case 256:
                case 257:
                case 260:
                case 261:
                    //Alt+Tab, Alt+Esc, Ctrl+Esc, Windows Key
                    if (((lParam.vkCode == 9) && (lParam.flags == 32)) ||
                        ((lParam.vkCode == 27) && (lParam.flags == 32)) ||
                        ((lParam.vkCode == 27) && (lParam.flags == 0)) ||
                        ((lParam.vkCode == 91) && (lParam.flags == 1)) ||
                         ((lParam.vkCode == 91) && (lParam.flags == 33)) ||
                          ((lParam.vkCode == 27) && (lParam.flags == 0)) ||
                        ((lParam.vkCode == 92) && (lParam.flags == 1)) ||
                        ((lParam.flags == 32)) || ((lParam.vkCode == 170))
                        )
                    {
                        blnEat = true;
                    }
                    break;
            }

            if (blnEat)
                return 1;
            else return CallNextHookEx(0, nCode, wParam, ref lParam);

        }

        public int Hook()
        {
            if (callbackDelegate != null) throw new InvalidOperationException("Can't hook more than once");
            IntPtr instance = LoadLibrary("User32");
            callbackDelegate = new keyboardHookProc(LowLevelKeyboardProc);
            intLLKey = SetWindowsHookEx(WH_KEYBOARD_LL,callbackDelegate, instance, 0);

            return intLLKey;
        }
        public void ReleaseKeyboardHook(int intLLKey)
        {
            if (callbackDelegate == null) return;
            UnhookWindowsHookEx(intLLKey);
        }


        [DllImport("Library", CallingConvention = CallingConvention.StdCall)]
        public static extern void Initialize(DCallback pfDelegate);

        [DllImport("Library", CallingConvention = CallingConvention.StdCall)]
        public static extern void Callback();

        ~KeyboardHook()
        {
            Console.Error.WriteLine("Entry Collected");
        }

    }
}
