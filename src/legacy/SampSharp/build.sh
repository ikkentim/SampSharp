#!/bin/bash
# Build script for legacy SampSharp x86 plugin on Linux

set -e

SRCDIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOTDIR="$(cd "$SRCDIR/../../.." && pwd)"
BUILDDIR="$ROOTDIR/build/cmake/legacy"

echo "Building legacy SampSharp plugin (x86)..."

mkdir -p "$BUILDDIR"

# Configure
cmake -S "$SRCDIR" -B "$BUILDDIR"

# Build
cmake --build "$BUILDDIR" --config RelWithDebInfo

echo "Legacy plugin build complete. Output: $ROOTDIR/build/bin/SampSharp/Release"
