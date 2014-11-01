-- A solution contains projects, and defines the available configurations
solution "SampSharp"
   configurations { "Debug", "Release" }

   -- A project defines one build target
   project "SampSharp"
      targetname "SampSharp"
      kind "SharedLib"

      language "C++"
      platforms { "x32" }
      defines { "LINUX", "SAMPGDK_AMALGAMATION" }
      
      libdirs { "lib/mono/linux" }
      links { "mono-2.0", "rt" }
      includedirs { "includes", "includes/sdk" }

      files { "**.cpp" }

      configuration "Debug"
         objdir "obj/linux/Debug"
         targetdir "bin/linux/Debug"
         defines { "DEBUG" }
         flags { "Symbols" }

      configuration "Release"
         objdir "obj/linux/Release"
         targetdir "bin/linux/Release"
         defines { "NDEBUG" }
         flags { "Optimize" }

