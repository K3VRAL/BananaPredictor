#include "objects.h"

char *etsType(types type) {
    return  type == circle   ?   "cirle"   :
            type == slider   ?   "slider"  :
            type == spinner  ?   "spinner" :
                                "isnull";
}