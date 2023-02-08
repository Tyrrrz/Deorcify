using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace FuckRussia;

internal static partial class Initializer
{
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    private static bool IsConsoleAttached()
    {
        try
        {
            _ = Console.WindowHeight;
            return true;
        }
        catch
        {
            return false;
        }
    }
}