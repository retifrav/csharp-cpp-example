#include <msclr/marshal_cppstd.h>

#include <Thingy/thingy.h>

using namespace System;

public ref class ThingyWrapperCLR
{
public:
    static String^ DoThingy()
    {
        std::string thng = dpndnc::doThingy();
        // using msclr
        return msclr::interop::marshal_as<String^>(thng);
        // or
        //return gcnew String(thng.c_str());
    }

    static String^ WhoHasTheBestBoobs(String^ jsn)
    {
        std::string bestBoobs = dpndnc::whoHasTheBestBoobs(
            msclr::interop::marshal_as<std::string>(jsn)
        );
        // using msclr
        return msclr::interop::marshal_as<String^>(bestBoobs);
        // or
        //return gcnew String(bestBoobs.c_str());
    }
};

