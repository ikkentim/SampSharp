#!/bin/bash
# Build script for SampSharp component (x64) on Linux

set -e

SRCDIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOTDIR="$(cd "$SRCDIR/../../.." && pwd)"
BUILDDIR="$ROOTDIR/build/cmake/component"

echo "Building SampSharp component (x64)..."
echo "Root: $ROOTDIR"
echo "Build: $BUILDDIR"
echo

mkdir -p "$BUILDDIR"

# Configure
cmake -S "$SRCDIR" -B "$BUILDDIR"

# Build
cmake --build "$BUILDDIR" --config RelWithDebInfo

echo
echo "Component build complete. Output: $BUILDDIR/artifacts"
