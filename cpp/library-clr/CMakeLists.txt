add_library(thingy-clr SHARED) # has to be `SHARED`, since this is for .NET

target_sources(thingy-clr
    PRIVATE
        src/thingy-clr.cpp
)

target_link_libraries(thingy-clr
    PRIVATE
        thingy
)

set_target_properties(thingy-clr
    PROPERTIES
        # https://cmake.org/cmake/help/latest/prop_tgt/COMMON_LANGUAGE_RUNTIME.html
        # https://learn.microsoft.com/en-us/cpp/build/reference/clr-common-language-runtime-compilation
        COMMON_LANGUAGE_RUNTIME "netcore" # empty string means "classic" .NET Framework
)
