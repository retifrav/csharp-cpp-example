enable_language(CSharp)

include(GNUInstallDirs) # need to be after at least some language has been enabled

# https://cmake.org/cmake/help/latest/module/CSharpUtilities.html
#include(CSharpUtilities) # none of the commands from this module are needed (at least, for now)

add_subdirectory(library)
add_subdirectory(application)
