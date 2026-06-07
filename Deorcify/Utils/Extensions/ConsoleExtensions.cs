using System;

namespace Deorcify.Utils;

internal static class ConsoleExtensions
{
    extension(Console)
    {
        public static bool IsAttached
        {
            get
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
    }
}
