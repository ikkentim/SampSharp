#!/bin/bash
# Devcontainer setup script for SampSharp.
# Installs build tools, downloads the pre-built open.mp server (x86_64) into
# /openmp, and initialises repository submodules.

set -e

# Pre-built open.mp release v1.5.8.3079 (x86_64, dynamic OpenSSL)
OPENMP_URL="https://github.com/SampSharp/openmultiplayer-x64-builds/releases/download/v1.5.8.3079/open.mp-linux-x86_64-dynssl-v1.5.8.3079.tar.xz"
OPENMP_DIR="/openmp"

echo "=== SampSharp devcontainer setup ==="
echo ""

# ---------------------------------------------------------------------------
# Initialize repository submodules (needed to build the SampSharp component)
# ---------------------------------------------------------------------------
echo "--- Initializing repository submodules ---"
git submodule update --init --recursive

# ---------------------------------------------------------------------------
# Install build dependencies (for building the SampSharp component)
# ---------------------------------------------------------------------------
echo "--- Installing build dependencies ---"
sudo apt-get update -y
sudo apt-get install -y \
    clang \
    clang-format \
    clangd \
    cmake \
    ninja-build \
    xz-utils \
    git

# ---------------------------------------------------------------------------
# Download and extract the pre-built open.mp server
# ---------------------------------------------------------------------------
echo "--- Downloading open.mp server ($OPENMP_URL) ---"
sudo mkdir -p "$OPENMP_DIR"
sudo chown "$(id -u):$(id -g)" "$OPENMP_DIR"

curl -fsSL "$OPENMP_URL" | tar -xJ --strip-components=1 -C "$OPENMP_DIR"

# ---------------------------------------------------------------------------
# Build and install the SampSharp component
# ---------------------------------------------------------------------------
echo ""
echo "--- Building and installing SampSharp component ---"
bash .devcontainer/build-sampsharp-component.sh

# ---------------------------------------------------------------------------
# Install sampsharp development utility
# ---------------------------------------------------------------------------
echo ""
echo "--- Installing sampsharp utility ---"
sudo cp .devcontainer/sampsharp /usr/local/bin/sampsharp
sudo chmod +x /usr/local/bin/sampsharp

echo ""
echo "=== Devcontainer setup complete ==="
