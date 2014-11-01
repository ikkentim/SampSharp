-- A solution contains projects, and defines the available configurations
solution "SampSharp"
   configurations { "Debug", "Release" }

   -- A project defines one build target
   project "SampSharp"
      targetname "SampSharp"
      kind "SharedLib"

      language "C++"
      platforms { "x32" }
      links { "mono-2.0", "dl" }
      includedirs { "includes", "includes/sdk" }
      buildoptions { "-fvisibility=hidden", "-fvisibility-inlines-hidden", "-m32" }
      
      files { "**.cpp", "includes/sampgdk/sampgdk.c" }

      configuration "Debug"
         objdir "obj/linux/Debug"
         -- targetdir "bin/linux/Debug"
         targetdir "../../environment/plugins"
         defines { "DEBUG", "LINUX", "SAMPGDK_AMALGAMATION", "_GNU_SOURCE" }
         flags { "Symbols" }

      configuration "Release"
         objdir "obj/linux/Release"
         targetdir "bin/linux/Release"
         defines { "NDEBUG", "LINUX", "SAMPGDK_AMALGAMATION", "_GNU_SOURCE" }
         flags { "Optimize" }

