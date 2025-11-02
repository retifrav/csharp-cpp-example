enable_language(CXX)

include(GNUInstallDirs) # need to be after at least some language has been enabled

set(REQUIRED_CPP_STANDARD 17 CACHE STRING "Required C++ standard")
if(DEFINED CMAKE_CXX_STANDARD)
    if(CMAKE_CXX_STANDARD LESS ${REQUIRED_CPP_STANDARD})
        message(FATAL_ERROR "This project requires at least C++${REQUIRED_CPP_STANDARD}")
    endif()
else()
    set(CMAKE_CXX_STANDARD ${REQUIRED_CPP_STANDARD})
endif()
set(CMAKE_CXX_STANDARD_REQUIRED YES)
if(NOT DEFINED CMAKE_CXX_EXTENSIONS)
    set(CMAKE_CXX_EXTENSIONS NO)
endif()

add_subdirectory(library)
if(CSHARP_CPP_EXAMPLE_BUILD_TOOL_CPP)
    add_subdirectory(tool)
endif()
add_subdirectory(library-c-api)
add_subdirectory(library-clr)
