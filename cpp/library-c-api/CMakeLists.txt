add_library(thingy-c-api SHARED) # has to be `SHARED`, since this is for .NET (an in general for FFI)

target_sources(thingy-c-api
    PRIVATE
        src/thingy-c-api.cpp
)

target_link_libraries(thingy-c-api
    PRIVATE
        thingy
)

# you might need the DLL is expected to have the same consistent name,
# as it gets referred to / loaded under the same consistent hardcoded string value
# set_target_properties(thingy-c-api
#     PROPERTIES
#         DEBUG_POSTFIX ""
# )
