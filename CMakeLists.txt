cmake_minimum_required(VERSION 3.28)

# seems to be redundant, CMake is managing on its own just fine
#
# also, `AnyCPU` doesn't seem to be any good, so then one is apparently expected
# to hardcode these values to specific platform such as `x64`, which doesn't feel right
#
#if(NOT DEFINED CMAKE_GENERATOR_PLATFORM OR CMAKE_GENERATOR_PLATFORM STREQUAL "")
#    message(WARNING
#        "CMAKE_GENERATOR_PLATFORM isn't set, defaulting it to AnyCPU"
#    )
#    string(APPEND CMAKE_CSharp_FLAGS " /platform:AnyCPU")
#    set(CMAKE_GENERATOR_PLATFORM "AnyCPU")
#elseif(CMAKE_GENERATOR_PLATFORM STREQUAL "x64")
#    string(APPEND CMAKE_CSharp_FLAGS " /platform:x64")
#elseif(CMAKE_GENERATOR_PLATFORM STREQUAL "Win32")
#    string(APPEND CMAKE_CSharp_FLAGS " /platform:x86")
#else()
#    message(WARNING
#        "Generator platform is set to ${CMAKE_GENERATOR_PLATFORM}, "
#        "which is not supported by managed projects, defaulting to AnyCPU"
#    )
#    string(APPEND CMAKE_CSharp_FLAGS " /platform:AnyCPU")
#    set(CMAKE_GENERATOR_PLATFORM "AnyCPU")
#endif()

project(
    "csharp-cpp-example"
    DESCRIPTION "An example project about using a C++ library inside .NET/C# application"
    VERSION 1.2.3
    LANGUAGES CSharp
)

# global debug postfix for libraries (executables still need to set it)
set(CMAKE_DEBUG_POSTFIX "d" CACHE STRING "Filename postfix for libraries under DEBUG configuration")

include(CSharpUtilities)

set(CMAKE_DOTNET_SDK "Microsoft.NET.Sdk")
set(CMAKE_DOTNET_TARGET_FRAMEWORK "net9.0")
#set(CMAKE_DOTNET_TARGET_FRAMEWORK_VERSION "net9.0")
#string(APPEND CMAKE_CSharp_FLAGS " /langversion:13")

#add_subdirectory(library-csharp)
add_subdirectory(application-csharp)
