#!/bin/bash
# Root build dispatcher for SampSharp plugins
# Usage: build.sh <target> [action] [options]
# Targets: legacy-plugin, legacy-libraries, component, component-libraries
# Actions: (empty), test, publish
# Options: --no-build, --version=<version>

set -euo pipefail

show_usage() {
    echo ""
    echo "Usage:"
    echo "  build.sh legacy-plugin            - Build legacy x86 plugin"
    echo "  build.sh legacy-plugin publish    - Build and publish legacy x86 plugin"
    echo "  build.sh legacy-libraries         - Build legacy C# libraries"
    echo "  build.sh legacy-libraries publish - Build and pack legacy C# libraries"
    echo "  build.sh component                - Build open.mp component"
    echo "  build.sh component publish        - Build and publish open.mp component"
    echo "  build.sh component-libraries      - Build C# libraries"
    echo "  build.sh component-libraries test - Test C# libraries"
    echo "  build.sh component-libraries publish - Build and pack C# libraries"
    echo "  build.sh clean                    - Delete build directory contents"
    echo ""
    echo "Options:"
    echo "  --no-build            - Skip the build step for tests"
    echo "  --version=<version>   - Set the CI package version"
}

build_component_libraries() {
    local SCRIPTDIR="$1"
    cd "$SCRIPTDIR"

    echo ""
    echo "Building C# libraries..."
    if [ -n "$VERSION" ]; then
        dotnet build SampSharp.slnx -c Release "/p:CiVersion=$VERSION"
    else
        dotnet build SampSharp.slnx -c Release
    fi
}

test_component_libraries() {
    local SCRIPTDIR="$1"
    local RESULTSDIR="$SCRIPTDIR/build/test-results/component-libraries"
    cd "$SCRIPTDIR"

    mkdir -p "$RESULTSDIR"

    echo ""
    echo "Testing C# libraries..."

    local command=(dotnet test SampSharp.slnx -c Release --results-directory "$RESULTSDIR" --logger "trx;LogFilePrefix=component-libraries")

    if [ "$NO_BUILD" = true ]; then
        command+=(--no-build)
    fi

    if [ -n "$VERSION" ]; then
        command+=("/p:CiVersion=$VERSION")
    fi

    "${command[@]}"
}

pack_component_libraries() {
    local SCRIPTDIR="$1"
    cd "$SCRIPTDIR"

    echo ""
    echo "Packing C# libraries..."
    if [ -n "$VERSION" ]; then
        dotnet pack SampSharp.slnx -c Release "/p:CiVersion=$VERSION"
    else
        dotnet pack SampSharp.slnx -c Release
    fi

    echo ""
    echo "NuGet packages created in: $SCRIPTDIR/build/artifacts/packages"
}

build_legacy_libraries() {
    local SCRIPTDIR="$1"
    cd "$SCRIPTDIR/src/legacy"
    
    echo ""
    echo "Building C# libraries..."
    if [ -n "$VERSION" ]; then
        dotnet build SampSharp.Legacy.slnx -c Release "/p:CiVersion=$VERSION"
    else
        dotnet build SampSharp.Legacy.slnx -c Release
    fi
}

pack_legacy_libraries() {
    local SCRIPTDIR="$1"
    cd "$SCRIPTDIR/src/legacy"
    
    echo ""
    echo "Packing C# libraries..."
    if [ -n "$VERSION" ]; then
        dotnet pack SampSharp.Legacy.slnx -c Release "/p:CiVersion=$VERSION"
    else
        dotnet pack SampSharp.Legacy.slnx -c Release
    fi
    
    echo ""
    echo "NuGet packages created in: $SCRIPTDIR/build/artifacts/packages"
}

TARGET="${1:-}"
SCRIPTDIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

if [ -z "$TARGET" ]; then
    show_usage
    exit 1
fi

shift

ACTION=""
if [ $# -gt 0 ]; then
    case "$1" in
        test|publish)
            ACTION="$1"
            shift
            ;;
    esac
fi

VERSION=""
NO_BUILD=false

while [ $# -gt 0 ]; do
    case "$1" in
        --no-build)
            NO_BUILD=true
            ;;
        --version=*)
            VERSION="${1#--version=}"
            if [ -z "$VERSION" ]; then
                echo "Missing value for --version"
                exit 1
            fi
            ;;
        --version)
            shift
            if [ $# -eq 0 ]; then
                echo "Missing value for --version"
                exit 1
            fi
            VERSION="$1"
            ;;
        *)
            echo "Invalid option: $1"
            show_usage
            exit 1
            ;;
    esac

    shift
done

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
        if [ -z "$ACTION" ]; then
            echo "Building open.mp component..."
            "$SCRIPTDIR/src/sampsharp-component/build.sh"
        elif [ "$ACTION" = "publish" ]; then
            echo "Building and publishing open.mp component..."
            "$SCRIPTDIR/src/sampsharp-component/build.sh"
            "$SCRIPTDIR/src/sampsharp-component/publish.sh"
        else
            show_usage
            exit 1
        fi
        ;;
    component-libraries)
        if [ -z "$ACTION" ]; then
            build_component_libraries "$SCRIPTDIR"
        elif [ "$ACTION" = "test" ]; then
            test_component_libraries "$SCRIPTDIR"
        elif [ "$ACTION" = "publish" ]; then
            pack_component_libraries "$SCRIPTDIR"
        else
            show_usage
            exit 1
        fi
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
