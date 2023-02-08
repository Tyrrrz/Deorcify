using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace FuckRussia;

#pragma warning disable CA2255
internal static partial class Initializer
{
    private static bool IsBypassed() =>
        string.Equals(
            Environment.GetEnvironmentVariable("FUCK_RUSSIA"),
            "1",
            StringComparison.OrdinalIgnoreCase
        ) ||
        // Legacy environment variable
        string.Equals(
            Environment.GetEnvironmentVariable("RUSNI"),
            "PYZDA",
            StringComparison.OrdinalIgnoreCase
        );

    private static bool IsRestricted()
    {
        var locale = CultureInfo.CurrentCulture.Name;

        if (locale.EndsWith("-ru", StringComparison.OrdinalIgnoreCase) ||
            locale.EndsWith("-by", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var region = Registry.CurrentUser
                .OpenSubKey(@"Control Panel\International\Geo", false)?
                .GetValue("Name") as string;

            if (string.Equals(region, "ru", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(region, "by", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    [ModuleInitializer]
    public static void Execute()
    {
        if (IsBypassed() || !IsRestricted())
            return;

        var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

        var message =
            $"""
            Based on your system settings, it appears you're located in Russia or Belarus. You cannot use {assemblyName} on the territory of a terrorist state.

            If you believe this to be an error, check your system settings and make sure your country and region are configured correctly.

            If you wish to bypass this check, set the environment variable `FUCK_RUSSIA=1` in your system settings.
            """;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            MessageBox(IntPtr.Zero, message, "Restricted region", 0x00000010);
            Environment.Exit(1);
        }
        else if (IsConsoleAttached())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(message);
            Console.ResetColor();
            Environment.Exit(1);
        }
        else
        {
            throw new ApplicationException(message);
        }
    }
}