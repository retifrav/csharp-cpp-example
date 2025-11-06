#include <iostream>
//#include <windows.h>

#include <Thingy/thingy.h>

int main(int argc, char *argv[])
{
    // this will set the console encoding/codepage for the entire current
    // console session, so it will persist even if you recompile this execitable
    // without setting the codepage
    // ...let alone that this is for Windows only
    //SetConsoleOutputCP(CP_UTF8);

    std::cout << "Base application message" << std::endl;
    std::cout << std::endl;

    std::cout << "Thingy dependency:" << std::endl;
    std::cout << dpndnc::doThingy() << std::endl;
}
