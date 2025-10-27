add_library(some SHARED) # has to be `SHARED`, since this is for .NET

target_sources(some
    PRIVATE
        src/Some.cs
)
