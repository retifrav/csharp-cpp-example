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
        COMMON_LANGUAGE_RUNTIME "" # or `netcore`?
)

# the `thingy` DLL is only needed when it is built as a SHARED library,
# but actually this command should always be enabled just in case
# there are / will be other runtime dependencies
get_target_property(THINGY_LIBRARY_TYPE thingy TYPE)
if (THINGY_LIBRARY_TYPE STREQUAL "SHARED_LIBRARY")
   add_custom_command(
       TARGET thingy-clr
       POST_BUILD
       COMMAND ${CMAKE_COMMAND} -E copy_if_different
           $<TARGET_RUNTIME_DLLS:thingy-clr>
           $<TARGET_FILE_DIR:applctn>
       COMMAND_EXPAND_LISTS
       COMMENT "Copying the CLI/C++ CLR wrapper dependencies DLLs"
   )
endif()
