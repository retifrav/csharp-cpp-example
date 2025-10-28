#include <Thingy/thingy-c-api.h>
#include <Thingy/thingy.h>

static std::string thng;

const char *do_thingy()
{
    thng = dpndnc::doThingy(); // call the C++ function
    return thng.c_str(); // return C-style string
}
