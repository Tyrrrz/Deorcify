using System.Runtime.InteropServices;
using System.Text;

namespace Deorcify.Utils;

internal static class NativeMethods
{
    [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
    public static extern int RegOpenKeyEx(
        nuint hKey,
        string subKey,
        int ulOptions,
        int samDesired,
        out nuint hkResult
    );

    [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
    public static extern int RegQueryValueEx(
        nuint hKey,
        string lpValueName,
        int lpReserved,
        out uint lpType,
        StringBuilder lpData,
        ref uint lpcbData
    );

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int MessageBox(nint hWnd, string text, string caption, uint type);
}
