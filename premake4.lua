-- A solution contains projects, and defines the available configurations
solution "SampSharp"
   configurations { "Debug", "Release" }

   -- A project defines one build target
   project "SampSharp"
      targetname "SampSharp"
      kind "SharedLib"

      language "C++"
      platforms { "x32" }
      links { "mono-2.0", "rt" }
      
      includedirs { "src/SampSharp/includes", "src/SampSharp/includes/sdk", "src/SampSharp/includes/sdk/amx" }
      buildoptions { "-fvisibility=hidden", "-fvisibility-inlines-hidden", "-m32", "-std=c++11", "-stdlib=libc++" }
      
      files { "src/SampSharp**.cpp", "src/SampSharp/includes/sampgdk/sampgdk.c" }

      configuration "Debug"
         objdir "obj/Debug"
         targetdir "env/plugins"
         defines { "DEBUG", "LINUX", "_GNU_SOURCE", "SAMPGDK_AMALGAMATION" }
         flags { "Symbols" }

      configuration "Release"
         objdir "obj/Release"
         targetdir "bin"
         defines { "NDEBUG", "LINUX", "_GNU_SOURCE", "SAMPGDK_AMALGAMATION" }
         flags { "Optimize" }

