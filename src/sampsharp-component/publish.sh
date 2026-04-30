#!/bin/bash
# Publish script for SampSharp component - copies release library to artifacts

set -e

SRCDIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOTDIR="$(cd "$SRCDIR/../.." && pwd)"
BUILDDIR="$ROOTDIR/build/cmake/component"
ARTIFACTDIR="$ROOTDIR/build/artifacts/sampsharp-component"

echo "Publishing SampSharp component..."
echo "Source: $BUILDDIR/artifacts"
echo "Destination: $ARTIFACTDIR"
echo

if [ ! -f "$BUILDDIR/artifacts/SampSharp.so" ] && [ ! -f "$BUILDDIR/artifacts/SampSharp.dll" ]; then
    echo "Error: SampSharp.so or SampSharp.dll not found at $BUILDDIR/artifacts"
    echo "Please run build first: build.sh"
    exit 1
fi

mkdir -p "$ARTIFACTDIR"

echo "Copying files..."
if [ -f "$BUILDDIR/artifacts/SampSharp.so" ]; then
    cp "$BUILDDIR/artifacts/SampSharp.so" "$ARTIFACTDIR/"
fi
if [ -f "$BUILDDIR/artifacts/SampSharp.dll" ]; then
    cp "$BUILDDIR/artifacts/SampSharp.dll" "$ARTIFACTDIR/" || true
    cp "$BUILDDIR/artifacts/SampSharp.pdb" "$ARTIFACTDIR/" 2>/dev/null || true
fi

echo
echo "Artifacts published to: $ARTIFACTDIR"
