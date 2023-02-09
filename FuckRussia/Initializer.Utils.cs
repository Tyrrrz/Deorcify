using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FuckRussia;

file static class NativeMethods
{
    [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
    public static extern int RegOpenKeyEx(
        UIntPtr hKey,
        string subKey,
        int ulOptions,
        int samDesired,
        out UIntPtr hkResult
    );

    [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
    public static extern int RegQueryValueEx(
        UIntPtr hKey,
        string lpValueName,
        int lpReserved,
        out uint lpType,
        StringBuilder lpData,
        ref uint lpcbData
    );

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int MessageBox(
        IntPtr hWnd,
        string text,
        string caption,
        uint type
    );
}

internal static partial class Initializer
{
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

    private static void ShowErrorMessageBox(string title, string message) =>
        NativeMethods.MessageBox(IntPtr.Zero, message, title, 0x00000010);

    private static string? GetCurrentUserRegistryValue(string key, string entry)
    {
        if (NativeMethods.RegOpenKeyEx(new UIntPtr(0x80000001u), key, 0, 0x20019, out var keyHandle) != 0)
            return null;

        var size = 1024u;
        var buffer = new StringBuilder((int)size);
        if (NativeMethods.RegQueryValueEx(keyHandle, entry, 0, out _, buffer, ref size) != 0)
            return null;

        return buffer.ToString();
    }
}