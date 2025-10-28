add_executable(applctn)

target_sources(applctn
    PRIVATE
        src/Program.cs
)

target_link_libraries(applctn
    PRIVATE
        some
)

set_target_properties(applctn
    PROPERTIES
        VS_PACKAGE_REFERENCES "System.CommandLine_2.0.0-rc.1.25451.107"
        DEBUG_POSTFIX "${CMAKE_DEBUG_POSTFIX}"
)

# this is pretty much useless for installing .NET applications, as they require quite a lot of things,
# all of which are already available in the `CMAKE_CURRENT_BINARY_DIR/{Debug,Release}/`
#
#if(NOT EXISTS ${CMAKE_INSTALL_PREFIX}/${CMAKE_INSTALL_BINDIR}) # it doesn't exist before the very first installation yet
#    file(MAKE_DIRECTORY ${CMAKE_INSTALL_PREFIX}/${CMAKE_INSTALL_BINDIR}) # but at least this one will execute before add_custom_command()
#endif()
#install(TARGETS applctn
#    #ARCHIVE DESTINATION ${CMAKE_INSTALL_LIBDIR}
#    #LIBRARY DESTINATION ${CMAKE_INSTALL_LIBDIR}
#    RUNTIME DESTINATION ${CMAKE_INSTALL_BINDIR}
#)
## will not discover even its own `applctn(d).dll`
#add_custom_command(
#    TARGET applctn
#    POST_BUILD
#    COMMAND ${CMAKE_COMMAND} -E copy_if_different
#        $<TARGET_RUNTIME_DLLS:applctn> # locate DLLs (might discover some redundant ones too)
#        ${CMAKE_INSTALL_PREFIX}/${CMAKE_INSTALL_BINDIR} # where to copy them
#    COMMAND_EXPAND_LISTS
#    COMMENT "Copying runtime dependencies (DLLs)"
#)
