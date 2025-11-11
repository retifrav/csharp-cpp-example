# .NET/C# and C++ example

An example of using a C++ library inside .NET/C# application.

<!-- MarkdownTOC -->

- [Building and running](#building-and-running)
    - [On Windows](#on-windows)
    - [On platforms other than Windows](#on-platforms-other-than-windows)

<!-- /MarkdownTOC -->

More information in the following [article](https://decovar.dev/blog/2025/11/11/cpp-library-in-csharp/).

## Building and running

### On Windows

Assuming Git BASH environment:

``` sh
$ cd /path/to/csharp-cpp-example/
$ mkdir build && cd $_
$ cmake -G "Visual Studio 17 2022" \
    -DCMAKE_TOOLCHAIN_FILE="$VCPKG_ROOT/scripts/buildsystems/vcpkg.cmake" \
    -DVCPKG_APPLOCAL_DEPS=0 \
    ..
$ cmake --build . --config Release
$ ./csharp/application/Release/applctn.exe -j ./csharp/application/Release/grils.json
```

### On platforms other than Windows

``` sh
$ cd /path/to/csharp-cpp-example
$ mkdir build && cd $_

$ cmake -G Ninja -DCMAKE_BUILD_TYPE=Release \
    -DCMAKE_TOOLCHAIN_FILE="$VCPKG_ROOT/scripts/buildsystems/vcpkg.cmake" \
    ..
$ cmake --build .

$ mkdir ./csharp && cd $_
$ dotnet build ../../csharp/application/applctn.csproj \
    --artifacts-path . \
    --configuration Release
$ ./bin/applctn/release/applctn -j ./bin/applctn/release/grils.json
```
