cmake_minimum_required(VERSION 3.19)
project(SampSharp LANGUAGES CXX C VERSION 1.0.0)

set(CMAKE_CXX_STANDARD 17)

# Compute root directory (3 levels up: src/legacy/SampSharp -> .)
get_filename_component(ROOTDIR "${CMAKE_SOURCE_DIR}/../../.." ABSOLUTE)

# Legacy plugin outputs to build/bin/SampSharp/Release
set(OUTDIR "${ROOTDIR}/build/bin/SampSharp/Release")

set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY "${OUTDIR}")
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY "${OUTDIR}")
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY "${OUTDIR}")

set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY_DEBUG "${OUTDIR}")
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY_DEBUG "${OUTDIR}")
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY_DEBUG "${OUTDIR}")

set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY_RELEASE "${OUTDIR}")
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY_RELEASE "${OUTDIR}")
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY_RELEASE "${OUTDIR}")

set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY_RELWITHDEBINFO "${OUTDIR}")
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY_RELWITHDEBINFO "${OUTDIR}")
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY_RELWITHDEBINFO "${OUTDIR}")

if(MSVC)
   set(CXX_STANDARD "/std:c++latest")
   set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} ${CXX_STANDARD} /Zc:preprocessor")
endif()

# Collect all source files
file(GLOB_RECURSE plugin_source_list CONFIGURE_DEPENDS
    "${CMAKE_CURRENT_SOURCE_DIR}/*.cpp"
    "${CMAKE_CURRENT_SOURCE_DIR}/*.c"
    "${CMAKE_CURRENT_SOURCE_DIR}/includes/sampgdk/sampgdk.c"
)

# Create the DLL/SO
if(WIN32)
    add_library(SampSharp SHARED ${plugin_source_list})
    set_target_properties(SampSharp PROPERTIES 
        SUFFIX ".dll"
        PREFIX ""
    )
else()
    add_library(SampSharp SHARED ${plugin_source_list})
    set_target_properties(SampSharp PROPERTIES 
        SUFFIX ".so"
        PREFIX "lib"
    )
endif()

# Include directories
target_include_directories(SampSharp PRIVATE
    "${CMAKE_CURRENT_SOURCE_DIR}/includes"
    "${CMAKE_CURRENT_SOURCE_DIR}/includes/sdk"
    "${CMAKE_CURRENT_SOURCE_DIR}/includes/sdk/amx"
)

# Compiler definitions
target_compile_definitions(SampSharp PRIVATE
    SAMPGDK_AMALGAMATION
    SAMPGDK_CPP_WRAPPERS
    _CRT_SECURE_NO_WARNINGS
)

if(UNIX AND NOT APPLE)
    target_compile_definitions(SampSharp PRIVATE LINUX _GNU_SOURCE)
    target_compile_options(SampSharp PRIVATE -m32 -fPIC -fvisibility=hidden -fvisibility-inlines-hidden)
    target_link_options(SampSharp PRIVATE -m32)
endif()

if(MSVC)
    set_property(TARGET SampSharp PROPERTY MSVC_RUNTIME_LIBRARY "MultiThreaded$<$<CONFIG:Debug>:Debug>")
endif()

# Platform-specific linking
if(WIN32)
    target_link_libraries(SampSharp PRIVATE Shlwapi)
endif()
