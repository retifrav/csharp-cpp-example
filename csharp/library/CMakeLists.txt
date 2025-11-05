add_library(some SHARED) # has to be `SHARED`, since this is for .NET

target_sources(some
    PRIVATE
        src/Some.cs
)

# that does not work, because generator expression gets URL-encoded, and even if it is manually corrected,
# it won't get evaluated in the generated VS project file (as it only gets evaluated during build?)
#set_target_properties(some
#    PROPERTIES
#        VS_DOTNET_REFERENCE_thing "$<TARGET_FILE:thing>"
#)

# and this is useless, because .NET projects don't get installed properly with CMake,
# plus you'd need to account for possible Debug postfix in the binary name,
# and finally `DllImport` doesn't seem to care about the references,
# it just picks up the DLL by file name
#set_target_properties(some
#    PROPERTIES
#        VS_DOTNET_REFERENCE_thingy "${CMAKE_INSTALL_PREFIX}/bin/thingy.dll"
#)

target_link_libraries(some
    PRIVATE
        #thingy-c-api # might need to be explicitly linked too, but it seems to be fine without
        thingy-clr
)

# install the target
install(TARGETS some)
