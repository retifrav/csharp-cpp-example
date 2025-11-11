# .NET/C# and C++ example

An example of using a C++ library inside .NET/C# application.

<!-- MarkdownTOC -->

- [Building and running](#building-and-running)
    - [On Windows](#on-windows)
    - [On platforms other than Windows](#on-platforms-other-than-windows)
        - [Desktop](#desktop)
        - [MAUI](#maui)

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

#### Desktop

Works equally fine on Mac OS and GNU/Linux:

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

#### MAUI

Targetting iOS simulator:

``` sh
$ cd /path/to/csharp-cpp-example
$ mkdir build && cd $_

$ cmake -G Xcode \
    -DCMAKE_TOOLCHAIN_FILE="$VCPKG_ROOT/scripts/buildsystems/vcpkg.cmake" \
    -DVCPKG_TARGET_TRIPLET="arm64-ios-simulator" \
    -DCMAKE_SYSTEM_NAME="iOS" \
    -DCMAKE_OSX_SYSROOT="iphonesimulator" \
    -DCMAKE_OSX_ARCHITECTURES="arm64" \
    ..
$ cmake --build . --config Debug

$ mkdir ./maui && cd $_
$ MD_APPLE_SDK_ROOT='/Users/vasya/Applications/Xcode-26.0.0.app' \
    dotnet build ../../csharp/maui/maui.csproj \
    --artifacts-path . \
    --configuration Debug \
    -f net9.0-ios \
    /p:ApplicationTargetPlatform=ios-simulator
```

here:

- `MD_APPLE_SDK_ROOT` is only needed if .NET/MAUI didn't like your "main" Xcode, so you needed to install a different version;
- the resulting application bundle will be at `./bin/maui/debug_net9.0-ios/maui.app`;
- instead of `Debug` you might of course prefer building `Release`, but that build might never finish or/and exhaust all your disk space.
