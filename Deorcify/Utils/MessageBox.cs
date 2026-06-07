namespace Deorcify.Utils;

internal static class MessageBox
{
    public static void ShowError(string title, string message) =>
        NativeMethods.MessageBox(0, message, title, 0x00000010);
}
