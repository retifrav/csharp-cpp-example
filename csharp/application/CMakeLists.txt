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

add_custom_command(
    TARGET applctn
    POST_BUILD
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        ${CMAKE_CURRENT_SOURCE_DIR}/etc/grils.json
        $<TARGET_FILE_DIR:applctn>
    COMMAND_EXPAND_LISTS
    COMMENT "Copying the JSON file with grils"
)

# this is pretty much useless for installing .NET applications, as they require
# quite a lot of things aside from just the runtime dependencies; so, instead of hardcoding
# the installation for all the required files one by one, you can just pack the contents
# of `${CMAKE_CURRENT_BINARY_DIR}/Release/` (when you build the Release configuration)
#
## install the target
#install(TARGETS applctn
#   RUNTIME DESTINATION ${CMAKE_INSTALL_BINDIR}
#)
## install runtime dependencies
#install(
#    FILES $<TARGET_RUNTIME_DLLS:applctn>
#    DESTINATION ${CMAKE_INSTALL_BINDIR}
#)

