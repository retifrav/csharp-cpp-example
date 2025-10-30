#ifndef THINGY_H
#define THINGY_H

#include <string>

// CMake sets that when BUILD_SHARED_LIBS=1 (or if library is hardcoded to SHARED)
#if defined(thingy_EXPORTS)
//#if defined(thingy_EXPORTS) || defined(thingy_c_api_EXPORTS)
    // export on compiling the DLL (when building)
    #define DLLEXPORT __declspec(dllexport)
#else
    // import on using the created DLL (when using in projects)
    #define DLLEXPORT __declspec(dllimport)
#endif

#ifndef DLLEXPORT
    #define DLLEXPORT // so it doesn't fail being undefined
#endif

namespace dpndnc
{
    DLLEXPORT std::string doThingy();
    DLLEXPORT std::string whoHasTheBestBoobs(std::string jsonString);
}

#endif // THINGY_H
