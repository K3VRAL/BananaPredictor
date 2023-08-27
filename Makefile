CC	= gcc
CFLAGS	= -Wall -c -Iinclude/ $(shell pkg-config --cflags libosu)
LFLAGS	= $(shell pkg-config --libs libosu) -lm
TARGET	= bnprdctr
BINFLR	= bin/

all:	CFLAGS += -g
all:	$(TARGET)

unix64:	CFLAGS += -m64
unix64:	LFLAGS += -m64
unix64:	$(TARGET)

unix32:	CFLAGS += -m32
unix32: LFLAGS += -m32
unix32:	$(TARGET)

win64:	CC = x86_64-w64-mingw32-gcc
win64:	LFLAGS += -static-libgcc -L.
win64:	$(TARGET)

win32:	CC = i686-w64-mingw32-gcc
win32:	LFLAGS += -static-libgcc -static-libstdc++ -L.
win32:	$(TARGET)

# Compiling specific object
%.o: %.c | $(BINFLR)
	$(CC) $(CFLAGS) -o $(BINFLR)$(notdir $@) $<

# Compiling all objects to an executable
$(TARGET): $(addsuffix .o, $(basename $(shell find include/ -type f -name "*.h" | grep -Po '(?<=include/).*' | sed 's/^/src\//')))
	$(CC) -o $(BINFLR)$@ $(addprefix $(BINFLR), $(notdir $^)) $(LFLAGS)

# Install
install:
	$(shell cp ./bin/$(TARGET) /usr/local/bin/$(TARGET))
	$(shell chmod 755 /usr/local/bin/$(TARGET))
	$(shell ln -s /usr/local/bin/$(TARGET) /usr/bin/)

# Uninstall
uninstall:
	$(shell test -L /usr/bin/$(TARGET) && unlink /usr/bin/$(TARGET))
	$(shell test -f /usr/local/bin/$(TARGET) && rm -rf /usr/local/bin/$(TARGET))

# Make bin/ folder
$(BINFLR):
	$(shell mkdir -p $@)

# Clean up
clean:
	$(shell rm -rf $(BINFLR))
