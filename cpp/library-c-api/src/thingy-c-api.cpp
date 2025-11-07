#include <Thingy/thingy.h>

extern "C" __declspec(dllexport) const char *do_thingy_c()
{
    // to keep the string in memory it needs to be `static`, otherwise it will be destroyed
    // after `do_thingy()` does the return, so in C# that would be a pointer to invalid memory
    static std::string thng = dpndnc::doThingy();

    // return C-style string
    return thng.c_str();
}

extern "C" __declspec(dllexport) const char *who_has_the_best_boobs_c(const char *jsn, int bornIn)
{
    static std::string bestBoobs = dpndnc::whoHasTheBestBoobs(std::string(jsn), bornIn);
    return bestBoobs.c_str();
}
