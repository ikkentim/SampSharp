


TARGET=bin/libSampSharp.so

CC=gcc
CXX=g++

SRCDIR=src/SampSharp
BINDIR=bin
OBJDIR=bin/obj
FLAGS= -O2 -fPIC -fvisibility=hidden -fvisibility-inlines-hidden -I$(SRCDIR)/includes -I$(SRCDIR)/includes/sdk -I$(SRCDIR)/includes/sdk/amx -DNDEBUG -DLINUX -D_GNU_SOURCE -DSAMPGDK_AMALGAMATION
CFLAGS= -m32
CXXFLAGS=-std=c++17 -m32
LDFLAGS=-shared

SOURCES_CXX = $(shell echo $(SRCDIR)/*.cpp) $(SRCDIR)/includes/sdk/amxplugin.cpp
SOURCES_C = $(SRCDIR)/includes/sampgdk/sampgdk.c
OBJECTS=$(SOURCES_CXX:$(SRCDIR)/%.cpp=$(OBJDIR)/%.o) $(SOURCES_C:$(SRCDIR)/%.c=$(OBJDIR)/%.o)

all: $(TARGET)

.PHONY: clean

clean:
	rm -rf $(BINDIR)

$(OBJDIR)/%.o: $(SRCDIR)/%.cpp
	@mkdir -p $(@D)
	@echo $(CXX) $<
	@$(CXX) $(FLAGS) $(CFLAGS) $(CXXFLAGS) -c -o "$@" "$<"

$(OBJDIR)/%.o: $(SRCDIR)/%.c
	@mkdir -p $(@D)
	@echo $(CC) $<
	@$(CC) $(FLAGS) $(CFLAGS) -c -o "$@" "$<"

$(TARGET): $(OBJECTS)
	@mkdir -p $(@D)
	$(CXX) $(CXXFLAGS) $(OBJECTS) -o $@ $(LDFLAGS)
