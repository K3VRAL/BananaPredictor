CC	= gcc
CFLAGS	= -Wall -c
LFLAGS	= 
TARGET	= bnprdctr
BINFLR	= bin/

all:	CFLAGS += -g
all:	$(TARGET)

%.o: src/%.c include/%.h | $(BINFLR)
	$(CC) $(CFLAGS) -o $(BINFLR)$(notdir $@) -c $<

$(TARGET): $(addsuffix .o, $(basename $(notdir $(wildcard include/*.h))))
	$(CC) $(LFLAGS) -o $(BINFLR)$@ $(addprefix $(BINFLR), $(notdir $^))

$(BINFLR):
	[ -d $(BINFLR) ] || mkdir -p $(BINFLR)

clean:
	rm $(BINFLR)$(TARGET)* $(BINFLR)*.o