using System;
using System.Runtime.InteropServices;

namespace Lbr;

public static class Some
{
#if DEBUG
    [DllImport("thingy-c-apid.dll", CallingConvention=CallingConvention.Cdecl)] // CharSet = CharSet.Unicode
#else
    [DllImport("thingy-c-api.dll", CallingConvention=CallingConvention.Cdecl)] // CharSet = CharSet.Unicode
#endif
    private static extern IntPtr do_thingy();
    public static string DoThingy()
    {
        return Marshal.PtrToStringAnsi(do_thingy()); // `Marshal.PtrToStringUTF8` is apparently no good for C
    }

#if DEBUG
    [DllImport("thingy-c-apid.dll", CallingConvention=CallingConvention.Cdecl)] // CharSet = CharSet.Unicode
#else
    [DllImport("thingy-c-api.dll", CallingConvention=CallingConvention.Cdecl)] // CharSet = CharSet.Unicode
#endif
    private static extern IntPtr who_has_the_best_boobs(IntPtr jsonStringPtr);
    public static string WhoHasTheBestBoobs(string jsonString)
    {
        //Console.WriteLine($"[DEBUG] Got this JSON string on C# side: {jsonString}");

        // ideally should also call `Marshal.FreeHGlobal(jsonStringPtr)` somewhere
        IntPtr jsonStringPtr = Marshal.StringToHGlobalAnsi(jsonString);
        return Marshal.PtrToStringUTF8(
            who_has_the_best_boobs(
                jsonStringPtr
            )
        );
    }

    public static string getSome()
    {
        return "a string from C#";
    }
}
