CC	= gcc
CFLAGS	= -Wall -c -Iinclude/ -g $(shell pkg-config --cflags libosu)
LFLAGS	= $(shell pkg-config --libs libosu)
TARGET	= bnprdctr
BINFLR	= bin/

all:	$(TARGET)

%.o: %.c | $(BINFLR)
	$(CC) $(CFLAGS) -o $(BINFLR)$(notdir $@) $<

$(TARGET): $(addsuffix .o, $(basename $(shell find include/ -type f -name "*.h" | grep -Po '(?<=include/).*' | sed 's/^/src\//')))
	$(CC) -o $(BINFLR)$@ $(addprefix $(BINFLR), $(notdir $^)) $(LFLAGS)

$(BINFLR):
	[ -d $(BINFLR) ] || mkdir -p $(BINFLR)

clean:
	rm $(BINFLR)$(TARGET)* $(BINFLR)*.o