using System;
using System.Runtime.InteropServices;

namespace Lbr;

public static class Some
{
#if DEBUG
    [DllImport("thingy_c_apid.dll", CallingConvention=CallingConvention.Cdecl)] // CharSet = CharSet.Unicode
#else
    [DllImport("thingy_c_api.dll", CallingConvention=CallingConvention.Cdecl)] // CharSet = CharSet.Unicode
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
