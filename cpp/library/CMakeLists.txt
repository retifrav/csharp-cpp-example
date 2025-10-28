add_library(thingy SHARED) # has to be `SHARED`, since this is for .NET (and also `thingy_c_api`)

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

# ---

add_library(thingy_c_api SHARED) # has to be `SHARED`, since this is for .NET

target_sources(thingy_c_api
    PRIVATE
        src/thingy-c-api.cpp
)

target_link_libraries(thingy_c_api
    PRIVATE
        thingy
)

# you might need the DLL is expected to have the same consistent name,
# as it gets referred to / loaded under the same consistent hardcoded string value
# set_target_properties(thingy_c_api
#     PROPERTIES
#         DEBUG_POSTFIX ""
# )

# should be a proper installation, but standard CMake installation procedures are useless
# for installing .NET projects, so one can just as well just collect everything in the final
# application build folder
add_custom_command(
    TARGET thingy_c_api
    POST_BUILD
    # copy the library itself (thingy_c_api[d].dll)
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        $<TARGET_FILE:thingy_c_api>
        $<TARGET_FILE_DIR:applctn>
    # copy its dependencies (thingy[d].dll)
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        $<TARGET_RUNTIME_DLLS:thingy_c_api>
        $<TARGET_FILE_DIR:applctn>
    COMMAND_EXPAND_LISTS
    COMMENT "Copying runtime dependencies (DLLs)"
)
