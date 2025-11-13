add_library(thingy)

target_sources(thingy
    PRIVATE
        src/thingy.cpp
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

# doesn't seem to help with anything, still need to use `.StringToCoTaskMemUTF8()` and `.PtrToStringUTF8()`
# and convert to `std::wstring` with `codecvt`
#target_compile_definitions(thingy
#   PUBLIC
#       "UNICODE"
#       "_UNICODE" # should be for C runtime, but probably won't hurt anyway
#)

target_compile_features(thingy INTERFACE "cxx_std_${REQUIRED_CPP_STANDARD}")

# installation isn't really needed, as this library is meant to be just an internal project dependency
if(CSHARP_CPP_EXAMPLE_INSTALL_THINGY_LIBRARY)
    # install the target and create export-set
    install(TARGETS thingy
       EXPORT "thingyTargets"
       FILE_SET public_headers
    )

    # generate and install export file
    install(EXPORT "thingyTargets"
       FILE "thingyTargets.cmake"
       NAMESPACE "${PROJECT_NAME}::"
       DESTINATION "share/thingy"
    )

    include(CMakePackageConfigHelpers)

    # generate the version file for the config file
    write_basic_package_version_file(
       "${CMAKE_CURRENT_BINARY_DIR}/thingyConfigVersion.cmake"
       COMPATIBILITY AnyNewerVersion
    )
    # create config file
    configure_package_config_file(
       "${CMAKE_CURRENT_SOURCE_DIR}/Config.cmake.in"
       "${CMAKE_CURRENT_BINARY_DIR}/thingyConfig.cmake"
       INSTALL_DESTINATION "share/thingy"
    )
    # install config files
    install(FILES
       "${CMAKE_CURRENT_BINARY_DIR}/thingyConfig.cmake"
       "${CMAKE_CURRENT_BINARY_DIR}/thingyConfigVersion.cmake"
       DESTINATION "share/thingy"
    )
endif()
