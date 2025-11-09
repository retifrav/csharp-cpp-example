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
    const string debugPostfix = "d";
#else
    const string debugPostfix = "";
#endif

#if OS_LINUX
    const string libraryPrefix = ""; // "lib" // looks like DllImport is smart enough to add `lib` suffix on its own
    const string libraryExtension = "so";
#elif OS_MAC
    const string libraryPrefix = ""; // "lib" // looks like DllImport is smart enough to add `lib` suffix on its own
    const string libraryExtension = "dylib";
#else
    const string libraryPrefix = "";
    const string libraryExtension = "dll";
#endif

    const string thingyDLLfileName = $"{libraryPrefix}thingy-c-api{debugPostfix}.{libraryExtension}";

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
    private static extern IntPtr who_has_the_best_boobs_c(IntPtr jsnPtr, int bornIn);
    public static string WhoHasTheBestBoobsC(string jsn, int bornIn = 0)
    {
        //Console.WriteLine($"[DEBUG] Got this JSON string on C# side: {jsn}");

        IntPtr jsnPtr = Marshal.StringToCoTaskMemUTF8(jsn);
        string marshalledString = Marshal.PtrToStringUTF8(
            who_has_the_best_boobs_c(
                jsnPtr,
                bornIn
            )
        );
        //Marshal.FreeHGlobal(jsnPtr); // no need to free the memory, because this string was created with `c_str()`?
        return marshalledString;
    }

    // --- CLI/C++ CLR wrapper

#if OS_WINDOWS
    public static string DoThingyCLR()
    {
        return ThingyWrapperCLR.DoThingy();
    }

    public static string WhoHasTheBestBoobsCLR(string jsn, int bornIn = 0)
    {
        return ThingyWrapperCLR.WhoHasTheBestBoobs(jsn, bornIn);
    }
#endif
}
