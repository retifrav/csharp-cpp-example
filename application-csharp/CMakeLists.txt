add_executable(applctn)

set_target_properties(applctn
    PROPERTIES
        VS_PACKAGE_REFERENCES "System.CommandLine_2.0.0-rc.1.25451.107"
        DEBUG_POSTFIX "${CMAKE_DEBUG_POSTFIX}"
)

target_sources(applctn
    PRIVATE
        src/Program.cs
)

target_link_libraries(applctn
    PRIVATE
        some
)
