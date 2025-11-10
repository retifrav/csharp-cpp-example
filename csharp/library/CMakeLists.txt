add_library(some SHARED) # has to be `SHARED`, since this is for .NET

target_sources(some
    PRIVATE
        src/Some.cs
)

# that does not work, because generator expressions are not supported in `VS_DOTNET_REFERENCE_*` property
#set_target_properties(some
#    PROPERTIES
#        VS_DOTNET_REFERENCE_thingycapi "$<TARGET_FILE:thingy-c-api>"
#)
#
# and this is useless, because .NET projects don't get installed properly with CMake,
# plus you'd need to account for possible Debug postfix in the binary name,
# and finally `DllImport` doesn't seem to care about the references at all,
# it just picks up the DLL by file name
#set_target_properties(some
#    PROPERTIES
#        VS_DOTNET_REFERENCE_thingycapi "${CMAKE_INSTALL_PREFIX}/bin/thingy-c-api.dll"
#)

if(CMAKE_SYSTEM_NAME STREQUAL "Windows")
    target_compile_definitions(some
        PUBLIC
            OS_WINDOWS
    )
elseif(CMAKE_SYSTEM_NAME STREQUAL "Linux")
    target_compile_definitions(some
        PUBLIC
            OS_LINUX
    )
elseif(CMAKE_SYSTEM_NAME STREQUAL "Darwin")
    target_compile_definitions(some
        PUBLIC
            OS_MAC
    )
else()
    # although it might actually work on other platforms
    message(FATAL_ERROR "Unknown/unsupported platform")
endif()

target_link_libraries(some
    PRIVATE
        thingy-c-api # doesn't seem to be actually required, unless you are going to use `$<TARGET_RUNTIME_DLLS:some>`
        thingy-clr
)

# install the target
install(TARGETS some
    RUNTIME DESTINATION ${CMAKE_INSTALL_BINDIR}
)
# install runtime dependencies
install(
    FILES $<TARGET_RUNTIME_DLLS:some>
    DESTINATION ${CMAKE_INSTALL_BINDIR}
)
