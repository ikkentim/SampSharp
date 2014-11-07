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
      includedirs { "includes", "includes/sdk", "includes/sdk/amx" }
      buildoptions { "-fvisibility=hidden", "-fvisibility-inlines-hidden", "-m32" }
      
      files { "**.cpp", "includes/sampgdk/sampgdk.c" }

      configuration "Debug"
         objdir "obj/linux/Debug"
         -- targetdir "bin/linux/Debug"
         targetdir "../../environment/plugins"
         defines { "DEBUG", "LINUX", "_GNU_SOURCE", "SAMPGDK_AMALGAMATION" }
         flags { "Symbols" }

      configuration "Release"
         objdir "obj/linux/Release"
         targetdir "bin/linux/Release"
         defines { "NDEBUG", "LINUX", "_GNU_SOURCE", "SAMPGDK_AMALGAMATION" }
         flags { "Optimize" }

