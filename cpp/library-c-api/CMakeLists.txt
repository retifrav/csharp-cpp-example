add_library(thingy-c-api SHARED) # has to be `SHARED`, since this is for .NET (an in general for FFI)

target_sources(thingy-c-api
    PRIVATE
        src/thingy-c-api.cpp
)

target_link_libraries(thingy-c-api
    PRIVATE
        thingy
)

# you might need the DLL is expected to have the same consistent name,
# as it gets referred to / loaded under the same consistent hardcoded string value
# set_target_properties(thingy-c-api
#     PROPERTIES
#         DEBUG_POSTFIX ""
# )

# should be a proper installation, but standard CMake installation procedures are useless
# for installing .NET projects, so one can just as well just collect everything in the final
# application build folder
add_custom_command(
    TARGET thingy-c-api
    POST_BUILD
    # copy the library itself (thingy-c-api[d].dll)
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        $<TARGET_FILE:thingy-c-api>
        $<TARGET_FILE_DIR:applctn>
    COMMAND_EXPAND_LISTS
    COMMENT "Copying the C API wrapper DLL"
)
# the `thingy` DLL is only needed when it is built as a SHARED library,
# but actually this command should always be enabled just in case
# there are / will be other runtime dependencies
get_target_property(THINGY_LIBRARY_TYPE thingy TYPE)
if (THINGY_LIBRARY_TYPE STREQUAL "SHARED_LIBRARY")
   add_custom_command(
       TARGET thingy-c-api
       POST_BUILD
       COMMAND ${CMAKE_COMMAND} -E copy_if_different
           $<TARGET_RUNTIME_DLLS:thingy-c-api>
           $<TARGET_FILE_DIR:applctn>
       COMMAND_EXPAND_LISTS
       COMMENT "Copying the C API wrapper dependencies DLLs"
   )
endif()
