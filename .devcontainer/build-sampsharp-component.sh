#!/bin/bash
# Builds the SampSharp open.mp component and installs it to the open.mp server's
# components directory so it is loaded automatically when the server starts.
#
# Prerequisites:
#   - The devcontainer setup (on-create.sh) must have completed successfully.
#   - Run from anywhere inside the devcontainer.

set -e

OPENMP_DIR="/openmp"
OPENMP_COMPONENTS_DIR="$OPENMP_DIR/components"

SCRIPTDIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOTDIR="$(cd "$SCRIPTDIR/.." && pwd)"
ARTIFACTS_DIR="$ROOTDIR/build/artifacts/sampsharp-component"

echo "=== Building SampSharp open.mp component ==="
echo ""

if [ ! -d "$OPENMP_COMPONENTS_DIR" ]; then
    echo "ERROR: open.mp components directory not found at $OPENMP_COMPONENTS_DIR"
    echo "Ensure the devcontainer setup script (on-create.sh) has completed successfully."
    exit 1
fi

echo "Component source : $ROOTDIR/src/sampsharp-component"
echo "Build output     : $ARTIFACTS_DIR"
echo "Install target   : $OPENMP_COMPONENTS_DIR"
echo ""

# Build and publish the component
cd "$ROOTDIR"
./build.sh component publish

# Copy the built component to the open.mp server
if [ -f "$ARTIFACTS_DIR/SampSharp.so" ]; then
    cp "$ARTIFACTS_DIR/SampSharp.so" "$OPENMP_COMPONENTS_DIR/"
    echo ""
    echo "=== Build complete ==="
    echo "SampSharp.so installed to: $OPENMP_COMPONENTS_DIR"
else
    echo ""
    echo "ERROR: SampSharp.so not found at $ARTIFACTS_DIR/SampSharp.so"
    exit 1
fi

echo ""
echo "To start the open.mp server:"
echo "  cd $OPENMP_DIR && ./omp-server"
