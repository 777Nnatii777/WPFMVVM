using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Input;
using KeyListener.Interfaces;

namespace KeyListener.Services
{
    public class KeyboardListenerService : IKeyBoardListener
    {
        private Dictionary<string, int> _keyStats = new();
        private IntPtr _hookID = IntPtr.Zero;
        private LowLevelKeyboardProc _proc;

        public KeyboardListenerService()
        {
            _proc = HookCallback;
        }

        public void Start()
        {
            _keyStats.Clear();
            _hookID = SetHook(_proc);
              
        }

        public void Stop()
        {
            UnhookWindowsHookEx(_hookID);
        }

        public IReadOnlyDictionary<string, int> GetStats() => _keyStats;


        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using Process curProcess = Process.GetCurrentProcess();
            using ProcessModule curModule = curProcess.MainModule!;
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                GetModuleHandle(curModule.ModuleName), 0);
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
            {
                int vkCode = Marshal.ReadInt32(lParam);
                string keyName = ((Key)KeyInterop.KeyFromVirtualKey(vkCode)).ToString();

                if (_keyStats.ContainsKey(keyName))
                    _keyStats[keyName]++;
                else
                    _keyStats[keyName] = 1;
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }


        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk,
            int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
