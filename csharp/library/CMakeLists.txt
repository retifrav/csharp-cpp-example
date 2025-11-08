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
        thingy-c-api # doesn't seem to be actually required, unless you are going to use `$<TARGET_RUNTIME_DLLS:some>`
        thingy-clr
)

# should be a proper installation, but standard CMake installation procedures are useless(?)
# for installing .NET projects, so one can just as well just collect everything in the final
# application build folder
if(CSHARP_CPP_EXAMPLE_COPY_RUNTIME_DEPENDENCIES_TO_APPLICATION_BINARY)
    # by some mysterious logic `thingy-clr.dll` gets copied on its own,
    # but the rest of DLLs are not, hence this crutch
    add_custom_command(
       TARGET some
       POST_BUILD
       COMMAND ${CMAKE_COMMAND} -E copy_if_different
           $<TARGET_RUNTIME_DLLS:some>
           $<TARGET_FILE_DIR:applctn>
       COMMAND_EXPAND_LISTS
       COMMENT "Copying the C# library dependencies DLLs"
    )
endif()

# install the target
install(TARGETS some
    RUNTIME DESTINATION ${CMAKE_INSTALL_BINDIR}
)
# install runtime dependencies
install(
    FILES $<TARGET_RUNTIME_DLLS:some>
    DESTINATION ${CMAKE_INSTALL_BINDIR}
)
