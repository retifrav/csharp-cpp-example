using System;
using System.Runtime.InteropServices;

namespace Lbr;

public static class Some
{
    public static string getSome()
    {
        return "a string from C#";
    }

    // --- P/Invoke with C API wrapper

#if DEBUG
    [DllImport("thingy-c-apid.dll", CallingConvention=CallingConvention.Cdecl)] // CharSet = CharSet.Unicode
#else
    [DllImport("thingy-c-api.dll", CallingConvention=CallingConvention.Cdecl)] // CharSet = CharSet.Unicode
#endif
    private static extern IntPtr do_thingy_c();
    public static string DoThingyC()
    {
        return Marshal.PtrToStringAnsi(do_thingy_c()); // `Marshal.PtrToStringUTF8` is apparently no good for C
    }

#if DEBUG
    [DllImport("thingy-c-apid.dll", CallingConvention=CallingConvention.Cdecl)] // CharSet = CharSet.Unicode
#else
    [DllImport("thingy-c-api.dll", CallingConvention=CallingConvention.Cdecl)] // CharSet = CharSet.Unicode
#endif
    private static extern IntPtr who_has_the_best_boobs_c(IntPtr jsnPtr);
    public static string WhoHasTheBestBoobsC(string jsn)
    {
        //Console.WriteLine($"[DEBUG] Got this JSON string on C# side: {jsn}");

        // ideally should also call `Marshal.FreeHGlobal(jsnPtr)` somewhere
        IntPtr jsnPtr = Marshal.StringToHGlobalAnsi(jsn);
        return Marshal.PtrToStringUTF8(
            who_has_the_best_boobs_c(
                jsnPtr
            )
        );
    }

    // --- CLI/C++ CLR wrapper

    public static string DoThingyCLR()
    {
        return ThingyWrapperCLR.DoThingy();
    }

    public static string WhoHasTheBestBoobsCLR(string jsn)
    {
        return ThingyWrapperCLR.WhoHasTheBestBoobs(jsn);
    }
}
