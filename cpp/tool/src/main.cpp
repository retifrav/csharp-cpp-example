#include <iostream>

#include <Thingy/thingy.h>

int main(int argc, char *argv[])
{
    std::cout << "Base application message" << std::endl;
    std::cout << std::endl;

    std::cout << "Thingy dependency:" << std::endl;
    std::cout << dpndnc::doThingy() << std::endl;
}
