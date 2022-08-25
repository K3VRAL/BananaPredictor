CC	= gcc
CFLAGS	= -Wall -c -Iinclude/
LFLAGS	= -losu
TARGET	= bnprdctr
BINFLR	= bin/

all:	CFLAGS += -g
all:	$(TARGET)

%.o: %.c | $(BINFLR)
	$(CC) $(CFLAGS) -o $(BINFLR)$(notdir $@) $<

$(TARGET): $(addsuffix .o, $(basename $(shell find include/ -type f -name "*.h" | grep -Po '(?<=include/).*' | sed 's/^/src\//')))
	$(CC) -o $(BINFLR)$@ $(addprefix $(BINFLR), $(notdir $^)) $(LFLAGS)

$(BINFLR):
	[ -d $(BINFLR) ] || mkdir -p $(BINFLR)

clean:
	rm $(BINFLR)$(TARGET)* $(BINFLR)*.o