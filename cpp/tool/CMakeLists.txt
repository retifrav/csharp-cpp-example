add_executable(thingy-tool)

target_sources(thingy-tool
    PRIVATE
        src/main.cpp
)

set_target_properties(thingy-tool
    PROPERTIES
        DEBUG_POSTFIX ${CMAKE_DEBUG_POSTFIX}
)

target_link_libraries(thingy-tool
    PRIVATE
        thingy
)

install(TARGETS thingy-tool)

# this is actually somewhat redundant, because if the library itself is going to be installed too,
# then its DLL will already be in the `bin` folder, but then `copy_if_different` should guard from
# overwriting it, since it will be the same
#
#if(NOT EXISTS "${CMAKE_INSTALL_PREFIX}/${CMAKE_INSTALL_BINDIR}") # it doesn't exist before the very first installation yet
#   file(MAKE_DIRECTORY "${CMAKE_INSTALL_PREFIX}/${CMAKE_INSTALL_BINDIR}") # but at least this one will execute before add_custom_command()
#endif()
#
#
#add_custom_command(
#   TARGET thingy-tool
#   POST_BUILD
#   COMMAND ${CMAKE_COMMAND} -E copy_if_different
#       $<TARGET_RUNTIME_DLLS:thingy-tool> # locate DLLs (might discover some redundant ones too)
#       ${CMAKE_INSTALL_PREFIX}/${CMAKE_INSTALL_BINDIR} # where to copy them
#   COMMAND_EXPAND_LISTS
#   COMMENT "Copying the tool runtime dependencies (DLLs)"
#)
