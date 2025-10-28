add_library(thingy SHARED) # has to be `SHARED`, since this is for .NET

target_sources(thingy
    PRIVATE
        src/thingy.cpp
        src/thingy-c-api.cpp
    PUBLIC
        FILE_SET public_headers
            TYPE HEADERS
            BASE_DIRS include
            FILES
                include/Thingy/thingy.h
)

target_include_directories(thingy
    PRIVATE
        # where the library itself will look for its internal headers
        ${CMAKE_CURRENT_SOURCE_DIR}/src
    PUBLIC
        # where top-level project will look for the library's public headers
        $<BUILD_INTERFACE:${CMAKE_CURRENT_SOURCE_DIR}/include>
        # where external projects will look for the library's public headers
        $<INSTALL_INTERFACE:${CMAKE_INSTALL_INCLUDEDIR}>
)

find_package(jsoncpp CONFIG REQUIRED)
target_link_libraries(thingy
    PRIVATE
        JsonCpp::JsonCpp
)

target_compile_features(thingy INTERFACE "cxx_std_${REQUIRED_CPP_STANDARD}")

# you might need the DLL is expected to have the same consistent name,
# as it gets referred to / loaded under the same consistent hardcoded string value
# set_target_properties(thingy
#     PROPERTIES
#         DEBUG_POSTFIX ""
# )

# should be a proper installation, but standard CMake installation procedures are useless
# for installing .NET projects, so one can just as well just collect everything in the final
# application build folder
add_custom_command(
    TARGET thingy
    POST_BUILD
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        $<TARGET_FILE:thingy>
        $<TARGET_FILE_DIR:applctn>
    COMMAND_EXPAND_LISTS
    COMMENT "Copying runtime dependencies (DLLs)"
)

# installation isn't really needed, as this library is meant to be just an internal project dependency
#
## install the target and create export-set
#install(TARGETS thingy
#    EXPORT "thingyTargets"
#    FILE_SET public_headers
#)
#
## generate and install export file
#install(EXPORT "thingyTargets"
#    FILE "thingyTargets.cmake"
#    DESTINATION "share/thingy"
#)
#
#include(CMakePackageConfigHelpers)
#
## generate the version file for the config file
#write_basic_package_version_file(
#    "${CMAKE_CURRENT_BINARY_DIR}/thingyConfigVersion.cmake"
#    COMPATIBILITY AnyNewerVersion
#)
## create config file
#configure_package_config_file(
#    ${CMAKE_CURRENT_SOURCE_DIR}/Config.cmake.in
#    "${CMAKE_CURRENT_BINARY_DIR}/thingyConfig.cmake"
#    INSTALL_DESTINATION "share/thingy"
#)
## install config files
#install(FILES
#    "${CMAKE_CURRENT_BINARY_DIR}/thingyConfig.cmake"
#    "${CMAKE_CURRENT_BINARY_DIR}/thingyConfigVersion.cmake"
#    DESTINATION "share/thingy"
#)
