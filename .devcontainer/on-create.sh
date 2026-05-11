#!/bin/bash
# Devcontainer setup script for SampSharp.
# Installs build tools, clones the open.mp server at the pinned release commit,
# and builds it (x86_64) into /openmp.

set -e

# Pinned to the commit of open.mp release v1.5.8.3079
OPENMP_COMMIT="c6759bd8d265171ae3d86598895a23d5a8d92a3b"
OPENMP_DIR="/openmp"

echo "=== SampSharp devcontainer setup ==="
echo ""

# ---------------------------------------------------------------------------
# Initialize repository submodules (needed to build the SampSharp component)
# ---------------------------------------------------------------------------
echo "--- Initializing repository submodules ---"
git submodule update --init --recursive

# ---------------------------------------------------------------------------
# Install build dependencies
# ---------------------------------------------------------------------------
echo "--- Installing build dependencies ---"
sudo apt-get update -y
sudo apt-get install -y \
    clang \
    cmake \
    ninja-build \
    python3-venv \
    git

# ---------------------------------------------------------------------------
# Install Conan 1.x (open.mp requires Conan 1.57+; v2.x is not supported).
# A dedicated virtual environment is used to avoid conflicts with the
# system-managed Python installation (PEP 668).
# ---------------------------------------------------------------------------
echo "--- Installing Conan 1.x ---"
CONAN_VENV="/opt/conan-env"
sudo python3 -m venv "$CONAN_VENV"
sudo "$CONAN_VENV/bin/pip" install "conan==1.64.1"

# Expose the conan binary at a location that is always on PATH
sudo ln -sf "$CONAN_VENV/bin/conan" /usr/local/bin/conan

# ---------------------------------------------------------------------------
# Initialise the default Conan profile for the current host (x86_64 / clang)
# ---------------------------------------------------------------------------
echo "--- Setting up Conan profile ---"
export CC=clang
export CXX=clang++

conan profile new default --detect 2>/dev/null || true

CLANG_MAJOR=$(clang --version 2>&1 | sed 's/.*version \([0-9]*\).*/\1/' | head -1)
conan profile update settings.compiler=clang default
conan profile update "settings.compiler.version=$CLANG_MAJOR" default
conan profile update settings.compiler.libcxx=libstdc++11 default

echo "Conan profile:"
conan profile show default

# ---------------------------------------------------------------------------
# Clone open.mp source at the pinned commit and pull submodules
# ---------------------------------------------------------------------------
echo "--- Creating /openmp directory ---"
sudo mkdir -p "$OPENMP_DIR"
sudo chown "$(id -u):$(id -g)" "$OPENMP_DIR"

echo "--- Cloning open.mp at commit $OPENMP_COMMIT ---"
git clone https://github.com/openmultiplayer/open.mp "$OPENMP_DIR"
cd "$OPENMP_DIR"
git checkout "$OPENMP_COMMIT"
git submodule update --init --recursive

# ---------------------------------------------------------------------------
# Build the open.mp server (x86_64 to match the SampSharp x64 component)
# ---------------------------------------------------------------------------
echo "--- Building open.mp server (x86_64) ---"
mkdir -p "$OPENMP_DIR/build"
cd "$OPENMP_DIR/build"

cmake .. \
    -G Ninja \
    -DCMAKE_C_COMPILER=clang \
    -DCMAKE_CXX_COMPILER=clang++ \
    -DTARGET_BUILD_ARCH=x86_64 \
    -DCMAKE_BUILD_TYPE=RelWithDebInfo \
    -DSHARED_OPENSSL=ON \
    -DSTATIC_STDCXX=false \
    -DBUILD_SERVER=ON

cmake --build . --config RelWithDebInfo --parallel "$(nproc)"

echo ""
echo "=== Setup complete ==="
echo ""
echo "open.mp server: $OPENMP_DIR/build/Output/RelWithDebInfo/Server/"
echo ""
echo "Next step: run .devcontainer/build-sampsharp-component.sh to build and"
echo "install the SampSharp component, then start the server."
