enable_language(CSharp)

include(CSharpUtilities)

set(CMAKE_DOTNET_SDK "Microsoft.NET.Sdk")
set(CMAKE_DOTNET_TARGET_FRAMEWORK "net9.0")
#set(CMAKE_DOTNET_TARGET_FRAMEWORK_VERSION "net9.0")
#string(APPEND CMAKE_CSharp_FLAGS " /langversion:13")

add_subdirectory(library)
add_subdirectory(application)
