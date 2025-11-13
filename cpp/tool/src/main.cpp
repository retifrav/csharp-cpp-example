#include <iostream>
#include <fstream>
//#include <windows.h>

#include <Thingy/thingy.h>

int main(int argc, char *argv[])
{
    // this will set the console encoding/codepage for the entire current
    // console session, so it will persist even if you recompile this executable
    // without setting the codepage
    // ...let alone that this is for Windows only
    //SetConsoleOutputCP(CP_UTF8);

    std::cout << "Base application message" << std::endl;
    std::cout << std::endl;

    std::string results = dpndnc::doThingy();
    std::cout << "Thingy dependency:" << std::endl;
    std::cout << results << std::endl;

    // the UTF-8 string is totally fine when writing to file, so encoding problems
    // are indeed only in the (Windows) console/terminal
    std::ofstream tmpFile("tmp-results.txt");
    if (tmpFile.is_open())
    {
        tmpFile << results << std::endl;
        tmpFile.close();
    }
    else { std::cerr << "[ERROR] Failed to write results to file"; }
}
