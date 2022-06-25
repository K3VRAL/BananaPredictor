#ifndef CONFIG_H
#define CONFIG_H

#include <stdio.h>

#include "map.h"
#include "project.h"

typedef struct Config {
    Map map;
    Project project;
} Config;

extern Config config;

#endif