#ifndef THINGY_C_API_H
#define THINGY_C_API_H

#ifdef __cplusplus
extern "C"
{
#endif

#ifdef thingy_EXPORTS // CMake sets that when BUILD_SHARED_LIBS=1 (or if library is hardcoded to SHARED)
    #ifdef _MSC_VER // for MSVC
        // export on compiling the DLL (when building)
        #define DLLEXPORT __declspec(dllexport)
    //#elif __GNUC__ >= 4 // for GCC
    //    #define DLLEXPORT __attribute__ ((visibility("default")))
    #endif // any other compilers require anything like that?
#else
    #ifdef _MSC_VER // for MSVC
        // import on using the created DLL (when using in projects)
        #define DLLEXPORT __declspec(dllimport)
    //#elif __GNUC__ >= 4 // for GCC
    //    // something here for GCC?
    #endif // any other compilers require anything like that?
#endif

#ifndef DLLEXPORT
    #define DLLEXPORT // so it doesn't fail being undefined
#endif

// returns a pointer to a string, caller must not free it
DLLEXPORT const char *do_thingy();

#ifdef __cplusplus
}
#endif

#endif // THINGY_C_API_H
