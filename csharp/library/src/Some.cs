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
    const string thingyDLLfileName = "thingy-c-apid.dll";
#else
    const string thingyDLLfileName = "thingy-c-api.dll";
#endif

    [DllImport(thingyDLLfileName)] // CallingConvention=CallingConvention.Cdecl, CharSet = CharSet.Unicode
    private static extern IntPtr do_thingy_c();
    public static string DoThingyC()
    {
        IntPtr thng = do_thingy_c();
        string marshalledString = Marshal.PtrToStringUTF8(thng);
        //Marshal.FreeHGlobal(thng); // no need to free the memory, because this string was created with `c_str()`?
        return marshalledString;
    }

    [DllImport(thingyDLLfileName)] // CallingConvention=CallingConvention.Cdecl, CharSet = CharSet.Unicode
    private static extern IntPtr who_has_the_best_boobs_c(IntPtr jsnPtr);
    public static string WhoHasTheBestBoobsC(string jsn)
    {
        //Console.WriteLine($"[DEBUG] Got this JSON string on C# side: {jsn}");

        IntPtr jsnPtr = Marshal.StringToCoTaskMemUTF8(jsn);
        string marshalledString = Marshal.PtrToStringUTF8(
            who_has_the_best_boobs_c(
                jsnPtr
            )
        );
        //Marshal.FreeHGlobal(jsnPtr); // no need to free the memory, because this string was created with `c_str()`?
        return marshalledString;
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
