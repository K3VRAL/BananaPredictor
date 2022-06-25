#ifndef CONFIG_H
#define CONFIG_H

#include <stdio.h>

#include "map.h"

typedef struct Config {
    Map map;
} Config;

extern Config config;

#endif