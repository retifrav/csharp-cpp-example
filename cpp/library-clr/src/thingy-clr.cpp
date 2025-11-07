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

    static String^ WhoHasTheBestBoobs(String^ jsn, int bornIn)
    {
        // on some () hosts it doesn't seem to be required to do this, as apparently
        // `msclr::interop::marshal_as<std::string>()` manages `String^` just fine
        // on its own (converts from UTF-16 to UTF-8 on its own behind the scenes),
        // but on other hosts that isn't the case for some reason

        // for instance, on Windows ARM inside Parallels virtual machine on ARM-based Mac OS
        // I had to do this
        std::wstring_convert<std::codecvt_utf8_utf16<wchar_t>> converter;
        //
        // from System::String (UTF-16) to UTF-8
        std::wstring jsnWstring = msclr::interop::marshal_as<std::wstring>(jsn);
        std::string jsnString = converter.to_bytes(jsnWstring);
        //
        std::string bestBoobs = dpndnc::whoHasTheBestBoobs(jsnString, bornIn);
        //
        // from UTF-8 to UTF-16 (System::String)
        std::wstring bestBoobsWstring = converter.from_bytes(bestBoobs);
        //
        // return result using msclr
        return msclr::interop::marshal_as<String^>(bestBoobsWstring);
        // or
        //return gcnew String(bestBoobsWstring.c_str());

        // while on Windows x64 actual physical x64 host it was enough to just do this simple thing
        /*
        std::string bestBoobs = dpndnc::whoHasTheBestBoobs(
            msclr::interop::marshal_as<std::string>(jsn),
            bornIn
        );
        // using msclr
        return msclr::interop::marshal_as<String^>(bestBoobs);
        // or
        //return gcnew String(bestBoobs.c_str());
        */
    }
};

