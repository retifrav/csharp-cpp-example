using System;
using System.Runtime.InteropServices;

namespace Lbr;

public static class Some
{
#if DEBUG
    [DllImport("thingyd.dll", CallingConvention=CallingConvention.Cdecl)] // CharSet = CharSet.Unicode
#else
    [DllImport("thingy.dll", CallingConvention=CallingConvention.Cdecl)] // CharSet = CharSet.Unicode
#endif
    private static extern IntPtr do_thingy();

    public static string DoThingy()
    {
        return Marshal.PtrToStringUTF8(do_thingy());
    }

    public static string getSome()
    {
        return "a string from C#";
    }
}
