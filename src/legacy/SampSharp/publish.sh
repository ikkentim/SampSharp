#!/bin/bash
# Publish script for legacy SampSharp plugin - copies release DLL/SO to artifacts

set -e

SRCDIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOTDIR="$(cd "$SRCDIR/../../.." && pwd)"
DLLSOURCE="$ROOTDIR/build/bin/SampSharp/Release"
ARTIFACTDIR="$ROOTDIR/build/artifacts/sampsharp-legacy"

echo "Publishing legacy SampSharp plugin..."
echo "Source: $DLLSOURCE"
echo "Destination: $ARTIFACTDIR"
echo ""

# Check for the built library (either .so or .dll on Windows/WSL)
if [ ! -f "$DLLSOURCE/libSampSharp.so" ] && [ ! -f "$DLLSOURCE/SampSharp.dll" ]; then
    echo "Error: libSampSharp.so or SampSharp.dll not found at $DLLSOURCE"
    echo "Please run build first: build.sh legacy"
    exit 1
fi

mkdir -p "$ARTIFACTDIR"

echo "Copying files..."
if [ -f "$DLLSOURCE/libSampSharp.so" ]; then
    cp "$DLLSOURCE/libSampSharp.so" "$ARTIFACTDIR/" || true
fi
if [ -f "$DLLSOURCE/SampSharp.dll" ]; then
    cp "$DLLSOURCE/SampSharp.dll" "$ARTIFACTDIR/" || true
    cp "$DLLSOURCE/SampSharp.pdb" "$ARTIFACTDIR/" 2>/dev/null || true
    cp "$DLLSOURCE/SampSharp.lib" "$ARTIFACTDIR/" 2>/dev/null || true
fi

echo ""
echo "Artifacts published to: $ARTIFACTDIR"
