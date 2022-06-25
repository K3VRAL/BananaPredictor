CC		= gcc
CFLAGS	= -Wall -c
LFLAGS	= -lncurses
TARGET	= bnprdctr
BINFLR	= bin/

all:		CFLAGS += -g
all:		$(TARGET)

Linux64:	CFLAGS += -m64 -O3
Linux64:	$(TARGET)

Linux32:	CFLAGS += -m64 -O3
Linux32:	$(TARGET)

%.o: src/%.c include/%.h | $(BINFLR)
	$(CC) $(CFLAGS) -o $(BINFLR)$(notdir $@) $<

$(TARGET): $(addsuffix .o, $(basename $(notdir $(wildcard include/*.h))))
	$(CC) $(LFLAGS) -o $(BINFLR)$@ $(addprefix $(BINFLR), $(notdir $^))

$(BINFLR):
	[ -d $(BINFLR) ] || mkdir -p $(BINFLR)

clean:
	rm $(BINFLR)$(TARGET)* $(BINFLR)*.o