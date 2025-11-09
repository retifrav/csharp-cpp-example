#ifndef THINGY_H
#define THINGY_H

#include <string>

// CMake sets that when the library is `SHARED` (either hardcoded or when `-DBUILD_SHARED_LIBS=1`)
#if defined(thingy_EXPORTS)
    #ifdef _MSC_VER // for Windows/MSVC only
        // export on compiling the DLL (when building)
        #define DLLEXPORT __declspec(dllexport)
    //#elif __GNUC__ >= 4 // for GCC
    //    #define DLLEXPORT __attribute__ ((visibility("default")))
    #endif // any other compilers require anything like that?
// that does nothing good when this header is included in other libraries within the project
//#else
//    #ifdef _MSC_VER // for Windows/MSVC only
//        // import on using the created DLL (when using in projects)
//        #define DLLEXPORT __declspec(dllimport)
//    #elif __GNUC__ >= 4 // for GCC
//        // something here for GCC?
//    #endif // any other compilers require anything like that?
#endif

#ifndef DLLEXPORT
    #define DLLEXPORT // so it doesn't fail being undefined
#endif

namespace dpndnc
{
    DLLEXPORT std::string doThingy();
    DLLEXPORT std::string whoHasTheBestBoobs(std::string jsonString, int bornIn = 0);
}

#endif // THINGY_H
