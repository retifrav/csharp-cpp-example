#include <codecvt>
#include <msclr/marshal_cppstd.h>

#include <Thingy/thingy.h>

using namespace System;

public ref class ThingyWrapperCLR
{
public:
    static String^ DoThingy()
    {
        std::string thng = dpndnc::doThingy();

        // convert from UTF-8 to UTF-16 (System::String)
        std::wstring_convert<std::codecvt_utf8_utf16<wchar_t>> converter;
        std::wstring thngWstring = converter.from_bytes(thng);

        // return result using msclr
        return msclr::interop::marshal_as<String^>(thngWstring);
        // or
        //return gcnew String(thngWstring.c_str());
    }

    static String^ WhoHasTheBestBoobs(String^ jsn)
    {
        std::wstring_convert<std::codecvt_utf8_utf16<wchar_t>> converter;

        // from System::String (UTF-16) to UTF-8
        std::wstring jsnWstring = msclr::interop::marshal_as<std::wstring>(jsn);
        std::string jsnString = converter.to_bytes(jsnWstring);

        std::string bestBoobs = dpndnc::whoHasTheBestBoobs(jsnString);

        // from UTF-8 to UTF-16 (System::String)
        std::wstring bestBoobsWstring = converter.from_bytes(bestBoobs);

        // return result using msclr
        return msclr::interop::marshal_as<String^>(bestBoobsWstring);
        // or
        //return gcnew String(bestBoobsWstring.c_str());
    }
};

