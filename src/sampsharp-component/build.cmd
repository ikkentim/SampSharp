cmake -S . -B build -A x64 -T ClangCL -DCMAKE_POLICY_VERSION_MINIMUM=3.5
cmake --build build --config RelWithDebInfo
