cmake_minimum_required(VERSION 3.22)

project(
    "applctn"
    DESCRIPTION "Just some application"
    VERSION 1.2.3
    LANGUAGES CSharp
)

# global debug postfix for libraries (executables still need to set it)
set(CMAKE_DEBUG_POSTFIX "d" CACHE STRING "Filename postfix for libraries under DEBUG configuration")

include(CSharpUtilities)

set(CMAKE_DOTNET_SDK "Microsoft.NET.Sdk")
set(CMAKE_DOTNET_TARGET_FRAMEWORK "net9.0")
#set(CMAKE_DOTNET_TARGET_FRAMEWORK_VERSION "net9.0")
#set(CMAKE_CSharp_FLAGS "/langversion:13")

if(CMAKE_GENERATOR_PLATFORM STREQUAL "x64")
    string(APPEND CMAKE_CSharp_FLAGS " /platform:x64")
elseif(CMAKE_GENERATOR_PLATFORM STREQUAL "Win32")
    string(APPEND CMAKE_CSharp_FLAGS " /platform:x86")
else()
    message(WARNING
        "Generator platform is set to ${CMAKE_GENERATOR_PLATFORM}, "
        "which is not supported by managed projects, defaulting to AnyCPU"
    )
    string(APPEND CMAKE_CSharp_FLAGS " /platform:AnyCPU")
endif()

add_executable(${PROJECT_NAME})

set_target_properties(${PROJECT_NAME}
    PROPERTIES
        VS_PACKAGE_REFERENCES "System.CommandLine_2.0.0-rc.1.25451.107"
        DEBUG_POSTFIX "${CMAKE_DEBUG_POSTFIX}"
)

target_sources(${PROJECT_NAME}
    PRIVATE
        src/Program.cs
)
