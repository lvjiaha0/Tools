﻿namespace WindowsFormsApp1
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class HotKey
    {
        [DllImport("user32.dll", SetLastError=true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, Keys vk);
        [DllImport("user32.dll", SetLastError=true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [Flags]
        public enum KeyModifiers
        {
            Alt = 1,
            Ctrl = 2,
            None = 0,
            Shift = 4,
            WindowsKey = 8
        }
    }
}

