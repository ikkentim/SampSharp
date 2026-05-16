cmake_minimum_required(VERSION 3.19)
project(sampsharp LANGUAGES CXX VERSION 1.0.0)

set(CMAKE_CXX_STANDARD 17)

add_subdirectory("../../external/sdk" "${CMAKE_BINARY_DIR}/external/sdk")
include_directories(SYSTEM "../../external/dotnet")
include_directories(SYSTEM "../../external/sdk/include")
link_directories("../../external/dotnet")

# can be specified as arugment cmake -DCOMPONENTS_DIR="path/to/x64/server/components"
if(DEFINED COMPONENTS_DIR AND NOT COMPONENTS_DIR STREQUAL "")
    set(OUTDIR "${COMPONENTS_DIR}")
else()
    set(OUTDIR "${CMAKE_BINARY_DIR}/artifacts")
endif()

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

if(WIN32)
    set(LIBNETHOST "${CMAKE_CURRENT_SOURCE_DIR}/../../external/dotnet/libnethost.lib")
else()
    set(LIBNETHOST "${CMAKE_CURRENT_SOURCE_DIR}/../../external/dotnet/libnethost.a")
endif()

if(MSVC)
   set(CXX_STANDARD "/std:c++latest")
   set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} ${CXX_STANDARD} /Zc:preprocessor")
endif()

file(GLOB_RECURSE component_source_list CONFIGURE_DEPENDS "${CMAKE_CURRENT_SOURCE_DIR}/*.cpp" "${CMAKE_CURRENT_SOURCE_DIR}/*.hpp")

add_library(SampSharp SHARED ${component_source_list})

set_target_properties(SampSharp PROPERTIES OUTPUT_NAME "SampSharp" PREFIX "")
set_property(TARGET SampSharp PROPERTY MSVC_RUNTIME_LIBRARY "MultiThreaded$<$<CONFIG:Debug>:Debug>")

target_compile_definitions(SampSharp PUBLIC
	WIN32_LEAN_AND_MEAN
	VC_EXTRALEAN
	NOGDI
    NETHOST_USE_AS_STATIC
)

if(WIN32)
target_link_libraries(SampSharp PRIVATE
    OMP-SDK
    dbghelp
    psapi
    $<$<CONFIG:Debug>:nethost>
    $<$<CONFIG:Release>:${LIBNETHOST}>
    $<$<CONFIG:RelWithDebInfo>:${LIBNETHOST}>
    $<$<CONFIG:MinSizeRel>:${LIBNETHOST}>
)
else()
    if(CMAKE_BUILD_TYPE STREQUAL "Debug")
        target_link_libraries(SampSharp PRIVATE OMP-SDK nethost)
    else()
        target_link_libraries(SampSharp PRIVATE OMP-SDK ${LIBNETHOST})
    endif() 
endif()