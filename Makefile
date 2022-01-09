CC = gcc
CFLAGS = -Wall
LFLAGS = 
TARGET = bnprdctr
BINFLR = bin/

all: CFLAGS += -g
all: $(TARGET)

%.o: %.c %.h | $(BINFLR)
	$(CC) $(CFLAGS) -o $(BINFLR)$(notdir $@) -c $<

$(TARGET): src/main.o src/run.o src/objects.o src/print.o src/fastrandom.o
	$(CC) $(CFLAGS) -o $(BINFLR)$@ $(addprefix $(BINFLR), $(notdir $^)) $(LFLAGS)

$(BINFLR):
	if [ ! -d $(BINFLR) ]; then mkdir $(BINFLR); fi

clean:
	rm $(BINFLR)*.o $(BINFLR)$(TARGET)