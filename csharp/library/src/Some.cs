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
    public static extern IntPtr do_thingy();

    public static string DoThingy()
    {
        return Marshal.PtrToStringAnsi(do_thingy());
    }

    public static string getSome()
    {
        return "here's some string from C#";
    }
}
