using System.Text;

namespace Deorcify.Utils;

internal static class Registry
{
    public static string? GetCurrentUserRegistryValue(string key, string entry)
    {
        if (NativeMethods.RegOpenKeyEx(0x80000001u, key, 0, 0x20019, out var keyHandle) != 0)
            return null;

        var size = 1024u;
        var buffer = new StringBuilder((int)size);
        if (NativeMethods.RegQueryValueEx(keyHandle, entry, 0, out _, buffer, ref size) != 0)
            return null;

        return buffer.ToString();
    }
}
