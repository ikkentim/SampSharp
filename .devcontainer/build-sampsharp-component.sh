#!/bin/bash
# Builds the SampSharp open.mp component and installs it to the open.mp server's
# components directory so it is loaded automatically when the server starts.
#
# Prerequisites:
#   - The devcontainer setup (on-create.sh) must have completed successfully.
#   - Run from anywhere inside the devcontainer.

set -e

OPENMP_DIR="/openmp"
OPENMP_SERVER_DIR="$OPENMP_DIR/build/Output/RelWithDebInfo/Server"
OPENMP_COMPONENTS_DIR="$OPENMP_SERVER_DIR/components"

SCRIPTDIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOTDIR="$(cd "$SCRIPTDIR/.." && pwd)"
BUILDDIR="$ROOTDIR/build/cmake/component"

echo "=== Building SampSharp open.mp component ==="
echo ""

if [ ! -d "$OPENMP_COMPONENTS_DIR" ]; then
    echo "ERROR: open.mp components directory not found at $OPENMP_COMPONENTS_DIR"
    echo "Ensure the devcontainer setup script (on-create.sh) has completed successfully."
    exit 1
fi

echo "Component source : $ROOTDIR/src/sampsharp-component"
echo "Build directory  : $BUILDDIR"
echo "Install target   : $OPENMP_COMPONENTS_DIR"
echo ""

mkdir -p "$BUILDDIR"

cmake -S "$ROOTDIR/src/sampsharp-component" -B "$BUILDDIR" \
    -DCOMPONENTS_DIR="$OPENMP_COMPONENTS_DIR" \
    -DCMAKE_BUILD_TYPE=RelWithDebInfo

cmake --build "$BUILDDIR" --config RelWithDebInfo --parallel "$(nproc)"

echo ""
echo "=== Build complete ==="
echo "SampSharp.so installed to: $OPENMP_COMPONENTS_DIR"
echo ""
echo "To start the open.mp server:"
echo "  cd $OPENMP_SERVER_DIR && ./omp-server"
