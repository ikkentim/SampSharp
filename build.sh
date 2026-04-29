#!/bin/bash
# Root build dispatcher for SampSharp plugins
# Usage: build.sh <target> [action]
# Targets: legacy-plugin, legacy-libraries, component, component-libraries
# Actions: (empty), publish

set -e

show_usage() {
    echo ""
    echo "Usage:"
    echo "  build.sh legacy-plugin            - Build legacy x86 plugin"
    echo "  build.sh legacy-plugin publish    - Build and publish legacy x86 plugin"
    echo "  build.sh legacy-libraries         - Build legacy C# libraries"
    echo "  build.sh legacy-libraries publish - Build and pack legacy C# libraries"
    echo "  build.sh component                - Build component x64 plugin (not implemented)"
    echo "  build.sh component-libraries      - Build component C# libraries (not implemented)"
    echo "  build.sh clean                    - Delete build directory contents"
}

build_legacy_libraries() {
    local SCRIPTDIR="$1"
    cd "$SCRIPTDIR/src/legacy"
    
    echo ""
    echo "Building C# libraries..."
    dotnet build SampSharp.sln -c Release
}

pack_legacy_libraries() {
    local SCRIPTDIR="$1"
    cd "$SCRIPTDIR/src/legacy"
    
    echo ""
    echo "Packing C# libraries..."
    dotnet pack SampSharp.sln -c Release
    
    echo ""
    echo "NuGet packages created in: $SCRIPTDIR/build/artifacts/packages"
}

TARGET="${1}"
ACTION="${2}"
SCRIPTDIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

if [ -z "$TARGET" ]; then
    show_usage
    exit 1
fi

case "$TARGET" in
    legacy-plugin)
        if [ -z "$ACTION" ]; then
            echo "Building legacy plugin (x86)..."
            "$SCRIPTDIR/src/legacy/SampSharp/build.sh"
        elif [ "$ACTION" = "publish" ]; then
            echo "Building and publishing legacy plugin (x86)..."
            "$SCRIPTDIR/src/legacy/SampSharp/build.sh"
            "$SCRIPTDIR/src/legacy/SampSharp/publish.sh"
        else
            show_usage
            exit 1
        fi
        ;;
    legacy-libraries)
        if [ -z "$ACTION" ]; then
            build_legacy_libraries "$SCRIPTDIR"
        elif [ "$ACTION" = "publish" ]; then
            pack_legacy_libraries "$SCRIPTDIR"
        else
            show_usage
            exit 1
        fi
        ;;
    component)
        echo "Component x64 plugin build not yet implemented."
        ;;
    component-libraries)
        echo "Component C# libraries not yet implemented."
        ;;
    clean)
        echo "Cleaning build directory..."
        rm -rf "$SCRIPTDIR/build"
        mkdir -p "$SCRIPTDIR/build"
        echo "Build directory cleaned."
        ;;
    *)
        echo "Invalid target: $TARGET"
        show_usage
        exit 1
        ;;
esac

echo "Build complete."
exit 0
