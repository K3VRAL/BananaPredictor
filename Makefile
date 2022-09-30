CC	= gcc
CFLAGS	= -Wall -c -Iinclude/ -g $(shell pkg-config --cflags libosu)
LFLAGS	= $(shell pkg-config --libs libosu)
TARGET	= bnprdctr
BINFLR	= bin/

all:	$(TARGET)

# Compiling specific object
%.o: %.c | $(BINFLR)
	$(CC) $(CFLAGS) -o $(BINFLR)$(notdir $@) $<

# Compiling all objects to an executable
$(TARGET): $(addsuffix .o, $(basename $(shell find include/ -type f -name "*.h" | grep -Po '(?<=include/).*' | sed 's/^/src\//')))
	$(CC) -o $(BINFLR)$@ $(addprefix $(BINFLR), $(notdir $^)) $(LFLAGS)

# TODO man pages
# Install
install:
	$(shell rm -rf /usr/local/bin/$(TARGET))
	$(shell cp ./bin/bnprdctr /usr/local/bin/$(TARGET))
	$(shell rm -rf /usr/bin/$(TARGET))
	$(shell ln -s /usr/local/bin/$(TARGET) /usr/bin/)

# Uninstall
uninstall:
	$(shell rm -rf /usr/local/bin/$(TARGET))
	$(shell rm -rf /usr/bin/$(TARGET))

# Make bin/ folder
$(BINFLR):
	[ -d $(BINFLR) ] || mkdir -p $(BINFLR)

# Clean up
clean:
	rm $(BINFLR)$(TARGET)* $(BINFLR)*.o