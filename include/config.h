#ifndef CONFIG_H
#define CONFIG_H

#include <stdio.h>

#include "map.h"
#include "user.h"

typedef struct Config {
    Map map;
    User user;
} Config;

extern Config config;

#endif