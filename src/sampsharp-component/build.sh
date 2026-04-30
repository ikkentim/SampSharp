#!/bin/bash
# Build script for SampSharp component (x64) on Linux

set -e

SRCDIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOTDIR="$(cd "$SRCDIR/../.." && pwd)"
BUILDDIR="$ROOTDIR/build/cmake/component"

echo "Building open.mp component..."
echo "Root: $ROOTDIR"
echo "Build: $BUILDDIR"
echo

mkdir -p "$BUILDDIR"

# Configure
cmake -S "$SRCDIR" -B "$BUILDDIR"

# Build
cmake --build "$BUILDDIR" --config RelWithDebInfo

echo
echo "Open.mp component build complete. Output: $BUILDDIR/artifacts"
